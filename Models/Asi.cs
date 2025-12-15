using System;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Aşı kaydını temsil eden sınıf.
    /// Hayvanların aşı geçmişini takip etmek için kullanılır.
    /// </summary>
    public class Asi
    {
        #region Private Fields

        private int _id;
        private int _hayvanId;
        private string _asiAdi;
        private DateTime _asiTarihi;
        private DateTime? _sonrakiAsiTarihi;
        private string _uygulayan;
        private string _marka;
        private string _seriNo;
        private string _notlar;
        private bool _hatirlatmaAktifMi;

        #endregion

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int HayvanId
        {
            get { return _hayvanId; }
            set { _hayvanId = value; }
        }

        public string AsiAdi
        {
            get { return _asiAdi; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _asiAdi = value.Trim();
                else
                    throw new ArgumentException("Aşı adı boş olamaz.");
            }
        }

        public DateTime AsiTarihi
        {
            get { return _asiTarihi; }
            set { _asiTarihi = value; }
        }

        public DateTime? SonrakiAsiTarihi
        {
            get { return _sonrakiAsiTarihi; }
            set { _sonrakiAsiTarihi = value; }
        }

        public string Uygulayan
        {
            get { return _uygulayan; }
            set { _uygulayan = value ?? ""; }
        }

        public string Marka
        {
            get { return _marka; }
            set { _marka = value ?? ""; }
        }

        public string SeriNo
        {
            get { return _seriNo; }
            set { _seriNo = value ?? ""; }
        }

        public string Notlar
        {
            get { return _notlar; }
            set { _notlar = value ?? ""; }
        }

        public bool HatirlatmaAktifMi
        {
            get { return _hatirlatmaAktifMi; }
            set { _hatirlatmaAktifMi = value; }
        }

        #endregion

        #region Constructors

        public Asi()
        {
            _asiTarihi = DateTime.Now;
            _hatirlatmaAktifMi = true;
        }

        public Asi(int id, int hayvanId, string asiAdi, DateTime asiTarihi)
            : this()
        {
            Id = id;
            HayvanId = hayvanId;
            AsiAdi = asiAdi;
            AsiTarihi = asiTarihi;
        }

        public Asi(int id, int hayvanId, string asiAdi, DateTime asiTarihi, DateTime? sonrakiAsiTarihi)
            : this(id, hayvanId, asiAdi, asiTarihi)
        {
            SonrakiAsiTarihi = sonrakiAsiTarihi;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Aşı bilgilerini özet olarak döndürür.
        /// </summary>
        public string AsiBilgisi()
        {
            string bilgi = $"Aşı: {AsiAdi}\n" +
                          $"Uygulama Tarihi: {AsiTarihi:dd.MM.yyyy}\n" +
                          $"Marka: {Marka}\n";

            if (SonrakiAsiTarihi.HasValue)
            {
                bilgi += $"Sonraki Aşı: {SonrakiAsiTarihi.Value:dd.MM.yyyy}\n";
            }

            return bilgi;
        }

        /// <summary>
        /// Sonraki aşı tarihine kaç gün kaldığını hesaplar.
        /// </summary>
        public int? SonrakiAsiyeKalanGun()
        {
            if (SonrakiAsiTarihi.HasValue)
            {
                TimeSpan fark = SonrakiAsiTarihi.Value - DateTime.Now;
                return (int)fark.TotalDays;
            }
            return null;
        }

        /// <summary>
        /// Aşı hatırlatmasının gerekip gerekmediğini kontrol eder.
        /// (Sonraki aşıya 7 gün veya daha az kaldıysa)
        /// </summary>
        public bool HatirlatmaGerekliMi()
        {
            if (!HatirlatmaAktifMi || !SonrakiAsiTarihi.HasValue)
                return false;

            int? kalanGun = SonrakiAsiyeKalanGun();
            return kalanGun.HasValue && kalanGun.Value <= 7 && kalanGun.Value >= 0;
        }

        /// <summary>
        /// Aşının süresinin geçip geçmediğini kontrol eder.
        /// </summary>
        public bool SuresiGectiMi()
        {
            if (SonrakiAsiTarihi.HasValue)
            {
                return SonrakiAsiTarihi.Value < DateTime.Now;
            }
            return false;
        }

        #endregion

        public override string ToString()
        {
            return $"{AsiAdi} - {AsiTarihi:dd.MM.yyyy}";
        }
    }
}
