using System;
using System.IO;
using System.Text;

namespace VeterinerProjectApp.Services
{
    /// <summary>
    /// SMS ve E-posta bildirim servisi
    /// Gerçek SMS/E-posta göndermek yerine log dosyasına kaydeder.
    /// </summary>
    public class BildirimServisi
    {
        private static BildirimServisi _instance; //olusturulan nesneyi içinde tutar
        private static readonly object _lock = new object(); 
        private readonly string _logDosyasi; //log dosyasının yolu

        public static BildirimServisi Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BildirimServisi();
                        }
                    }
                }
                return _instance;
            }
        }

        private BildirimServisi()
        {
            _logDosyasi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BildirimLog.txt");
        }

        /// <summary>
        /// SMS bildirimi gönderir (simülasyon - log'a yazar).
        /// </summary>
        public void SmsGonder(string telefon, string mesaj)
        {
            string log = $"[SMS] {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n" +
                         $"Alıcı: {telefon}\n" +
                         $"Mesaj: {mesaj}\n" +
                         $"Durum: BAŞARILI (Simülasyon)\n" +
                         "────────────────────────────────────────\n";
            
            LogYaz(log);
        }

        /// <summary>
        /// E-posta bildirimi gönderir (simülasyon - log'a yazar).
        /// </summary>
        public void EmailGonder(string email, string konu, string icerik)
        {
            string log = $"[E-POSTA] {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n" +
                         $"Alıcı: {email}\n" +
                         $"Konu: {konu}\n" +
                         $"İçerik:\n{icerik}\n" +
                         $"Durum: BAŞARILI (Simülasyon)\n" +
                         "────────────────────────────────────────\n";
            
            LogYaz(log);
        }

        /// <summary>
        /// Randevu onay bildirimi gönderir (basit versiyon).
        /// </summary>
        public void RandevuOnayBildirimi(string telefon, DateTime randevuTarihi)
        {
            string smsMesaj = $"Randevunuz onaylanmıştır. Tarih: {randevuTarihi:dd.MM.yyyy}. Veteriner Klinik";
            SmsGonder(telefon, smsMesaj);
        }

        /// <summary>
        /// Randevu onay bildirimi gönderir (detaylı versiyon).
        /// </summary>
        public void RandevuOnayBildirimi(string telefon, string email, DateTime randevuTarihi, string hayvanAdi)
        {
            string smsMesaj = $"Randevunuz onaylanmıştır. Tarih: {randevuTarihi:dd.MM.yyyy}, Hayvan: {hayvanAdi}. Veteriner Klinik";
            SmsGonder(telefon, smsMesaj);

            string emailIcerik = $"Sayın Müşterimiz,\n\n" +
                                 $"{hayvanAdi} isimli evcil hayvanınız için oluşturduğunuz randevu onaylanmıştır.\n\n" +
                                 $"Randevu Tarihi: {randevuTarihi:dd MMMM yyyy}\n\n" +
                                 $"Lütfen randevunuzdan en az 10 dakika önce kliniğimize geliniz.\n\n";
            EmailGonder(email, "Randevu Onayı - Veteriner Klinik", emailIcerik);
        }




        private void LogYaz(string log)
        {
            try
            {
                File.AppendAllText(_logDosyasi, log, Encoding.UTF8);// türkçe karakter bozulmasını engeelledik
            }
            catch
            {
                // Log yazma hatası (programın durmasını engelledik)
            }
        }

        /// <summary>
        /// Bildirim loglarını okur.
        /// </summary>
        public string LoglariOku()
        {
            try
            {
                if (File.Exists(_logDosyasi))
                {
                    return File.ReadAllText(_logDosyasi, Encoding.UTF8);
                }
            }
            catch { }//programın durmasını engelledik
            
            return "Henüz bildirim gönderilmemiş.";
        }
    }
}
