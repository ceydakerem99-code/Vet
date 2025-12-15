using System;
using System.Collections.Generic;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Sahipli evcil hayvanları temsil eden sınıf.
    /// HayvanBase abstract sınıfından türetilmiştir.
    /// </summary>
    public class EvcilHayvan : HayvanBase
    {
        #region Private Fields

        private int _sahipId;
        private string _sahipAdi;
        private string _chipNumarasi;
        private bool _kisirlastirildiMi;
        private bool _sigortaliMi;
        private string _sigortaNumarasi;
        private DateTime? _sonKontrolTarihi;
        private string _ozelNotlar;
        private string _fotografYolu;

        #endregion

        #region Public Properties

        /// <summary>
        /// Hayvan sahibinin ID'si.
        /// </summary>
        public int SahipId
        {
            get { return _sahipId; }
            set
            {
                if (value >= 0)
                    _sahipId = value;
                else
                    throw new ArgumentException("Sahip ID negatif olamaz.");
            }
        }

        /// <summary>
        /// Hayvan sahibinin adı.
        /// </summary>
        public string SahipAdi
        {
            get { return _sahipAdi; }
            set { _sahipAdi = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın mikroçip numarası.
        /// </summary>
        public string ChipNumarasi
        {
            get { return _chipNumarasi; }
            set { _chipNumarasi = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın kısırlaştırılıp kısırlaştırılmadığı.
        /// </summary>
        public bool KisirlastirildiMi
        {
            get { return _kisirlastirildiMi; }
            set { _kisirlastirildiMi = value; }
        }

        /// <summary>
        /// Hayvanın sigortalı olup olmadığı.
        /// </summary>
        public bool SigortaliMi
        {
            get { return _sigortaliMi; }
            set { _sigortaliMi = value; }
        }

        /// <summary>
        /// Sigorta poliçe numarası.
        /// </summary>
        public string SigortaNumarasi
        {
            get { return _sigortaNumarasi; }
            set { _sigortaNumarasi = value ?? ""; }
        }

        /// <summary>
        /// Son kontrol tarihi.
        /// </summary>
        public DateTime? SonKontrolTarihi
        {
            get { return _sonKontrolTarihi; }
            set { _sonKontrolTarihi = value; }
        }

        /// <summary>
        /// Özel notlar (alerjiler, hassasiyetler vb.).
        /// </summary>
        public string OzelNotlar
        {
            get { return _ozelNotlar; }
            set { _ozelNotlar = value ?? ""; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Varsayılan yapıcı metot.
        /// </summary>
        public EvcilHayvan() : base()
        {
            _kisirlastirildiMi = false;
            _sigortaliMi = false;
        }

        /// <summary>
        /// Temel bilgilerle yapıcı metot.
        /// </summary>
        public EvcilHayvan(int id, string ad, string tur, string irk, int yas, string cinsiyet, int sahipId)
            : base(id, ad, tur, irk, yas, cinsiyet)
        {
            SahipId = sahipId;
            _kisirlastirildiMi = false;
            _sigortaliMi = false;
        }

        /// <summary>
        /// Tam bilgilerle yapıcı metot.
        /// </summary>
        public EvcilHayvan(int id, string ad, string tur, string irk, int yas, string cinsiyet,
                          int sahipId, string sahipAdi, string chipNumarasi)
            : this(id, ad, tur, irk, yas, cinsiyet, sahipId)
        {
            SahipAdi = sahipAdi;
            ChipNumarasi = chipNumarasi;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Hayvan tipini döndürür.
        /// </summary>
        public override string GetHayvanTipi()
        {
            return "Evcil Hayvan";
        }

        /// <summary>
        /// Evcil hayvana özel detaylı bilgileri döndürür.
        /// </summary>
        public override string DetayliBilgiGetir()
        {
            string temelBilgi = BilgileriGoster();
            string evcilBilgi = $"\n--- Evcil Hayvan Bilgileri ---\n" +
                               $"Sahip: {SahipAdi}\n" +
                               $"Chip No: {(string.IsNullOrEmpty(ChipNumarasi) ? "Yok" : ChipNumarasi)}\n" +
                               $"Kısırlaştırıldı: {(KisirlastirildiMi ? "Evet" : "Hayır")}\n" +
                               $"Sigortalı: {(SigortaliMi ? "Evet - " + SigortaNumarasi : "Hayır")}\n";

            if (SonKontrolTarihi.HasValue)
            {
                evcilBilgi += $"Son Kontrol: {SonKontrolTarihi.Value:dd.MM.yyyy}\n";
            }

            if (!string.IsNullOrEmpty(OzelNotlar))
            {
                evcilBilgi += $"Özel Notlar: {OzelNotlar}\n";
            }

            return temelBilgi + evcilBilgi;
        }

        /// <summary>
        /// Hayvan bilgilerini gösterir (override).
        /// </summary>
        public override string BilgileriGoster()
        {
            return base.BilgileriGoster() + $"\nTip: {GetHayvanTipi()}";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Hayvanı kısırlaştırıldı olarak işaretler.
        /// </summary>
        public void Kisirlastir()
        {
            _kisirlastirildiMi = true;
        }

        /// <summary>
        /// Hayvana sigorta ekler.
        /// </summary>
        public void SigortaEkle(string policeNo)
        {
            if (!string.IsNullOrWhiteSpace(policeNo))
            {
                _sigortaliMi = true;
                _sigortaNumarasi = policeNo;
            }
        }

        /// <summary>
        /// Sigortayı iptal eder.
        /// </summary>
        public void SigortaIptalEt()
        {
            _sigortaliMi = false;
            _sigortaNumarasi = "";
        }

        /// <summary>
        /// Kontrol tarihini günceller.
        /// </summary>
        public void KontrolTarihiGuncelle()
        {
            _sonKontrolTarihi = DateTime.Now;
        }

        /// <summary>
        /// Chip numarası kaydetme.
        /// </summary>
        public void ChipKaydet(string chipNo)
        {
            if (!string.IsNullOrWhiteSpace(chipNo))
            {
                _chipNumarasi = chipNo;
            }
        }

        /// <summary>
        /// Sonraki kontrole kaç gün kaldığını hesaplar (yıllık kontrol varsayımıyla).
        /// </summary>
        public int? SonrakiKontroleKalanGun()
        {
            if (SonKontrolTarihi.HasValue)
            {
                DateTime sonrakiKontrol = SonKontrolTarihi.Value.AddYears(1);
                return (int)(sonrakiKontrol - DateTime.Now).TotalDays;
            }
            return null;
        }

        /// <summary>
        /// Kontrol gerekli mi kontrol eder (6 aydan fazla geçtiyse).
        /// </summary>
        public bool KontrolGerekliMi()
        {
            if (!SonKontrolTarihi.HasValue)
                return true;

            return (DateTime.Now - SonKontrolTarihi.Value).TotalDays > 180;
        }

        #endregion
    }
}
