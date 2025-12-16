using System;
using System.Collections.Generic;
using VeterinerProjectApp.Interfaces;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Tüm kullanıcı sınıflarının türetileceği abstract temel sınıf.
    /// IKullanici arayüzünü uygular ve ortak işlevselliği sağlar.
    /// Encapsulation prensibi ile şifre gibi hassas bilgiler korunur.
    /// </summary>
    public abstract class KullaniciBase : IKullanici
    {
        #region Private Fields (Kapsüllenmiş Alanlar)

        private int _id;
        private string _ad;
        private string _soyad;
        private string _email;
        private string _telefon;
        private string _sifre;
        private readonly DateTime _kayitTarihi;
        private bool _aktifMi;
        private DateTime? _sonGirisTarihi;
        private List<string> _yetkiler;

        #endregion

        #region Public Properties (Özellikler)

        /// <summary>
        /// Kullanıcının benzersiz kimlik numarası.
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
        /// Kullanıcının adı.
        /// </summary>
        public string Ad
        {
            get { return _ad; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _ad = value.Trim();
                else
                    throw new ArgumentException("Kullanıcı adı boş olamaz.");
            }
        }

        /// <summary>
        /// Kullanıcının soyadı.
        /// </summary>
        public string Soyad
        {
            get { return _soyad; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _soyad = value.Trim();
                else
                    throw new ArgumentException("Kullanıcı soyadı boş olamaz.");
            }
        }

        /// <summary>
        /// Kullanıcının e-posta adresi.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
                    _email = value.Trim().ToLower();
                else
                    throw new ArgumentException("Geçerli bir e-posta adresi giriniz.");
            }
        }

        /// <summary>
        /// Kullanıcının telefon numarası.
        /// </summary>
        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value?.Trim() ?? ""; }
        }

        /// <summary>
        /// Kullanıcının rolü (abstract property olarak alt sınıflar tarafından belirlenir).
        /// </summary>
        public abstract KullaniciRolu Rol { get; }

        /// <summary>
        /// Kullanıcının sisteme kayıt tarihi.
        /// </summary>
        public DateTime KayitTarihi
        {
            get { return _kayitTarihi; }
        }

        /// <summary>
        /// Kullanıcının aktif olup olmadığı.
        /// </summary>
        public bool AktifMi
        {
            get { return _aktifMi; }
            set { _aktifMi = value; }
        }

        /// <summary>
        /// Kullanıcının son giriş tarihi.
        /// </summary>
        public DateTime? SonGirisTarihi
        {
            get { return _sonGirisTarihi; }
            protected set { _sonGirisTarihi = value; }
        }

        /// <summary>
        /// Kullanıcının yetki listesi.
        /// </summary>
        protected List<string> Yetkiler
        {
            get { return _yetkiler; }
            set { _yetkiler = value; }
        }

        #endregion

        #region Constructors (Yapıcılar)

        /// <summary>
        /// Varsayılan yapıcı metot.
        /// </summary>
        protected KullaniciBase()
        {
            _kayitTarihi = DateTime.Now;
            _aktifMi = true;
            _yetkiler = new List<string>();
            YetkileriAyarla();
        }

        /// <summary>
        /// Parametreli yapıcı metot.
        /// </summary>
        protected KullaniciBase(int id, string ad, string soyad, string email, string telefon, string sifre)
            : this()
        {
            Id = id;
            Ad = ad;
            Soyad = soyad;
            Email = email;
            Telefon = telefon;
            SifreBelirle(sifre);
        }

        #endregion

        #region Abstract Methods (Soyut Metotlar)

        /// <summary>
        /// Kullanıcının yetkilerini ayarlar.
        /// Her kullanıcı tipi kendi yetkilerini belirler.
        /// </summary>
        protected abstract void YetkileriAyarla();

        /// <summary>
        /// Kullanıcının ana sayfasının adını döndürür.
        /// </summary>
        public abstract string AnaSayfaGetir();

        /// <summary>
        /// Kullanıcının detaylı bilgilerini döndürür.
        /// </summary>
        public abstract string DetayliBilgiGetir();

        #endregion

        #region Virtual Methods (Sanal Metotlar)

        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştirir.
        /// </summary>
        public virtual bool GirisYap(string email, string sifre)
        {
            if (Email == email.ToLower() && SifreDogrula(sifre))
            {
                _sonGirisTarihi = DateTime.Now;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kullanıcı çıkış işlemini gerçekleştirir.
        /// </summary>
        public virtual void CikisYap()
        {
            // Gerekli temizlik işlemleri yapılabilir
        }

        /// <summary>
        /// Kullanıcının belirli bir işlemi yapma yetkisi olup olmadığını kontrol eder.
        /// </summary>
        public virtual bool YetkiKontrol(string islemAdi)
        {
            if (string.IsNullOrWhiteSpace(islemAdi))
                return false;

            return _yetkiler.Contains(islemAdi.ToUpper());
        }

        #endregion

        #region Public Methods (Genel Metotlar)

        /// <summary>
        /// Kullanıcının tam adını döndürür.
        /// </summary>
        public string TamAdGetir()
        {
            return $"{Ad} {Soyad}";
        }

        /// <summary>
        /// Kullanıcı bilgilerini özet olarak döndürür.
        /// </summary>
        public string OzetBilgiGetir()
        {
            return $"{TamAdGetir()} ({Rol})";
        }

        /// <summary>
        /// Şifre değiştirme işlemi.
        /// </summary>
        public bool SifreDegistir(string eskiSifre, string yeniSifre)
        {
            if (SifreDogrula(eskiSifre))
            {
                SifreBelirle(yeniSifre);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kullanıcıya yetki ekler.
        /// </summary>
        public void YetkiEkle(string yetkiAdi)
        {
            if (!string.IsNullOrWhiteSpace(yetkiAdi))
            {
                string yetki = yetkiAdi.ToUpper();
                if (!_yetkiler.Contains(yetki))
                {
                    _yetkiler.Add(yetki);
                }
            }
        }

        /// <summary>
        /// Kullanıcıdan yetki kaldırır.
        /// </summary>
        public void YetkiKaldir(string yetkiAdi)
        {
            if (!string.IsNullOrWhiteSpace(yetkiAdi))
            {
                _yetkiler.Remove(yetkiAdi.ToUpper());
            }
        }

        /// <summary>
        /// Kullanıcının tüm yetkilerini listeler.
        /// </summary>
        public List<string> TumYetkileriGetir()
        {
            return new List<string>(_yetkiler);
        }

        #endregion

        #region Protected Methods (Korumalı Metotlar)

        /// <summary>
        /// Şifre belirleme işlemi (kapsüllenmiş).
        /// </summary>
        protected void SifreBelirle(string sifre)
        {
            if (string.IsNullOrWhiteSpace(sifre) || sifre.Length < 4)
            {
                throw new ArgumentException("Şifre en az 4 karakter olmalıdır.");
            }
            // Basit hash işlemi (gerçek uygulamada daha güvenli bir yöntem kullanılmalı)
            _sifre = SifreHashle(sifre);
        }

        /// <summary>
        /// Şifre doğrulama işlemi (kapsüllenmiş).
        /// </summary>
        protected bool SifreDogrula(string sifre)
        {
            return _sifre == SifreHashle(sifre);
        }

        /// <summary>
        /// Basit şifre hashleme (örnek amaçlı).
        /// </summary>
        private string SifreHashle(string sifre)
        {
            // Basit bir hash simülasyonu (gerçekte güvenli hash algoritması kullanılmalı)
            int hash = 17;
            foreach (char c in sifre)
            {
                hash = hash * 31 + c;
            }
            return hash.ToString("X");
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Nesnenin string temsilini döndürür.
        /// </summary>
        public override string ToString()
        {
            return $"{TamAdGetir()} - {Rol}";
        }

        /// <summary>
        /// Nesnelerin eşitliğini kontrol eder.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is KullaniciBase other)
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
