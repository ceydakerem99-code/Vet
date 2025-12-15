using System;
using System.Collections.Generic;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Sokak hayvanı sorumlusu kullanıcısını temsil eden sınıf.
    /// Sokak hayvanı kayıtları oluşturur ve takip eder.
    /// </summary>
    public class SokakHayvaniSorumlusu : KullaniciBase
    {
        private string _sorumluBolge;
        private string _adres;
        private List<SokakHayvani> _takipEdilenHayvanlar;
        private int _kaydettigiHayvanSayisi;

        public override KullaniciRolu Rol => KullaniciRolu.SokakHayvaniSorumlusu;

        public string SorumluBolge
        {
            get { return _sorumluBolge; }
            set { _sorumluBolge = value ?? ""; }
        }

        public string Adres
        {
            get { return _adres; }
            set { _adres = value ?? ""; }
        }

        public List<SokakHayvani> TakipEdilenHayvanlar => _takipEdilenHayvanlar;
        public int KaydettigiHayvanSayisi => _kaydettigiHayvanSayisi;

        public SokakHayvaniSorumlusu() : base()
        {
            _takipEdilenHayvanlar = new List<SokakHayvani>();
            _kaydettigiHayvanSayisi = 0;
        }

        public SokakHayvaniSorumlusu(int id, string ad, string soyad, string email, string telefon, string sifre)
            : base(id, ad, soyad, email, telefon, sifre)
        {
            _takipEdilenHayvanlar = new List<SokakHayvani>();
            _kaydettigiHayvanSayisi = 0;
        }

        public SokakHayvaniSorumlusu(int id, string ad, string soyad, string email, string telefon, 
                                     string sifre, string sorumluBolge)
            : this(id, ad, soyad, email, telefon, sifre)
        {
            SorumluBolge = sorumluBolge;
        }

        protected override void YetkileriAyarla()
        {
            YetkiEkle("SOKAK_HAYVANI_KAYDET");
            YetkiEkle("SOKAK_HAYVANI_GORUNTULE");
            YetkiEkle("SOKAK_HAYVANI_DUZENLE");
            YetkiEkle("SOKAK_HAYVANI_KONUM_GUNCELLE");
            YetkiEkle("ACIL_DURUM_BILDIR");
        }

        public override string AnaSayfaGetir() => "Form4";

        public override string DetayliBilgiGetir()
        {
            return $"Ad Soyad: {TamAdGetir()}\nE-posta: {Email}\nTelefon: {Telefon}\n" +
                   $"Sorumlu Bölge: {SorumluBolge}\nTakip Edilen Hayvan: {_takipEdilenHayvanlar.Count}";
        }

        /// <summary>
        /// Yeni sokak hayvanı kaydeder.
        /// </summary>
        public SokakHayvani HayvanKaydet(int id, string ad, string tur, string irk, int yas, 
                                         string cinsiyet, string bolge, string adres)
        {
            var hayvan = new SokakHayvani(id, ad, tur, irk, yas, cinsiyet, this.Id, bolge, adres);
            hayvan.SorumluAdi = TamAdGetir();
            _takipEdilenHayvanlar.Add(hayvan);
            _kaydettigiHayvanSayisi++;
            return hayvan;
        }

        /// <summary>
        /// Hayvan bulur.
        /// </summary>
        public SokakHayvani HayvanBul(int hayvanId)
        {
            foreach (var h in _takipEdilenHayvanlar)
                if (h.Id == hayvanId) return h;
            return null;
        }

        /// <summary>
        /// Hayvanın konumunu günceller.
        /// </summary>
        public void KonumGuncelle(int hayvanId, string yeniBolge, string yeniAdres)
        {
            var hayvan = HayvanBul(hayvanId);
            if (hayvan != null)
            {
                hayvan.BolgeGuncelle(yeniBolge, yeniAdres);
            }
        }

        /// <summary>
        /// Acil durum bildirir.
        /// </summary>
        public void AcilDurumBildir(int hayvanId, string aciklama)
        {
            var hayvan = HayvanBul(hayvanId);
            if (hayvan != null)
            {
                hayvan.AcilDurumBelirle(aciklama);
            }
        }

        /// <summary>
        /// Tedavi onayı bekleyen hayvanları listeler.
        /// </summary>
        public List<SokakHayvani> OnayBekleyenler()
        {
            var liste = new List<SokakHayvani>();
            foreach (var h in _takipEdilenHayvanlar)
            {
                if (!h.TedaviOnayliMi && h.SaglikDurumu != "Sağlıklı")
                    liste.Add(h);
            }
            return liste;
        }

        /// <summary>
        /// Kısırlaştırılmamış hayvanları listeler.
        /// </summary>
        public List<SokakHayvani> KisirlastirilmamisHayvanlar()
        {
            var liste = new List<SokakHayvani>();
            foreach (var h in _takipEdilenHayvanlar)
            {
                if (!h.KisirlastirildiMi)
                    liste.Add(h);
            }
            return liste;
        }
    }
}
