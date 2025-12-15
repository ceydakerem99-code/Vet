using System;
using System.Collections.Generic;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Veteriner Admin (Klinik Yöneticisi) kullanıcısını temsil eden sınıf.
    /// Tam yetkili kullanıcıdır, tüm işlemleri yapabilir.
    /// </summary>
    public class VeterinerAdmin : KullaniciBase
    {
        #region Private Fields

        private string _diplomaNo;
        private string _uzmanlikAlani;
        private string _klinikAdi;
        private string _klinikAdresi;
        private int _onaylananRandevuSayisi;
        private int _yapilaMuayeneSayisi;

        #endregion

        #region Public Properties

        /// <summary>
        /// Kullanıcı rolünü döndürür.
        /// </summary>
        public override KullaniciRolu Rol
        {
            get { return KullaniciRolu.VeterinerAdmin; }
        }

        /// <summary>
        /// Veterinerin diploma numarası.
        /// </summary>
        public string DiplomaNo
        {
            get { return _diplomaNo; }
            set { _diplomaNo = value ?? ""; }
        }

        /// <summary>
        /// Veterinerin uzmanlık alanı.
        /// </summary>
        public string UzmanlikAlani
        {
            get { return _uzmanlikAlani; }
            set { _uzmanlikAlani = value ?? "Genel Veterinerlik"; }
        }

        /// <summary>
        /// Klinik adı.
        /// </summary>
        public string KlinikAdi
        {
            get { return _klinikAdi; }
            set { _klinikAdi = value ?? ""; }
        }

        /// <summary>
        /// Klinik adresi.
        /// </summary>
        public string KlinikAdresi
        {
            get { return _klinikAdresi; }
            set { _klinikAdresi = value ?? ""; }
        }

        /// <summary>
        /// Onaylanan randevu sayısı.
        /// </summary>
        public int OnaylananRandevuSayisi
        {
            get { return _onaylananRandevuSayisi; }
            private set { _onaylananRandevuSayisi = value; }
        }

        /// <summary>
        /// Yapılan muayene sayısı.
        /// </summary>
        public int YapilaMuayeneSayisi
        {
            get { return _yapilaMuayeneSayisi; }
            private set { _yapilaMuayeneSayisi = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Varsayılan yapıcı metot.
        /// </summary>
        public VeterinerAdmin() : base()
        {
            _onaylananRandevuSayisi = 0;
            _yapilaMuayeneSayisi = 0;
        }

        /// <summary>
        /// Parametreli yapıcı metot.
        /// </summary>
        public VeterinerAdmin(int id, string ad, string soyad, string email, string telefon, string sifre)
            : base(id, ad, soyad, email, telefon, sifre)
        {
            _onaylananRandevuSayisi = 0;
            _yapilaMuayeneSayisi = 0;
        }

        /// <summary>
        /// Tam bilgili yapıcı metot.
        /// </summary>
        public VeterinerAdmin(int id, string ad, string soyad, string email, string telefon, string sifre,
                              string diplomaNo, string uzmanlikAlani)
            : this(id, ad, soyad, email, telefon, sifre)
        {
            DiplomaNo = diplomaNo;
            UzmanlikAlani = uzmanlikAlani;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Yetkileri ayarlar - Full access.
        /// </summary>
        protected override void YetkileriAyarla()
        {
            // Veteriner Admin tüm yetkilere sahip
            YetkiEkle("HAYVAN_GORUNTULE");
            YetkiEkle("HAYVAN_EKLE");
            YetkiEkle("HAYVAN_DUZENLE");
            YetkiEkle("HAYVAN_SIL");
            YetkiEkle("MUAYENE_GORUNTULE");
            YetkiEkle("MUAYENE_EKLE");
            YetkiEkle("MUAYENE_DUZENLE");
            YetkiEkle("ASI_EKLE");
            YetkiEkle("ASI_DUZENLE");
            YetkiEkle("TEDAVI_EKLE");
            YetkiEkle("TEDAVI_DUZENLE");
            YetkiEkle("RANDEVU_GORUNTULE");
            YetkiEkle("RANDEVU_ONAYLA");
            YetkiEkle("RANDEVU_REDDET");
            YetkiEkle("KULLANICI_GORUNTULE");
            YetkiEkle("KULLANICI_EKLE");
            YetkiEkle("KULLANICI_DUZENLE");
            YetkiEkle("KULLANICI_SIL");
            YetkiEkle("RAPOR_OLUSTUR");
            YetkiEkle("VERI_AKTAR");
            YetkiEkle("SOKAK_HAYVANI_TEDAVI_ONAYLA");
        }

        /// <summary>
        /// Ana sayfa adını döndürür.
        /// </summary>
        public override string AnaSayfaGetir()
        {
            return "Form2"; // Admin Panel
        }

        /// <summary>
        /// Detaylı kullanıcı bilgisini döndürür.
        /// </summary>
        public override string DetayliBilgiGetir()
        {
            return $"=== VETERİNER ADMİN BİLGİLERİ ===\n" +
                   $"Ad Soyad: {TamAdGetir()}\n" +
                   $"E-posta: {Email}\n" +
                   $"Telefon: {Telefon}\n" +
                   $"Diploma No: {DiplomaNo}\n" +
                   $"Uzmanlık: {UzmanlikAlani}\n" +
                   $"Klinik: {KlinikAdi}\n" +
                   $"Adres: {KlinikAdresi}\n" +
                   $"---\n" +
                   $"Onaylanan Randevu: {OnaylananRandevuSayisi}\n" +
                   $"Yapılan Muayene: {YapilaMuayeneSayisi}\n" +
                   $"Kayıt Tarihi: {KayitTarihi:dd.MM.yyyy}\n" +
                   $"================================";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Yeni hayvan kaydı oluşturur.
        /// </summary>
        public EvcilHayvan HayvanKaydet(int hayvanId, string ad, string tur, string irk, int yas, string cinsiyet, int sahipId)
        {
            return new EvcilHayvan(hayvanId, ad, tur, irk, yas, cinsiyet, sahipId);
        }

        /// <summary>
        /// Yeni sokak hayvanı kaydı oluşturur.
        /// </summary>
        public SokakHayvani SokakHayvaniKaydet(int hayvanId, string ad, string tur, string irk, int yas, string cinsiyet, int sorumluId, string bolge)
        {
            var hayvan = new SokakHayvani(hayvanId, ad, tur, irk, yas, cinsiyet, sorumluId);
            hayvan.BulunduguBolge = bolge;
            return hayvan;
        }

        /// <summary>
        /// Muayene kaydı oluşturur.
        /// </summary>
        public Muayene MuayeneOlustur(int muayeneId, int hayvanId, string sikayet, string tani, string tedavi)
        {
            var muayene = new Muayene(muayeneId, hayvanId, this.Id, sikayet);
            muayene.Tani = tani;
            muayene.Tedavi = tedavi;
            muayene.VeterinerAdi = TamAdGetir();
            _yapilaMuayeneSayisi++;
            return muayene;
        }

        /// <summary>
        /// Randevuyu onaylar.
        /// </summary>
        public void RandevuOnayla(Randevu randevu)
        {
            if (randevu != null)
            {
                randevu.Onayla(this.Id);
                _onaylananRandevuSayisi++;
            }
        }

        /// <summary>
        /// Randevuyu reddeder.
        /// </summary>
        public void RandevuReddet(Randevu randevu, string neden)
        {
            if (randevu != null)
            {
                randevu.Reddet(neden);
            }
        }

        /// <summary>
        /// Sokak hayvanı tedavisini onaylar.
        /// </summary>
        public void SokakHayvaniTedaviOnayla(SokakHayvani hayvan)
        {
            if (hayvan != null)
            {
                hayvan.TedaviOnayla();
            }
        }

        /// <summary>
        /// Aşı kaydı oluşturur.
        /// </summary>
        public Asi AsiKaydet(int asiId, int hayvanId, string asiAdi, DateTime? sonrakiAsiTarihi)
        {
            var asi = new Asi(asiId, hayvanId, asiAdi, DateTime.Now, sonrakiAsiTarihi);
            asi.Uygulayan = TamAdGetir();
            return asi;
        }

        /// <summary>
        /// Tedavi kaydı oluşturur.
        /// </summary>
        public Tedavi TedaviKaydet(int tedaviId, int muayeneId, string tedaviAdi, string ilacBilgisi, string dozaj)
        {
            return new Tedavi(tedaviId, muayeneId, tedaviAdi, ilacBilgisi, dozaj);
        }

        #endregion
    }
}
