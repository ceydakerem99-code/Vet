using System;
using System.Collections.Generic;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Hayvan sahibi kullanıcısını temsil eden sınıf.
    /// Sadece kendi hayvanlarını görüntüleyebilir ve randevu alabilir.
    /// </summary>
    public class HayvanSahibi : KullaniciBase
    {
        private string _adres;
        private string _tcKimlikNo;
        private List<EvcilHayvan> _hayvanlar;
        private List<Randevu> _randevular;

        public override KullaniciRolu Rol => KullaniciRolu.HayvanSahibi;

        public string Adres
        {
            get { return _adres; }
            set { _adres = value ?? ""; }
        }

        public string TcKimlikNo
        {
            get
            {
                if (!string.IsNullOrEmpty(_tcKimlikNo) && _tcKimlikNo.Length == 11)
                    return "******* " + _tcKimlikNo.Substring(7);
                return "Belirtilmemiş";
            }
            set { if (value?.Length == 11) _tcKimlikNo = value; }
        }

        public List<EvcilHayvan> Hayvanlar => _hayvanlar;
        public List<Randevu> Randevular => _randevular;
        public int HayvanSayisi => _hayvanlar.Count;

        public HayvanSahibi() : base()
        {
            _hayvanlar = new List<EvcilHayvan>();
            _randevular = new List<Randevu>();
        }

        public HayvanSahibi(int id, string ad, string soyad, string email, string telefon, string sifre)
            : base(id, ad, soyad, email, telefon, sifre)
        {
            _hayvanlar = new List<EvcilHayvan>();
            _randevular = new List<Randevu>();
        }

        protected override void YetkileriAyarla()
        {
            YetkiEkle("KENDI_HAYVAN_GORUNTULE");
            YetkiEkle("KENDI_HAYVAN_EKLE");
            YetkiEkle("KENDI_MUAYENE_GORUNTULE");
            YetkiEkle("RANDEVU_OLUSTUR");
            YetkiEkle("KENDI_RANDEVU_GORUNTULE");
        }

        public override string AnaSayfaGetir() => "Form3";

        public override string DetayliBilgiGetir()
        {
            return $"Ad Soyad: {TamAdGetir()}\nE-posta: {Email}\nTelefon: {Telefon}\n" +
                   $"Adres: {Adres}\nHayvan Sayısı: {HayvanSayisi}";
        }

        public EvcilHayvan HayvanEkle(int id, string ad, string tur, string irk, int yas, string cinsiyet)
        {
            var hayvan = new EvcilHayvan(id, ad, tur, irk, yas, cinsiyet, this.Id);
            hayvan.SahipAdi = TamAdGetir();
            _hayvanlar.Add(hayvan);
            return hayvan;
        }

        public EvcilHayvan HayvanBul(int hayvanId)
        {
            foreach (var h in _hayvanlar)
                if (h.Id == hayvanId) return h;
            return null;
        }

        public Randevu RandevuOlustur(int id, int hayvanId, DateTime tarih, TimeSpan saat, string sikayet)
        {
            var randevu = new Randevu(id, hayvanId, this.Id, tarih, saat, sikayet);
            _randevular.Add(randevu);
            return randevu;
        }

        public int AktifRandevuSayisi()
        {
            int sayi = 0;
            foreach (var r in _randevular)
                if (r.Durum == RandevuDurumu.Bekliyor || r.Durum == RandevuDurumu.Onaylandi)
                    sayi++;
            return sayi;
        }
    }
}
