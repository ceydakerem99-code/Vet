using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp.Services
{
    /// <summary>
    /// Veri yönetimi servisi - Singleton Pattern kullanır.
    /// JSON dosyalarına veri kaydetme/okuma işlemleri yapar.
    /// </summary>
    public class VeriYoneticisi
    {
        private static VeriYoneticisi _instance;
        private static readonly object _lock = new object();
        
        private readonly string _veriKlasoru;
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

        // Singleton Instance
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

        // Private constructor
        private VeriYoneticisi()
        {
            _veriKlasoru = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Veriler");
            
            if (!Directory.Exists(_veriKlasoru))
                Directory.CreateDirectory(_veriKlasoru);

            _evcilHayvanlar = new List<EvcilHayvan>();
            _sokakHayvanlari = new List<SokakHayvani>();
            _veterinerler = new List<VeterinerAdmin>();
            _hayvanSahipleri = new List<HayvanSahibi>();
            _sorumlular = new List<SokakHayvaniSorumlusu>();
            _randevular = new List<Randevu>();
            _muayeneler = new List<Muayene>();
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
            }
        }

        public void SokakHayvaniEkle(SokakHayvani hayvan)
        {
            if (hayvan != null && !_sokakHayvanlari.Contains(hayvan))
            {
                _sokakHayvanlari.Add(hayvan);
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
                return _evcilHayvanlar.Remove(hayvan);
            return false;
        }

        #endregion

        #region Kullanıcı İşlemleri

        public void VeterinerEkle(VeterinerAdmin veteriner)
        {
            if (veteriner != null)
                _veterinerler.Add(veteriner);
        }

        public void HayvanSahibiEkle(HayvanSahibi sahip)
        {
            if (sahip != null)
                _hayvanSahipleri.Add(sahip);
        }

        public void SorumluEkle(SokakHayvaniSorumlusu sorumlu)
        {
            if (sorumlu != null)
                _sorumlular.Add(sorumlu);
        }

        #endregion

        #region Randevu İşlemleri

        public void RandevuEkle(Randevu randevu)
        {
            if (randevu != null)
                _randevular.Add(randevu);
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
                _muayeneler.Add(muayene);
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
    }
}
