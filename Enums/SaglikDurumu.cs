namespace VeterinerProjectApp.Enums
{
    /// <summary>
    /// Hayvanın sağlık durumunu tanımlar.
    /// Muayene ve tedavi süreçlerinde kullanılır.
    /// </summary>
    public enum SaglikDurumu
    {
        /// <summary>
        /// Hayvan sağlıklı, herhangi bir sorun yok.
        /// </summary>
        Saglikli = 1,

        /// <summary>
        /// Hayvan tedavi altında, aktif tedavi süreci devam ediyor.
        /// </summary>
        TedaviAltinda = 2,

        /// <summary>
        /// Hayvanın durumu kritik, acil müdahale gerekiyor.
        /// </summary>
        Kritik = 3,

        /// <summary>
        /// Hayvan iyileşti, tedavi tamamlandı.
        /// </summary>
        Iyilesti = 4,

        /// <summary>
        /// Kronik hastalık, sürekli takip gerekiyor.
        /// </summary>
        KronikHasta = 5,

        /// <summary>
        /// Karantina altında.
        /// </summary>
        Karantina = 6,

        /// <summary>
        /// Operasyon sonrası iyileşme sürecinde.
        /// </summary>
        OperasyonSonrasi = 7
    }
}
