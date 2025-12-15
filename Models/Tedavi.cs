using System;
using VeterinerProjectApp.Interfaces;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Tedavi kaydını temsil eden sınıf.
    /// ITedavi arayüzünü uygular.
    /// </summary>
    public class Tedavi : ITedavi
    {
        #region Private Fields

        private int _id;
        private int _muayeneId;
        private string _tedaviAdi;
        private string _aciklama;
        private DateTime _baslangicTarihi;
        private DateTime? _bitisTarihi;
        private string _ilacBilgisi;
        private string _dozaj;
        private int _tekrarSayisi;
        private bool _aktifMi;
        private int _gunlukDozSayisi;
        private decimal _maliyet;

        #endregion

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int MuayeneId
        {
            get { return _muayeneId; }
            set { _muayeneId = value; }
        }

        public string TedaviAdi
        {
            get { return _tedaviAdi; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _tedaviAdi = value.Trim();
                else
                    throw new ArgumentException("Tedavi adı boş olamaz.");
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

        public string IlacBilgisi
        {
            get { return _ilacBilgisi; }
            set { _ilacBilgisi = value ?? ""; }
        }

        public string Dozaj
        {
            get { return _dozaj; }
            set { _dozaj = value ?? ""; }
        }

        public int TekrarSayisi
        {
            get { return _tekrarSayisi; }
            set
            {
                if (value >= 0)
                    _tekrarSayisi = value;
                else
                    throw new ArgumentException("Tekrar sayısı negatif olamaz.");
            }
        }

        public bool AktifMi
        {
            get { return _aktifMi; }
            set { _aktifMi = value; }
        }

        public int GunlukDozSayisi
        {
            get { return _gunlukDozSayisi; }
            set
            {
                if (value >= 0)
                    _gunlukDozSayisi = value;
                else
                    throw new ArgumentException("Günlük doz sayısı negatif olamaz.");
            }
        }

        public decimal Maliyet
        {
            get { return _maliyet; }
            set
            {
                if (value >= 0)
                    _maliyet = value;
                else
                    throw new ArgumentException("Maliyet negatif olamaz.");
            }
        }

        #endregion

        #region Constructors

        public Tedavi()
        {
            _baslangicTarihi = DateTime.Now;
            _aktifMi = true;
            _tekrarSayisi = 1;
            _gunlukDozSayisi = 1;
        }

        public Tedavi(int id, int muayeneId, string tedaviAdi)
            : this()
        {
            Id = id;
            MuayeneId = muayeneId;
            TedaviAdi = tedaviAdi;
        }

        public Tedavi(int id, int muayeneId, string tedaviAdi, string ilacBilgisi, string dozaj)
            : this(id, muayeneId, tedaviAdi)
        {
            IlacBilgisi = ilacBilgisi;
            Dozaj = dozaj;
        }

        #endregion

        #region ITedavi Implementation

        public string TedaviBilgisi()
        {
            string durum = AktifMi ? "Aktif" : "Tamamlandı";
            return $"Tedavi: {TedaviAdi}\n" +
                   $"İlaç: {IlacBilgisi}\n" +
                   $"Dozaj: {Dozaj}\n" +
                   $"Başlangıç: {BaslangicTarihi:dd.MM.yyyy}\n" +
                   $"Durum: {durum}";
        }

        public void TedaviyiTamamla()
        {
            _aktifMi = false;
            _bitisTarihi = DateTime.Now;
        }

        public int TedaviSuresiHesapla()
        {
            DateTime bitisTarihi = BitisTarihi ?? DateTime.Now;
            TimeSpan sure = bitisTarihi - BaslangicTarihi;
            return (int)sure.TotalDays;
        }

        public DateTime? SonrakiDozZamani()
        {
            if (!AktifMi || GunlukDozSayisi <= 0)
                return null;

            // Basit bir hesaplama: günlük doz sayısına göre saat aralığı
            int saatAraligi = 24 / GunlukDozSayisi;
            DateTime simdikiZaman = DateTime.Now;

            // Bugünkü ilk doz zamanını bul
            DateTime bugunIlkDoz = simdikiZaman.Date.AddHours(8); // Sabah 8'de başla

            for (int i = 0; i < GunlukDozSayisi; i++)
            {
                DateTime dozZamani = bugunIlkDoz.AddHours(i * saatAraligi);
                if (dozZamani > simdikiZaman)
                {
                    return dozZamani;
                }
            }

            // Bugünkü tüm dozlar alındıysa yarınki ilk doz
            return bugunIlkDoz.AddDays(1);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Tedavi süresinin bitmesine kaç gün kaldığını hesaplar.
        /// </summary>
        public int? KalanGunSayisi()
        {
            if (BitisTarihi.HasValue && AktifMi)
            {
                TimeSpan fark = BitisTarihi.Value - DateTime.Now;
                return Math.Max(0, (int)fark.TotalDays);
            }
            return null;
        }

        /// <summary>
        /// Tedavi tamamlanma yüzdesini hesaplar.
        /// </summary>
        public double TamamlanmaYuzdesi()
        {
            if (!BitisTarihi.HasValue)
                return 0;

            int toplamGun = (int)(BitisTarihi.Value - BaslangicTarihi).TotalDays;
            if (toplamGun <= 0)
                return 100;

            int gecenGun = (int)(DateTime.Now - BaslangicTarihi).TotalDays;
            double yuzde = (double)gecenGun / toplamGun * 100;

            return Math.Min(100, Math.Max(0, yuzde));
        }

        /// <summary>
        /// Tedavi detay raporunu oluşturur.
        /// </summary>
        public string DetayliRapor()
        {
            return "=== TEDAVİ DETAY RAPORU ===\n" +
                   $"Tedavi Adı: {TedaviAdi}\n" +
                   $"Açıklama: {Aciklama}\n" +
                   "---\n" +
                   $"İlaç: {IlacBilgisi}\n" +
                   $"Dozaj: {Dozaj}\n" +
                   $"Günlük Doz: {GunlukDozSayisi} kez\n" +
                   $"Toplam Tekrar: {TekrarSayisi}\n" +
                   "---\n" +
                   $"Başlangıç: {BaslangicTarihi:dd.MM.yyyy}\n" +
                   (BitisTarihi.HasValue ? $"Planlanan Bitiş: {BitisTarihi.Value:dd.MM.yyyy}\n" : "") +
                   $"Süre: {TedaviSuresiHesapla()} gün\n" +
                   $"İlerleme: %{TamamlanmaYuzdesi():F1}\n" +
                   $"Maliyet: {Maliyet:C}\n" +
                   $"Durum: {(AktifMi ? "Devam Ediyor" : "Tamamlandı")}\n" +
                   "===========================";
        }

        #endregion

        public override string ToString()
        {
            return $"{TedaviAdi} - {(AktifMi ? "Aktif" : "Tamamlandı")}";
        }
    }
}
