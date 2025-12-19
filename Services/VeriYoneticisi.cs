using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp.Services
{
    /// <summary>
    /// Veri yönetimi servisi - Singleton Pattern kullandık.
    /// JSON dosyalarına veri kaydetme/okuma işlemleri yaptık.
    /// </summary>
    public class VeriYoneticisi
    {
        private static VeriYoneticisi _instance;
        private static readonly object _lock = new object();
        
        private readonly string _veriKlasoru;
        private readonly string _veriDosyasiYolu;
        private List<EvcilHayvan> _evcilHayvanlar;
        private List<SokakHayvani> _sokakHayvanlari;
        private List<VeterinerAdmin> _veterinerler;
        private List<HayvanSahibi> _hayvanSahipleri;
        private List<SokakHayvaniSorumlusu> _sorumlular;
        private List<Randevu> _randevular;
        private List<Muayene> _muayeneler;

        private int _sonHayvanId = 0;
        private int _sonKullaniciId = 0;
        private int _sonRandevuId = 0;
        private int _sonMuayeneId = 0;

        
        public static VeriYoneticisi Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new VeriYoneticisi();
                        }
                    }
                }
                return _instance;
            }
        }

        // private constructor kullandık
        private VeriYoneticisi()
        {
            _veriKlasoru = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Veriler");
            _veriDosyasiYolu = Path.Combine(_veriKlasoru, "veriler.json");
            
            if (!Directory.Exists(_veriKlasoru))
                Directory.CreateDirectory(_veriKlasoru);

            _evcilHayvanlar = new List<EvcilHayvan>();
            _sokakHayvanlari = new List<SokakHayvani>();
            _veterinerler = new List<VeterinerAdmin>();
            _hayvanSahipleri = new List<HayvanSahibi>();
            _sorumlular = new List<SokakHayvaniSorumlusu>();
            _randevular = new List<Randevu>();
            _muayeneler = new List<Muayene>();
            
            // uygulama başladığında kayıtlı verileri yükle program kapandığında veri kaybı olmasın
            VerileriYukle();
        }

        #region Properties

        public List<EvcilHayvan> EvcilHayvanlar => _evcilHayvanlar;
        public List<SokakHayvani> SokakHayvanlari => _sokakHayvanlari;
        public List<VeterinerAdmin> Veterinerler => _veterinerler;
        public List<HayvanSahibi> HayvanSahipleri => _hayvanSahipleri;
        public List<SokakHayvaniSorumlusu> Sorumlular => _sorumlular;
        public List<Randevu> Randevular => _randevular;
        public List<Muayene> Muayeneler => _muayeneler;

        #endregion

        #region ID Generators

        public int YeniHayvanId() => ++_sonHayvanId;
        public int YeniKullaniciId() => ++_sonKullaniciId;
        public int YeniRandevuId() => ++_sonRandevuId;
        public int YeniMuayeneId() => ++_sonMuayeneId;

        #endregion

        #region Hayvan İşlemleri

        public void EvcilHayvanEkle(EvcilHayvan hayvan)
        {
            if (hayvan != null && !_evcilHayvanlar.Contains(hayvan))
            {
                _evcilHayvanlar.Add(hayvan);
                VerileriKaydet(); // Otomatik kaydet
            }
        }

        public void SokakHayvaniEkle(SokakHayvani hayvan)
        {
            if (hayvan != null && !_sokakHayvanlari.Contains(hayvan))
            {
                _sokakHayvanlari.Add(hayvan);
                VerileriKaydet(); // otomatik kaydet
            }
        }

        public EvcilHayvan EvcilHayvanBul(int id)
        {
            foreach (var h in _evcilHayvanlar)
                if (h.Id == id) return h;
            return null;
        }

        public SokakHayvani SokakHayvaniBul(int id)
        {
            foreach (var h in _sokakHayvanlari)
                if (h.Id == id) return h;
            return null;
        }

        public bool EvcilHayvanSil(int id)
        {
            var hayvan = EvcilHayvanBul(id);
            if (hayvan != null)
            {
                var result = _evcilHayvanlar.Remove(hayvan);
                VerileriKaydet(); // otomatik kaydet
                return result;
            }
            return false;
        }

        #endregion

        #region Kullanıcı İşlemleri

        public void VeterinerEkle(VeterinerAdmin veteriner)
        {
            if (veteriner != null)
            {
                _veterinerler.Add(veteriner);
                VerileriKaydet(); 
            }
        }

        public void HayvanSahibiEkle(HayvanSahibi sahip)
        {
            if (sahip != null)
            {
                _hayvanSahipleri.Add(sahip);
                VerileriKaydet(); 
            }
        }

        public void SorumluEkle(SokakHayvaniSorumlusu sorumlu)
        {
            if (sorumlu != null)
            {
                _sorumlular.Add(sorumlu);
                VerileriKaydet();
            }
        }

        #endregion

        #region Randevu İşlemleri

        public void RandevuEkle(Randevu randevu)
        {
            if (randevu != null)
            {
                _randevular.Add(randevu);
                VerileriKaydet();
            }
        }

        public Randevu RandevuBul(int id)
        {
            foreach (var r in _randevular)
                if (r.Id == id) return r;
            return null;
        }

        public List<Randevu> BekleyenRandevular()
        {
            var liste = new List<Randevu>();
            foreach (var r in _randevular)
                if (r.Durum == Enums.RandevuDurumu.Bekliyor)
                    liste.Add(r);
            return liste;
        }

        #endregion

        #region Muayene İşlemleri

        public void MuayeneEkle(Muayene muayene)
        {
            if (muayene != null)
            {
                _muayeneler.Add(muayene);
                VerileriKaydet(); 
            }
        }

        public int GunlukMuayeneSayisi(DateTime tarih)
        {
            int sayi = 0;
            foreach (var m in _muayeneler)
                if (m.MuayeneTarihi.Date == tarih.Date)
                    sayi++;
            return sayi;
        }

        #endregion

        #region İstatistikler

        public int ToplamHayvanSayisi() => _evcilHayvanlar.Count + _sokakHayvanlari.Count;
        public int ToplamKullaniciSayisi() => _veterinerler.Count + _hayvanSahipleri.Count + _sorumlular.Count;

        #endregion

        #region Veri Kaydetme/Yükleme

        /// <summary>
        /// Tüm verileri JSON dosyasına kaydet
        /// </summary>
        public void VerileriKaydet()
        {
            try
            {
                var tumVeriler = new VeriPaketi
                {
                    EvcilHayvanlar = _evcilHayvanlar,
                    SokakHayvanlari = _sokakHayvanlari,
                    Veterinerler = _veterinerler,
                    HayvanSahipleri = _hayvanSahipleri,
                    Sorumlular = _sorumlular,
                    Randevular = _randevular,
                    Muayeneler = _muayeneler,
                    SonHayvanId = _sonHayvanId,
                    SonKullaniciId = _sonKullaniciId,
                    SonRandevuId = _sonRandevuId,
                    SonMuayeneId = _sonMuayeneId
                };

                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                string json = JsonSerializer.Serialize(tumVeriler, options);
                File.WriteAllText(_veriDosyasiYolu, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Veri kaydetme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// JSON dosyasından verileri yükler
        /// </summary>
        public void VerileriYukle()
        {
            try
            {
                if (File.Exists(_veriDosyasiYolu))
                {
                    string json = File.ReadAllText(_veriDosyasiYolu);
                    
                    var options = new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    
                    var tumVeriler = JsonSerializer.Deserialize<VeriPaketi>(json, options);
                    
                    if (tumVeriler != null)
                    {
                        _evcilHayvanlar = tumVeriler.EvcilHayvanlar ?? new List<EvcilHayvan>();
                        _sokakHayvanlari = tumVeriler.SokakHayvanlari ?? new List<SokakHayvani>();
                        _veterinerler = tumVeriler.Veterinerler ?? new List<VeterinerAdmin>();
                        _hayvanSahipleri = tumVeriler.HayvanSahipleri ?? new List<HayvanSahibi>();
                        _sorumlular = tumVeriler.Sorumlular ?? new List<SokakHayvaniSorumlusu>();
                        _randevular = tumVeriler.Randevular ?? new List<Randevu>();
                        _muayeneler = tumVeriler.Muayeneler ?? new List<Muayene>();
                        _sonHayvanId = tumVeriler.SonHayvanId;
                        _sonKullaniciId = tumVeriler.SonKullaniciId;
                        _sonRandevuId = tumVeriler.SonRandevuId;
                        _sonMuayeneId = tumVeriler.SonMuayeneId;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Veri yükleme hatası: {ex.Message}");
            }
        }

        #endregion
    }

    /// <summary>
    /// JSON serileştirme için veri paketi sınıfı
    /// </summary>
    public class VeriPaketi
    {
        public List<EvcilHayvan> EvcilHayvanlar { get; set; }
        public List<SokakHayvani> SokakHayvanlari { get; set; }
        public List<VeterinerAdmin> Veterinerler { get; set; }
        public List<HayvanSahibi> HayvanSahipleri { get; set; }
        public List<SokakHayvaniSorumlusu> Sorumlular { get; set; }
        public List<Randevu> Randevular { get; set; }
        public List<Muayene> Muayeneler { get; set; }
        public int SonHayvanId { get; set; }
        public int SonKullaniciId { get; set; }
        public int SonRandevuId { get; set; }
        public int SonMuayeneId { get; set; }
    }
}

