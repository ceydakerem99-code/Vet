using System;
using VeterinerProjectApp.Interfaces;

namespace VeterinerProjectApp.Models
{
    /// <summary>
    /// Muayene kaydını temsil eden sınıf.
    /// IMuayene arayüzünü uygular.
    /// </summary>
    public class Muayene : IMuayene
    {
        #region Private Fields

        private int _id;
        private int _hayvanId;
        private int _veterinerId;
        private DateTime _muayeneTarihi;
        private string _sikayet;
        private string _tani;
        private string _tedavi;
        private string _notlar;
        private decimal _ucret;
        private bool _tamamlandiMi;
        private string _veterinerAdi;

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

        public int VeterinerId
        {
            get { return _veterinerId; }
            set { _veterinerId = value; }
        }

        public DateTime MuayeneTarihi
        {
            get { return _muayeneTarihi; }
            set { _muayeneTarihi = value; }
        }

        public string Sikayet
        {
            get { return _sikayet; }
            set { _sikayet = value ?? ""; }
        }

        public string Tani
        {
            get { return _tani; }
            set { _tani = value ?? ""; }
        }

        public string Tedavi
        {
            get { return _tedavi; }
            set { _tedavi = value ?? ""; }
        }

        public string Notlar
        {
            get { return _notlar; }
            set { _notlar = value ?? ""; }
        }

        public decimal Ucret
        {
            get { return _ucret; }
            set
            {
                if (value >= 0)
                    _ucret = value;
                else
                    throw new ArgumentException("Ücret negatif olamaz.");
            }
        }

        public bool TamamlandiMi
        {
            get { return _tamamlandiMi; }
            set { _tamamlandiMi = value; }
        }

        public string VeterinerAdi
        {
            get { return _veterinerAdi; }
            set { _veterinerAdi = value ?? ""; }
        }

        #endregion

        #region Constructors

        public Muayene()
        {
            _muayeneTarihi = DateTime.Now;
            _tamamlandiMi = false;
        }

        public Muayene(int id, int hayvanId, int veterinerId, string sikayet)
            : this()
        {
            Id = id;
            HayvanId = hayvanId;
            VeterinerId = veterinerId;
            Sikayet = sikayet;
        }

        #endregion

        #region IMuayene Implementation

        public string MuayeneBilgisi()
        {
            return $"Muayene #{Id}\n" +
                   $"Tarih: {MuayeneTarihi:dd.MM.yyyy HH:mm}\n" +
                   $"Şikayet: {Sikayet}\n" +
                   $"Tanı: {Tani}\n" +
                   $"Tedavi: {Tedavi}\n" +
                   $"Durum: {(TamamlandiMi ? "Tamamlandı" : "Devam Ediyor")}";
        }

        public void MuayeneyiTamamla()
        {
            _tamamlandiMi = true;
        }

        public string RaporOlustur()
        {
            return "=== MUAYENE RAPORU ===\n" +
                   $"Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}\n" +
                   $"Muayene No: {Id}\n" +
                   $"Muayene Tarihi: {MuayeneTarihi:dd.MM.yyyy HH:mm}\n" +
                   $"Veteriner: {VeterinerAdi}\n" +
                   "---\n" +
                   $"Şikayet: {Sikayet}\n" +
                   $"Tanı: {Tani}\n" +
                   $"Uygulanan Tedavi: {Tedavi}\n" +
                   $"Notlar: {Notlar}\n" +
                   "---\n" +
                   $"Ücret: {Ucret:C}\n" +
                   $"Durum: {(TamamlandiMi ? "Tamamlandı" : "Devam Ediyor")}\n" +
                   "======================";
        }

        #endregion

        public override string ToString()
        {
            return $"Muayene {Id} - {MuayeneTarihi:dd.MM.yyyy}";
        }
    }
}
