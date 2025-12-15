using System;

namespace VeterinerProjectApp.Interfaces
{
    /// <summary>
    /// Muayene kayıtları için arayüz.
    /// Hayvanların muayene geçmişini takip etmek için kullanılır.
    /// </summary>
    public interface IMuayene
    {
        // Properties (Özellikler)
        int Id { get; set; }
        int HayvanId { get; set; }
        int VeterinerId { get; set; }
        DateTime MuayeneTarihi { get; set; }
        string Sikayet { get; set; }
        string Tani { get; set; }
        string Tedavi { get; set; }
        string Notlar { get; set; }
        decimal Ucret { get; set; }
        bool TamamlandiMi { get; set; }

        // Methods (Metotlar)
        /// <summary>
        /// Muayene bilgilerini özet olarak döndürür.
        /// </summary>
        string MuayeneBilgisi();

        /// <summary>
        /// Muayeneyi tamamlandı olarak işaretler.
        /// </summary>
        void MuayeneyiTamamla();

        /// <summary>
        /// Muayene raporunu oluşturur.
        /// </summary>
        string RaporOlustur();
    }
}
