using System;
using VeterinerProjectApp.Models;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Services
{
    /// <summary>
    /// Oturum yönetimi servisi - Singleton Pattern kullanır.
    /// Aktif kullanıcı oturumunu yönetir ve rol bazlı erişim kontrolü sağlar.
    /// </summary>
    public class OturumYoneticisi
    {
        private static OturumYoneticisi _instance;
        private static readonly object _lock = new object();
        
        private KullaniciBase _aktifKullanici;
        private DateTime? _girisZamani;
        private bool _oturumAktifMi;

        // Singleton Instance
        public static OturumYoneticisi Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new OturumYoneticisi();
                        }
                    }
                }
                return _instance;
            }
        }

        // Private constructor
        private OturumYoneticisi()
        {
            _oturumAktifMi = false;
        }

        #region Properties

        public KullaniciBase AktifKullanici => _aktifKullanici;
        public bool OturumAktifMi => _oturumAktifMi;
        public DateTime? GirisZamani => _girisZamani;

        public KullaniciRolu? AktifRol
        {
            get
            {
                if (_aktifKullanici != null)
                    return _aktifKullanici.Rol;
                return null;
            }
        }

        #endregion

        #region Oturum İşlemleri

        /// <summary>
        /// Kullanıcı girişi yapar.
        /// </summary>
        public bool GirisYap(KullaniciBase kullanici, string email, string sifre)
        {
            if (kullanici == null)
                return false;

            if (kullanici.GirisYap(email, sifre))
            {
                _aktifKullanici = kullanici;
                _girisZamani = DateTime.Now;
                _oturumAktifMi = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kullanıcı çıkışı yapar.
        /// </summary>
        public void CikisYap()
        {
            if (_aktifKullanici != null)
            {
                _aktifKullanici.CikisYap();
            }
            _aktifKullanici = null;
            _girisZamani = null;
            _oturumAktifMi = false;
        }

        /// <summary>
        /// Demo modunda giriş yapar (şifresiz).
        /// </summary>
        public void DemoGiris(KullaniciBase kullanici)
        {
            _aktifKullanici = kullanici;
            _girisZamani = DateTime.Now;
            _oturumAktifMi = true;
        }

        

        #region Yetki Kontrolleri

        /// <summary>
        /// Aktif kullanıcının belirli bir yetkiye sahip olup olmadığını kontrol eder.
        /// </summary>
        public bool YetkiVarMi(string yetkiAdi)
        {
            if (_aktifKullanici == null)
                return false;
            return _aktifKullanici.YetkiKontrol(yetkiAdi);
        }

        /// <summary>
        /// Aktif kullanıcının veteriner admin olup olmadığını kontrol eder.
        /// </summary>
        public bool VeterinerAdminMi()
        {
            return _aktifKullanici != null && _aktifKullanici.Rol == KullaniciRolu.VeterinerAdmin;
        }

        /// <summary>
        /// Aktif kullanıcının hayvan sahibi olup olmadığını kontrol eder.
        /// </summary>
        public bool HayvanSahibiMi()
        {
            return _aktifKullanici != null && _aktifKullanici.Rol == KullaniciRolu.HayvanSahibi;
        }

        /// <summary>
        /// Aktif kullanıcının sokak hayvanı sorumlusu olup olmadığını kontrol eder.
        /// </summary>
        public bool SokakHayvaniSorumlusuMu()
        {
            return _aktifKullanici != null && _aktifKullanici.Rol == KullaniciRolu.SokakHayvaniSorumlusu;
        }

        /// <summary>
        /// Aktif kullanıcıyı VeterinerAdmin olarak döndürür.
        /// </summary>
        public VeterinerAdmin VeterinerAdminOlarakAl()
        {
            return _aktifKullanici as VeterinerAdmin;
        }

        /// <summary>
        /// Aktif kullanıcıyı HayvanSahibi olarak döndürür.
        /// </summary>
        public HayvanSahibi HayvanSahibiOlarakAl()
        {
            return _aktifKullanici as HayvanSahibi;
        }

        /// <summary>
        /// Aktif kullanıcıyı SokakHayvaniSorumlusu olarak döndürür.
        /// </summary>
        public SokakHayvaniSorumlusu SorumluOlarakAl()
        {
            return _aktifKullanici as SokakHayvaniSorumlusu;
        }

        #endregion

        #region Yardımcı Metotlar

        /// <summary>
        /// Aktif kullanıcının ana sayfasını döndürür.
        /// </summary>
        public string AnaSayfaGetir()
        {
            if (_aktifKullanici != null)
                return _aktifKullanici.AnaSayfaGetir();
            return "Form1";
        }

        #endregion // kod karmaşasını engellemek için
    }
}
