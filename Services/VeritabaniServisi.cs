using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp.Services
{
    /// <summary>
    /// SQLite veritabanı servisi - Kalıcı veri saklama.
    /// </summary>
    public class VeritabaniServisi
    {
        private static VeritabaniServisi _instance;
        private static readonly object _lock = new object();
        private readonly string _veritabaniYolu;
        private readonly string _connectionString;

        public static VeritabaniServisi Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new VeritabaniServisi();
                        }
                    }
                }
                return _instance;
            }
        }

        private VeritabaniServisi()
        {
            _veritabaniYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VeterinerKlinik.db");
            _connectionString = $"Data Source={_veritabaniYolu}";
            TablolariOlustur();
        }

        private void TablolariOlustur()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                // Kullanicilar tablosu
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Kullanicilar (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Ad TEXT NOT NULL,
                        Soyad TEXT NOT NULL,
                        Email TEXT UNIQUE NOT NULL,
                        Telefon TEXT,
                        Sifre TEXT NOT NULL,
                        Rol TEXT NOT NULL,
                        KayitTarihi TEXT NOT NULL
                    )";
                cmd.ExecuteNonQuery();

                // Hayvanlar tablosu
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Hayvanlar (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Ad TEXT NOT NULL,
                        Tur TEXT NOT NULL,
                        Irk TEXT,
                        Yas INTEGER,
                        Cinsiyet TEXT,
                        SaglikDurumu TEXT,
                        SahipId INTEGER,
                        ChipNumarasi TEXT,
                        FotografYolu TEXT,
                        KayitTarihi TEXT NOT NULL,
                        Tip TEXT NOT NULL
                    )";
                cmd.ExecuteNonQuery();

                // Randevular tablosu
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Randevular (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        HayvanId INTEGER,
                        KullaniciId INTEGER,
                        RandevuTarihi TEXT NOT NULL,
                        RandevuSaati TEXT,
                        Sikayet TEXT,
                        Durum TEXT NOT NULL,
                        Notlar TEXT
                    )";
                cmd.ExecuteNonQuery();

                // Muayeneler tablosu
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Muayeneler (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        HayvanId INTEGER,
                        VeterinerId INTEGER,
                        Tarih TEXT NOT NULL,
                        Sikayet TEXT,
                        Tani TEXT,
                        Tedavi TEXT,
                        Ucret REAL
                    )";
                cmd.ExecuteNonQuery();

                // Asilar tablosu
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Asilar (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        HayvanId INTEGER,
                        AsiAdi TEXT NOT NULL,
                        AsiTarihi TEXT NOT NULL,
                        SonrakiAsiTarihi TEXT
                    )";
                cmd.ExecuteNonQuery();
            }
        }

        #region Hayvan İşlemleri

        public void HayvanKaydet(EvcilHayvan hayvan)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Hayvanlar (Ad, Tur, Irk, Yas, Cinsiyet, SaglikDurumu, SahipId, ChipNumarasi, KayitTarihi, Tip)
                    VALUES (@Ad, @Tur, @Irk, @Yas, @Cinsiyet, @SaglikDurumu, @SahipId, @ChipNumarasi, @KayitTarihi, 'Evcil')";
                
                cmd.Parameters.AddWithValue("@Ad", hayvan.Ad);
                cmd.Parameters.AddWithValue("@Tur", hayvan.Tur);
                cmd.Parameters.AddWithValue("@Irk", hayvan.Irk ?? "");
                cmd.Parameters.AddWithValue("@Yas", hayvan.Yas);
                cmd.Parameters.AddWithValue("@Cinsiyet", hayvan.Cinsiyet ?? "");
                cmd.Parameters.AddWithValue("@SaglikDurumu", hayvan.SaglikDurumu ?? "");
                cmd.Parameters.AddWithValue("@SahipId", hayvan.SahipId);
                cmd.Parameters.AddWithValue("@ChipNumarasi", hayvan.ChipNumarasi ?? "");
                cmd.Parameters.AddWithValue("@KayitTarihi", DateTime.Now.ToString("yyyy-MM-dd"));
                
                cmd.ExecuteNonQuery();
                
                // Son eklenen ID'yi al
                cmd.CommandText = "SELECT last_insert_rowid()";
                hayvan.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<EvcilHayvan> TumHayvanlariGetir()
        {
            var liste = new List<EvcilHayvan>();
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Hayvanlar WHERE Tip = 'Evcil'";
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hayvan = new EvcilHayvan();
                        hayvan.Id = reader.GetInt32(0);
                        hayvan.Ad = reader.GetString(1);
                        hayvan.Tur = reader.GetString(2);
                        hayvan.Irk = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        hayvan.Yas = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        hayvan.Cinsiyet = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        hayvan.SaglikDurumu = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        hayvan.SahipId = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                        hayvan.ChipNumarasi = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        liste.Add(hayvan);
                    }
                }
            }
            
            return liste;
        }

        public void HayvanSil(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Hayvanlar WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Randevu İşlemleri

        public void RandevuKaydet(Randevu randevu)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Randevular (HayvanId, KullaniciId, RandevuTarihi, RandevuSaati, Sikayet, Durum, Notlar)
                    VALUES (@HayvanId, @KullaniciId, @RandevuTarihi, @RandevuSaati, @Sikayet, @Durum, @Notlar)";
                
                cmd.Parameters.AddWithValue("@HayvanId", randevu.HayvanId);
                cmd.Parameters.AddWithValue("@KullaniciId", randevu.KullaniciId);
                cmd.Parameters.AddWithValue("@RandevuTarihi", randevu.RandevuTarihi.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@RandevuSaati", randevu.RandevuSaati.ToString());
                cmd.Parameters.AddWithValue("@Sikayet", randevu.Sikayet ?? "");
                cmd.Parameters.AddWithValue("@Durum", randevu.Durum.ToString());
                cmd.Parameters.AddWithValue("@Notlar", randevu.Notlar ?? "");
                
                cmd.ExecuteNonQuery();
                
                cmd.CommandText = "SELECT last_insert_rowid()";
                randevu.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Randevu> TumRandevulariGetir()
        {
            var liste = new List<Randevu>();
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Randevular";
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var randevu = new Randevu();
                        randevu.Id = reader.GetInt32(0);
                        randevu.HayvanId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        randevu.KullaniciId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        randevu.RandevuTarihi = DateTime.Parse(reader.GetString(3));
                        randevu.Sikayet = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        liste.Add(randevu);
                    }
                }
            }
            
            return liste;
        }

        #endregion

        #region Muayene İşlemleri

        public void MuayeneKaydet(Muayene muayene)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Muayeneler (HayvanId, VeterinerId, Tarih, Sikayet, Tani, Tedavi, Ucret)
                    VALUES (@HayvanId, @VeterinerId, @Tarih, @Sikayet, @Tani, @Tedavi, @Ucret)";
                
                cmd.Parameters.AddWithValue("@HayvanId", muayene.HayvanId);
                cmd.Parameters.AddWithValue("@VeterinerId", muayene.VeterinerId);
                cmd.Parameters.AddWithValue("@Tarih", muayene.MuayeneTarihi.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Sikayet", muayene.Sikayet ?? "");
                cmd.Parameters.AddWithValue("@Tani", muayene.Tani ?? "");
                cmd.Parameters.AddWithValue("@Tedavi", muayene.Tedavi ?? "");
                cmd.Parameters.AddWithValue("@Ucret", muayene.Ucret);
                
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region İstatistikler

        public int ToplamHayvanSayisi()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Hayvanlar";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int ToplamRandevuSayisi()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Randevular";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        #endregion
    }
}
