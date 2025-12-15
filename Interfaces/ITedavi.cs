using System;

namespace VeterinerProjectApp.Interfaces
{
    /// <summary>
    /// Tedavi işlemleri için arayüz.
    /// Hayvanların tedavi süreçlerini yönetmek için kullanılır.
    /// </summary>
    public interface ITedavi
    {
        // Properties (Özellikler)
        int Id { get; set; }
        int MuayeneId { get; set; }
        string TedaviAdi { get; set; }
        string Aciklama { get; set; }
        DateTime BaslangicTarihi { get; set; }
        DateTime? BitisTarihi { get; set; }
        string IlacBilgisi { get; set; }
        string Dozaj { get; set; }
        int TekrarSayisi { get; set; }
        bool AktifMi { get; set; }

        // Methods (Metotlar)
        /// <summary>
        /// Tedavi bilgilerini özet olarak döndürür.
        /// </summary>
        string TedaviBilgisi();

        /// <summary>
        /// Tedaviyi tamamlandı olarak işaretler.
        /// </summary>
        void TedaviyiTamamla();

        /// <summary>
        /// Tedavi süresini gün olarak hesaplar.
        /// </summary>
        int TedaviSuresiHesapla();

        /// <summary>
        /// Sonraki ilaç alma zamanını hesaplar.
        /// </summary>
        DateTime? SonrakiDozZamani();
    }
}
