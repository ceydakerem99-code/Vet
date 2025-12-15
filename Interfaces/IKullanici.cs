using System;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Interfaces
{
    /// <summary>
    /// Tüm kullanıcı türleri için temel arayüz.
    /// Rol tabanlı yetkilendirme sistemini destekler.
    /// </summary>
    public interface IKullanici
    {
        // Properties (Özellikler)
        int Id { get; set; }
        string Ad { get; set; }
        string Soyad { get; set; }
        string Email { get; set; }
        string Telefon { get; set; }
        KullaniciRolu Rol { get; }
        DateTime KayitTarihi { get; }
        bool AktifMi { get; set; }

        // Methods (Metotlar)
        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştirir.
        /// </summary>
        bool GirisYap(string email, string sifre);

        /// <summary>
        /// Kullanıcı çıkış işlemini gerçekleştirir.
        /// </summary>
        void CikisYap();

        /// <summary>
        /// Kullanıcının tam adını döndürür.
        /// </summary>
        string TamAdGetir();

        /// <summary>
        /// Kullanıcının belirli bir işlemi yapma yetkisi olup olmadığını kontrol eder.
        /// </summary>
        bool YetkiKontrol(string islemAdi);
    }
}
