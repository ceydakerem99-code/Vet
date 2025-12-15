using System;
using System.Collections.Generic;

namespace VeterinerProjectApp.Interfaces
{
    /// <summary>
    /// Tüm hayvan türleri için temel arayüz.
    /// Bu arayüz, hayvanların ortak özelliklerini ve davranışlarını tanımlar.
    /// </summary>
    public interface IHayvan
    {
        // Properties (Özellikler)
        int Id { get; set; }
        string Ad { get; set; }
        string Tur { get; set; }
        string Irk { get; set; }
        int Yas { get; set; }
        string Cinsiyet { get; set; }
        double Agirlik { get; set; }
        string Renk { get; set; }
        DateTime KayitTarihi { get; }
        string SaglikDurumu { get; set; }

        // Methods (Metotlar)
        /// <summary>
        /// Hayvanın tüm bilgilerini string olarak döndürür.
        /// </summary>
        string BilgileriGoster();

        /// <summary>
        /// Hayvanın sağlık durumunu günceller.
        /// </summary>
        void SaglikDurumuGuncelle(string yeniDurum);

        /// <summary>
        /// Hayvanın yaşını hesaplar (kayıt tarihinden bu yana).
        /// </summary>
        int YasHesapla();
    }
}
