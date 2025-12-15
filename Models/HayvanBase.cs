using System;
using System.Collections.Generic;
using VeterinerProjectApp.Interfaces;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Tüm hayvan sınıflarının türetileceği abstract temel sınıf.
    /// IHayvan arayüzünü uygular ve ortak işlevselliği sağlar.
    /// Encapsulation prensibi ile private alanlar ve public property'ler içerir.
    /// </summary>
    public abstract class HayvanBase : IHayvan
    {
        #region Private Fields (Kapsüllenmiş Alanlar)

        private int _id;
        private string _ad;
        private string _tur;
        private string _irk;
        private int _yas;
        private string _cinsiyet;
        private double _agirlik;
        private string _renk;
        private readonly DateTime _kayitTarihi;
        private string _saglikDurumu;
        private List<Muayene> _muayeneGecmisi;
        private List<Asi> _asilar;
        private List<Hastalik> _hastalikGecmisi;

        #endregion

        #region Public Properties (Özellikler)

        /// <summary>
        /// Hayvanın benzersiz kimlik numarası.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                if (value > 0)
                    _id = value;
                else
                    throw new ArgumentException("Id pozitif bir sayı olmalıdır.");
            }
        }

        /// <summary>
        /// Hayvanın adı.
        /// </summary>
        public string Ad
        {
            get { return _ad; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _ad = value.Trim();
                else
                    throw new ArgumentException("Hayvan adı boş olamaz.");
            }
        }

        /// <summary>
        /// Hayvanın türü (Kedi, Köpek vb.).
        /// </summary>
        public string Tur
        {
            get { return _tur; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _tur = value.Trim();
                else
                    throw new ArgumentException("Hayvan türü boş olamaz.");
            }
        }

        /// <summary>
        /// Hayvanın ırkı.
        /// </summary>
        public string Irk
        {
            get { return _irk; }
            set { _irk = value?.Trim() ?? "Bilinmiyor"; }
        }

        /// <summary>
        /// Hayvanın yaşı (yıl olarak).
        /// </summary>
        public int Yas
        {
            get { return _yas; }
            set
            {
                if (value >= 0 && value <= 50)
                    _yas = value;
                else
                    throw new ArgumentException("Yaş 0-50 arasında olmalıdır.");
            }
        }

        /// <summary>
        /// Hayvanın cinsiyeti.
        /// </summary>
        public string Cinsiyet
        {
            get { return _cinsiyet; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _cinsiyet = value.Trim();
                else
                    _cinsiyet = "Bilinmiyor";
            }
        }

        /// <summary>
        /// Hayvanın ağırlığı (kg).
        /// </summary>
        public double Agirlik
        {
            get { return _agirlik; }
            set
            {
                if (value >= 0)
                    _agirlik = value;
                else
                    throw new ArgumentException("Ağırlık negatif olamaz.");
            }
        }

        /// <summary>
        /// Hayvanın rengi.
        /// </summary>
        public string Renk
        {
            get { return _renk; }
            set { _renk = value?.Trim() ?? "Belirtilmemiş"; }
        }

        /// <summary>
        /// Hayvanın sisteme kayıt tarihi (salt okunur).
        /// </summary>
        public DateTime KayitTarihi
        {
            get { return _kayitTarihi; }
        }

        /// <summary>
        /// Hayvanın güncel sağlık durumu.
        /// </summary>
        public string SaglikDurumu
        {
            get { return _saglikDurumu; }
            set { _saglikDurumu = value ?? "Bilinmiyor"; }
        }

        /// <summary>
        /// Hayvanın muayene geçmişi listesi.
        /// </summary>
        public List<Muayene> MuayeneGecmisi
        {
            get { return _muayeneGecmisi; }
            protected set { _muayeneGecmisi = value; }
        }

        /// <summary>
        /// Hayvanın aşı kayıtları listesi.
        /// </summary>
        public List<Asi> Asilar
        {
            get { return _asilar; }
            protected set { _asilar = value; }
        }

        /// <summary>
        /// Hayvanın hastalık geçmişi listesi.
        /// </summary>
        public List<Hastalik> HastalikGecmisi
        {
            get { return _hastalikGecmisi; }
            protected set { _hastalikGecmisi = value; }
        }

        #endregion

        #region Constructors (Yapıcılar)

        /// <summary>
        /// Varsayılan yapıcı metot.
        /// </summary>
        protected HayvanBase()
        {
            _kayitTarihi = DateTime.Now;
            _muayeneGecmisi = new List<Muayene>();
            _asilar = new List<Asi>();
            _hastalikGecmisi = new List<Hastalik>();
            _saglikDurumu = "Bilinmiyor";
        }

        /// <summary>
        /// Parametreli yapıcı metot.
        /// </summary>
        protected HayvanBase(int id, string ad, string tur, string irk, int yas, string cinsiyet)
            : this()
        {
            Id = id;
            Ad = ad;
            Tur = tur;
            Irk = irk;
            Yas = yas;
            Cinsiyet = cinsiyet;
        }

        #endregion

        #region Abstract Methods (Soyut Metotlar)

        /// <summary>
        /// Hayvan tipini döndürür (EvcilHayvan veya SokakHayvani).
        /// Alt sınıflar tarafından uygulanması zorunludur.
        /// </summary>
        public abstract string GetHayvanTipi();

        /// <summary>
        /// Hayvanın detaylı bilgilerini döndürür.
        /// Alt sınıflar kendi özel bilgilerini ekleyebilir.
        /// </summary>
        public abstract string DetayliBilgiGetir();

        #endregion

        #region Virtual Methods (Sanal Metotlar)

        /// <summary>
        /// Hayvanın temel bilgilerini string olarak döndürür.
        /// Alt sınıflar bu metodu override edebilir.
        /// </summary>
        public virtual string BilgileriGoster()
        {
            return $"ID: {Id}\n" +
                   $"Ad: {Ad}\n" +
                   $"Tür: {Tur}\n" +
                   $"Irk: {Irk}\n" +
                   $"Yaş: {Yas}\n" +
                   $"Cinsiyet: {Cinsiyet}\n" +
                   $"Ağırlık: {Agirlik} kg\n" +
                   $"Renk: {Renk}\n" +
                   $"Sağlık Durumu: {SaglikDurumu}\n" +
                   $"Kayıt Tarihi: {KayitTarihi:dd.MM.yyyy}";
        }

        /// <summary>
        /// Hayvanın sağlık durumunu günceller.
        /// </summary>
        public virtual void SaglikDurumuGuncelle(string yeniDurum)
        {
            if (!string.IsNullOrWhiteSpace(yeniDurum))
            {
                _saglikDurumu = yeniDurum;
            }
        }

        /// <summary>
        /// Hayvanın yaşını hesaplar.
        /// </summary>
        public virtual int YasHesapla()
        {
            return _yas;
        }

        #endregion

        #region Public Methods (Genel Metotlar)

        /// <summary>
        /// Hayvanın muayene geçmişine yeni bir muayene ekler.
        /// </summary>
        public void MuayeneEkle(Muayene muayene)
        {
            if (muayene != null)
            {
                _muayeneGecmisi.Add(muayene);
            }
        }

        /// <summary>
        /// Hayvanın aşı listesine yeni bir aşı ekler.
        /// </summary>
        public void AsiEkle(Asi asi)
        {
            if (asi != null)
            {
                _asilar.Add(asi);
            }
        }

        /// <summary>
        /// Hayvanın hastalık geçmişine yeni bir hastalık ekler.
        /// </summary>
        public void HastalikEkle(Hastalik hastalik)
        {
            if (hastalik != null)
            {
                _hastalikGecmisi.Add(hastalik);
            }
        }

        /// <summary>
        /// Hayvanın toplam muayene sayısını döndürür.
        /// </summary>
        public int ToplamMuayeneSayisi()
        {
            return _muayeneGecmisi.Count;
        }

        /// <summary>
        /// Hayvanın son muayene tarihini döndürür.
        /// </summary>
        public DateTime? SonMuayeneTarihi()
        {
            if (_muayeneGecmisi.Count > 0)
            {
                return _muayeneGecmisi[_muayeneGecmisi.Count - 1].MuayeneTarihi;
            }
            return null;
        }

        /// <summary>
        /// Hayvanın aktif tedavileri olup olmadığını kontrol eder.
        /// </summary>
        public bool AktifTedaviVarMi()
        {
            foreach (var hastalik in _hastalikGecmisi)
            {
                if (!hastalik.IyilestiMi)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sonraki aşı tarihini kontrol eder.
        /// </summary>
        public DateTime? SonrakiAsiTarihi()
        {
            DateTime? enYakinTarih = null;
            foreach (var asi in _asilar)
            {
                if (asi.SonrakiAsiTarihi.HasValue && asi.SonrakiAsiTarihi > DateTime.Now)
                {
                    if (!enYakinTarih.HasValue || asi.SonrakiAsiTarihi < enYakinTarih)
                    {
                        enYakinTarih = asi.SonrakiAsiTarihi;
                    }
                }
            }
            return enYakinTarih;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Nesnenin string temsilini döndürür.
        /// </summary>
        public override string ToString()
        {
            return $"{Ad} ({Tur} - {Irk})";
        }

        /// <summary>
        /// Nesnelerin eşitliğini kontrol eder.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is HayvanBase other)
            {
                return this.Id == other.Id;
            }
            return false;
        }

        /// <summary>
        /// Hash kodunu döndürür.
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
