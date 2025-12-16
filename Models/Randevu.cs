using System;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Randevu kaydını temsil eden sınıf.
    /// Kullanıcıların veteriner randevularını takip etmek için kullanılır.
    /// </summary>
    public class Randevu
    {
        #region Private Fields

        private int _id;
        private int _hayvanId;
        private int _kullaniciId;
        private int? _veterinerId;
        private DateTime _randevuTarihi;
        private TimeSpan _randevuSaati;
        private RandevuDurumu _durum;
        private string _sikayet;
        private string _notlar;
        private string _iptalNedeni;
        private DateTime _olusturmaTarihi;
        private DateTime? _onayTarihi;

        #endregion

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int HayvanId
        {
            get { return _hayvanId; }
            set { _hayvanId = value; }
        }

        public int KullaniciId
        {
            get { return _kullaniciId; }
            set { _kullaniciId = value; }
        }

        public int? VeterinerId
        {
            get { return _veterinerId; }
            set { _veterinerId = value; }
        }

        public DateTime RandevuTarihi
        {
            get { return _randevuTarihi; }
            set { _randevuTarihi = value.Date; }
        }

        public TimeSpan RandevuSaati
        {
            get { return _randevuSaati; }
            set { _randevuSaati = value; }
        }

        public RandevuDurumu Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        public string Sikayet
        {
            get { return _sikayet; }
            set { _sikayet = value ?? ""; }
        }

        public string Notlar
        {
            get { return _notlar; }
            set { _notlar = value ?? ""; }
        }

        public string IptalNedeni
        {
            get { return _iptalNedeni; }
            set { _iptalNedeni = value ?? ""; }
        }

        /// <summary>
        /// Randevunun nedeni/amacı
        /// </summary>
        public string RandevuNedeni { get; set; } = "Genel Kontrol";

        public DateTime OlusturmaTarihi
        {
            get { return _olusturmaTarihi; }
        }

        public DateTime? OnayTarihi
        {
            get { return _onayTarihi; }
            set { _onayTarihi = value; }
        }

        /// <summary>
        /// Randevunun tam tarih ve saatini döndürür.
        /// </summary>
        public DateTime TamTarihSaat
        {
            get { return RandevuTarihi.Add(RandevuSaati); }
        }

        #endregion

        #region Constructors

        public Randevu()
        {
            _olusturmaTarihi = DateTime.Now;
            _durum = RandevuDurumu.Bekliyor;
            _randevuSaati = new TimeSpan(9, 0, 0); // Varsayılan saat 09:00
        }

        public Randevu(int id, int hayvanId, int kullaniciId, DateTime randevuTarihi)
            : this()
        {
            Id = id;
            HayvanId = hayvanId;
            KullaniciId = kullaniciId;
            RandevuTarihi = randevuTarihi;
        }

        public Randevu(int id, int hayvanId, int kullaniciId, DateTime randevuTarihi, TimeSpan randevuSaati, string sikayet)
            : this(id, hayvanId, kullaniciId, randevuTarihi)
        {
            RandevuSaati = randevuSaati;
            Sikayet = sikayet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Randevu bilgilerini özet olarak döndürür.
        /// </summary>
        public string RandevuBilgisi()
        {
            return $"Randevu #{Id}\n" +
                   $"Tarih: {RandevuTarihi:dd.MM.yyyy}\n" +
                   $"Saat: {RandevuSaati:hh\\:mm}\n" +
                   $"Durum: {DurumMetni()}\n" +
                   $"Şikayet: {Sikayet}";
        }

        /// <summary>
        /// Randevu durumunun Türkçe metnini döndürür.
        /// </summary>
        public string DurumMetni()
        {
            switch (Durum)
            {
                case RandevuDurumu.Bekliyor: return "Onay Bekliyor";
                case RandevuDurumu.Onaylandi: return "Onaylandı";
                case RandevuDurumu.Reddedildi: return "Reddedildi";
                case RandevuDurumu.Tamamlandi: return "Tamamlandı";
                case RandevuDurumu.IptalEdildi: return "İptal Edildi";
                case RandevuDurumu.Gelmedi: return "Hasta Gelmedi";
                default: return "Bilinmiyor";
            }
        }

        /// <summary>
        /// Randevuyu onaylar.
        /// </summary>
        public void Onayla(int veterinerId)
        {
            if (Durum == RandevuDurumu.Bekliyor)
            {
                _durum = RandevuDurumu.Onaylandi;
                _veterinerId = veterinerId;
                _onayTarihi = DateTime.Now;
            }
            else
            {
                throw new InvalidOperationException("Sadece bekleyen randevular onaylanabilir.");
            }
        }

        /// <summary>
        /// Randevuyu reddeder.
        /// </summary>
        public void Reddet(string neden)
        {
            if (Durum == RandevuDurumu.Bekliyor)
            {
                _durum = RandevuDurumu.Reddedildi;
                _iptalNedeni = neden;
            }
            else
            {
                throw new InvalidOperationException("Sadece bekleyen randevular reddedilebilir.");
            }
        }

        /// <summary>
        /// Randevuyu iptal eder.
        /// </summary>
        public void IptalEt(string neden)
        {
            if (Durum == RandevuDurumu.Bekliyor || Durum == RandevuDurumu.Onaylandi)
            {
                _durum = RandevuDurumu.IptalEdildi;
                _iptalNedeni = neden;
            }
            else
            {
                throw new InvalidOperationException("Bu randevu iptal edilemez.");
            }
        }

        /// <summary>
        /// Randevuyu tamamlandı olarak işaretler.
        /// </summary>
        public void Tamamla()
        {
            if (Durum == RandevuDurumu.Onaylandi)
            {
                _durum = RandevuDurumu.Tamamlandi;
            }
            else
            {
                throw new InvalidOperationException("Sadece onaylanmış randevular tamamlanabilir.");
            }
        }

        /// <summary>
        /// Randevuya kaç gün kaldığını hesaplar.
        /// </summary>
        public int KalanGunSayisi()
        {
            TimeSpan fark = TamTarihSaat - DateTime.Now;
            return (int)fark.TotalDays;
        }

        /// <summary>
        /// Randevunun bugün olup olmadığını kontrol eder.
        /// </summary>
        public bool BugunMu()
        {
            return RandevuTarihi.Date == DateTime.Now.Date;
        }

        /// <summary>
        /// Randevunun geçip geçmediğini kontrol eder.
        /// </summary>
        public bool GecmisMi()
        {
            return TamTarihSaat < DateTime.Now && Durum != RandevuDurumu.Tamamlandi;
        }

        #endregion

        public override string ToString()
        {
            return $"Randevu {Id} - {RandevuTarihi:dd.MM.yyyy} {RandevuSaati:hh\\:mm} ({DurumMetni()})";
        }
    }
}
