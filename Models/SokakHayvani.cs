using System;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Sokak hayvanlarını temsil eden sınıf.
    /// HayvanBase abstract sınıfından türetilmiştir.
    /// </summary>
    public class SokakHayvani : HayvanBase
    {
        #region Private Fields

        private int _sorumluId;
        private string _sorumluAdi;
        private string _bulunduguBolge;
        private string _bulunduguAdres;
        private DateTime _bulunmaTarihi;
        private bool _kisirlastirildiMi;
        private string _kulakKupesiNo;
        private string _fizikselOzellikler;
        private bool _sahiplendirildiMi;
        private DateTime? _sahiplendirmeTarihi;
        private string _acilDurum;
        private bool _tedaviOnayliMi;
        private string _oncekiBolge;

        #endregion

        #region Public Properties

        /// <summary>
        /// Sorumlu kişinin ID'si.
        /// </summary>
        public int SorumluId
        {
            get { return _sorumluId; }
            set { _sorumluId = value; }
        }

        /// <summary>
        /// Sorumlu kişinin adı.
        /// </summary>
        public string SorumluAdi
        {
            get { return _sorumluAdi; }
            set { _sorumluAdi = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın bulunduğu bölge/mahalle.
        /// </summary>
        public string BulunduguBolge
        {
            get { return _bulunduguBolge; }
            set { _bulunduguBolge = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın bulunduğu detaylı adres.
        /// </summary>
        public string BulunduguAdres
        {
            get { return _bulunduguAdres; }
            set { _bulunduguAdres = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın bulunma/kayıt tarihi.
        /// </summary>
        public DateTime BulunmaTarihi
        {
            get { return _bulunmaTarihi; }
            set { _bulunmaTarihi = value; }
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
        /// Kulak küpesi numarası (kısırlaştırılmış sokak hayvanları için).
        /// </summary>
        public string KulakKupesiNo
        {
            get { return _kulakKupesiNo; }
            set { _kulakKupesiNo = value ?? ""; }
        }

        /// <summary>
        /// Fiziksel özellikler (tanımlayıcı işaretler).
        /// </summary>
        public string FizikselOzellikler
        {
            get { return _fizikselOzellikler; }
            set { _fizikselOzellikler = value ?? ""; }
        }

        /// <summary>
        /// Hayvanın sahiplendirilip sahiplendirilmediği.
        /// </summary>
        public bool SahiplendirildiMi
        {
            get { return _sahiplendirildiMi; }
            set { _sahiplendirildiMi = value; }
        }

        /// <summary>
        /// Sahiplendirilme tarihi.
        /// </summary>
        public DateTime? SahiplendirmeTarihi
        {
            get { return _sahiplendirmeTarihi; }
            set { _sahiplendirmeTarihi = value; }
        }

        /// <summary>
        /// Acil durum notu (varsa).
        /// </summary>
        public string AcilDurum
        {
            get { return _acilDurum; }
            set { _acilDurum = value ?? ""; }
        }

        /// <summary>
        /// Tedavinin veteriner admin tarafından onaylanıp onaylanmadığı.
        /// </summary>
        public bool TedaviOnayliMi
        {
            get { return _tedaviOnayliMi; }
            set { _tedaviOnayliMi = value; }
        }

        /// <summary>
        /// Önceki bölge (hayvan yer değiştirdiyse).
        /// </summary>
        public string OncekiBolge
        {
            get { return _oncekiBolge; }
            set { _oncekiBolge = value ?? ""; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Varsayılan yapıcı metot.
        /// </summary>
        public SokakHayvani() : base()
        {
            _bulunmaTarihi = DateTime.Now;
            _kisirlastirildiMi = false;
            _sahiplendirildiMi = false;
            _tedaviOnayliMi = false;
        }

        /// <summary>
        /// Temel bilgilerle yapıcı metot.
        /// </summary>
        public SokakHayvani(int id, string ad, string tur, string irk, int yas, string cinsiyet, int sorumluId)
            : base(id, ad, tur, irk, yas, cinsiyet)
        {
            SorumluId = sorumluId;
            _bulunmaTarihi = DateTime.Now;
            _kisirlastirildiMi = false;
            _sahiplendirildiMi = false;
            _tedaviOnayliMi = false;
        }

        /// <summary>
        /// Tam bilgilerle yapıcı metot.
        /// </summary>
        public SokakHayvani(int id, string ad, string tur, string irk, int yas, string cinsiyet,
                           int sorumluId, string bolge, string adres)
            : this(id, ad, tur, irk, yas, cinsiyet, sorumluId)
        {
            BulunduguBolge = bolge;
            BulunduguAdres = adres;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Hayvan tipini döndürür.
        /// </summary>
        public override string GetHayvanTipi()
        {
            return "Sokak Hayvanı";
        }

        /// <summary>
        /// Sokak hayvanına özel detaylı bilgileri döndürür.
        /// </summary>
        public override string DetayliBilgiGetir()
        {
            string temelBilgi = BilgileriGoster();
            string sokakBilgi = $"\n--- Sokak Hayvanı Bilgileri ---\n" +
                               $"Sorumlu: {SorumluAdi}\n" +
                               $"Bölge: {BulunduguBolge}\n" +
                               $"Adres: {BulunduguAdres}\n" +
                               $"Bulunma Tarihi: {BulunmaTarihi:dd.MM.yyyy}\n" +
                               $"Kısırlaştırıldı: {(KisirlastirildiMi ? "Evet" : "Hayır")}\n" +
                               $"Kulak Küpesi: {(string.IsNullOrEmpty(KulakKupesiNo) ? "Yok" : KulakKupesiNo)}\n" +
                               $"Tedavi Onayı: {(TedaviOnayliMi ? "Onaylı" : "Beklemede")}\n";

            if (!string.IsNullOrEmpty(FizikselOzellikler))
            {
                sokakBilgi += $"Fiziksel Özellikler: {FizikselOzellikler}\n";
            }

            if (SahiplendirildiMi)
            {
                sokakBilgi += $"Sahiplendirildi: {SahiplendirmeTarihi?.ToString("dd.MM.yyyy")}\n";
            }

            if (!string.IsNullOrEmpty(AcilDurum))
            {
                sokakBilgi += $"⚠️ ACİL DURUM: {AcilDurum}\n";
            }

            return temelBilgi + sokakBilgi;
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
        /// Hayvanı kısırlaştırıldı olarak işaretler ve kulak küpesi ekler.
        /// </summary>
        public void Kisirlastir(string kulakKupesiNo)
        {
            _kisirlastirildiMi = true;
            if (!string.IsNullOrWhiteSpace(kulakKupesiNo))
            {
                _kulakKupesiNo = kulakKupesiNo;
            }
        }

        /// <summary>
        /// Hayvanı kısırlaştırıldı olarak işaretler (parametresiz).
        /// </summary>
        public void Kisirlastir()
        {
            _kisirlastirildiMi = true;
        }

        /// <summary>
        /// Hayvanın bölgesini günceller.
        /// </summary>
        public void BolgeGuncelle(string yeniBolge, string yeniAdres)
        {
            if (!string.IsNullOrEmpty(_bulunduguBolge))
            {
                _oncekiBolge = _bulunduguBolge;
            }
            _bulunduguBolge = yeniBolge ?? "";
            _bulunduguAdres = yeniAdres ?? "";
        }

        /// <summary>
        /// Hayvanı sahiplendirildi olarak işaretler.
        /// </summary>
        public void Sahiplendir()
        {
            _sahiplendirildiMi = true;
            _sahiplendirmeTarihi = DateTime.Now;
        }

        /// <summary>
        /// Tedavi onayını değiştirir.
        /// </summary>
        public void TedaviOnayla()
        {
            _tedaviOnayliMi = true;
        }

        /// <summary>
        /// Tedavi onayını iptal eder.
        /// </summary>
        public void TedaviOnayiIptalEt()
        {
            _tedaviOnayliMi = false;
        }

        /// <summary>
        /// Acil durum belirleme.
        /// </summary>
        public void AcilDurumBelirle(string acilDurumAciklamasi)
        {
            _acilDurum = acilDurumAciklamasi ?? "";
            if (!string.IsNullOrEmpty(_acilDurum))
            {
                SaglikDurumuGuncelle("Kritik");
            }
        }

        /// <summary>
        /// Acil durumu kaldırır.
        /// </summary>
        public void AcilDurumuKaldir()
        {
            _acilDurum = "";
            SaglikDurumuGuncelle("Tedavi Altında");
        }

        /// <summary>
        /// Hayvanın sistemde kaç gündür kayıtlı olduğunu hesaplar.
        /// </summary>
        public int KayitliGunSayisi()
        {
            return (int)(DateTime.Now - BulunmaTarihi).TotalDays;
        }

        /// <summary>
        /// Sahiplenmeye uygun olup olmadığını kontrol eder.
        /// </summary>
        public bool SahiplendirmeUygunMu()
        {
            // Kısırlaştırılmış, sağlıklı ve acil durumu olmayan hayvanlar sahiplendirilebilir
            return KisirlastirildiMi &&
                   SaglikDurumu == "Sağlıklı" &&
                   string.IsNullOrEmpty(AcilDurum) &&
                   !SahiplendirildiMi;
        }

        #endregion
    }
}
