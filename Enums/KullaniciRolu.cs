namespace VeterinerProjectApp.Enums
{
    /// <summary>
    /// Sistemdeki kullanıcı rollerini tanımlar.
    /// Her rol farklı yetkilere sahiptir.
    /// </summary>
    public enum KullaniciRolu
    {
        /// <summary>
        /// Tam yetkili klinik yöneticisi.
        /// Tüm kayıtları görüntüleyebilir, ekleyebilir, silebilir ve güncelleyebilir.
        /// </summary>
        VeterinerAdmin = 1,

        /// <summary>
        /// Hayvan sahibi kullanıcı.
        /// Sadece kendi hayvanlarını görüntüleyebilir ve randevu alabilir.
        /// </summary>
        HayvanSahibi = 2,

        /// <summary>
        /// Sokak hayvanı sorumlusu.
        /// Sokak hayvanlarını kaydedebilir ve takip edebilir.
        /// </summary>
        SokakHayvaniSorumlusu = 3
    }
}
