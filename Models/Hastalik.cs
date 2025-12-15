using System;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Hastalık kaydını temsil eden sınıf.
    /// Hayvanların hastalık geçmişini takip etmek için kullanılır.
    /// </summary>
    public class Hastalik
    {
        #region Private Fields

        private int _id;
        private int _hayvanId;
        private string _hastalikAdi;
        private string _aciklama;
        private DateTime _baslangicTarihi;
        private DateTime? _bitisTarihi;
        private string _tedaviYontemi;
        private string _belirtiler;
        private string _siddet; // Hafif, Orta, Şiddetli, Kritik
        private bool _iyilestiMi;
        private bool _kronikMi;

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

        public string HastalikAdi
        {
            get { return _hastalikAdi; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _hastalikAdi = value.Trim();
                else
                    throw new ArgumentException("Hastalık adı boş olamaz.");
            }
        }

        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value ?? ""; }
        }

        public DateTime BaslangicTarihi
        {
            get { return _baslangicTarihi; }
            set { _baslangicTarihi = value; }
        }

        public DateTime? BitisTarihi
        {
            get { return _bitisTarihi; }
            set { _bitisTarihi = value; }
        }

        public string TedaviYontemi
        {
            get { return _tedaviYontemi; }
            set { _tedaviYontemi = value ?? ""; }
        }

        public string Belirtiler
        {
            get { return _belirtiler; }
            set { _belirtiler = value ?? ""; }
        }

        public string Siddet
        {
            get { return _siddet; }
            set { _siddet = value ?? "Bilinmiyor"; }
        }

        public bool IyilestiMi
        {
            get { return _iyilestiMi; }
            set
            {
                _iyilestiMi = value;
                if (value && !_bitisTarihi.HasValue)
                {
                    _bitisTarihi = DateTime.Now;
                }
            }
        }

        public bool KronikMi
        {
            get { return _kronikMi; }
            set { _kronikMi = value; }
        }

        #endregion

        #region Constructors

        public Hastalik()
        {
            _baslangicTarihi = DateTime.Now;
            _iyilestiMi = false;
            _kronikMi = false;
            _siddet = "Orta";
        }

        public Hastalik(int id, int hayvanId, string hastalikAdi)
            : this()
        {
            Id = id;
            HayvanId = hayvanId;
            HastalikAdi = hastalikAdi;
        }

        public Hastalik(int id, int hayvanId, string hastalikAdi, string belirtiler, string siddet)
            : this(id, hayvanId, hastalikAdi)
        {
            Belirtiler = belirtiler;
            Siddet = siddet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Hastalık bilgilerini özet olarak döndürür.
        /// </summary>
        public string HastalikBilgisi()
        {
            string durum = IyilestiMi ? "İyileşti" : (KronikMi ? "Kronik" : "Devam Ediyor");
            string bilgi = $"Hastalık: {HastalikAdi}\n" +
                          $"Başlangıç: {BaslangicTarihi:dd.MM.yyyy}\n" +
                          $"Şiddet: {Siddet}\n" +
                          $"Durum: {durum}\n";

            if (BitisTarihi.HasValue)
            {
                bilgi += $"Bitiş: {BitisTarihi.Value:dd.MM.yyyy}\n";
            }

            return bilgi;
        }

        /// <summary>
        /// Hastalığın süresini gün olarak hesaplar.
        /// </summary>
        public int HastalikSuresiGun()
        {
            DateTime bitisTarihi = BitisTarihi ?? DateTime.Now;
            TimeSpan sure = bitisTarihi - BaslangicTarihi;
            return (int)sure.TotalDays;
        }

        /// <summary>
        /// Hastalığı iyileşti olarak işaretler.
        /// </summary>
        public void IyilestirOlarakIsaretle()
        {
            IyilestiMi = true;
        }

        /// <summary>
        /// Hastalığı kronik olarak işaretler.
        /// </summary>
        public void KronikOlarakIsaretle()
        {
            _kronikMi = true;
        }

        /// <summary>
        /// Hastalık durumunu kontrol eder.
        /// </summary>
        public string DurumKontrol()
        {
            if (IyilestiMi)
                return "İyileşti";
            else if (KronikMi)
                return "Kronik - Takip Altında";
            else if (HastalikSuresiGun() > 30)
                return "Uzun Süreli - Dikkat Gerekiyor";
            else
                return "Tedavi Devam Ediyor";
        }

        #endregion

        public override string ToString()
        {
            return $"{HastalikAdi} ({Siddet}) - {BaslangicTarihi:dd.MM.yyyy}";
        }
    }
}
