namespace VeterinerProjectApp.Enums
{
    /// <summary>
    /// Randevu durumlarını tanımlar.
    /// Randevunun yaşam döngüsünü takip etmek için kullanılır.
    /// </summary>
    public enum RandevuDurumu
    {
        /// <summary>
        /// Randevu oluşturuldu, onay bekliyor.
        /// </summary>
        Bekliyor = 1,

        /// <summary>
        /// Randevu veteriner tarafından onaylandı.
        /// </summary>
        Onaylandi = 2,

        /// <summary>
        /// Randevu veteriner tarafından reddedildi.
        /// </summary>
        Reddedildi = 3,

        /// <summary>
        /// Randevu tamamlandı, muayene yapıldı.
        /// </summary>
        Tamamlandi = 4,

        /// <summary>
        /// Randevu kullanıcı veya veteriner tarafından iptal edildi.
        /// </summary>
        IptalEdildi = 5,

        /// <summary>
        /// Hasta randevuya gelmedi.
        /// </summary>
        Gelmedi = 6
    }
}
