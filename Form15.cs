using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class Form15 : Form
    {
        private static SokakHayvani _seciliHayvan;
        
        public Form15()
        {
            InitializeComponent();
        }

        public static void HayvanSec(SokakHayvani hayvan)
        {
            _seciliHayvan = hayvan;
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            var veriYoneticisi = VeriYoneticisi.Instance;
            
            if (_seciliHayvan != null)
            {
                // Seçili hayvanı göster
                textBox2.Text = $"{_seciliHayvan.Tur} - {_seciliHayvan.Irk}";
                textBox3.Text = _seciliHayvan.Yas.ToString();
                textBox4.Text = string.IsNullOrEmpty(_seciliHayvan.KulakKupesiNo) ? "Yok" : _seciliHayvan.KulakKupesiNo;
                textBox5.Text = _seciliHayvan.BulunmaTarihi.ToString("dd.MM.yyyy");
                textBox6.Text = _seciliHayvan.SaglikDurumu;
                
                StringBuilder islemler = new StringBuilder();
                islemler.AppendLine($"Bölge: {_seciliHayvan.BulunduguBolge}");
                islemler.AppendLine($"Kısırlaştırıldı: {(_seciliHayvan.KisirlastirildiMi ? "Evet" : "Hayır")}");
                islemler.AppendLine($"Tedavi Onayı: {(_seciliHayvan.TedaviOnayliMi ? "Onaylı" : "Beklemede")}");
                islemler.AppendLine($"Sorumlu: {_seciliHayvan.SorumluAdi}");
                
                if (_seciliHayvan.MuayeneGecmisi.Count > 0)
                {
                    islemler.AppendLine("\r\n=== MUAYENELER ===");
                    foreach (var m in _seciliHayvan.MuayeneGecmisi)
                    {
                        islemler.AppendLine($"• {m.MuayeneTarihi:dd.MM.yyyy} - {m.Tani}");
                    }
                }
                
                textBox7.Text = islemler.ToString();
            }
            else if (veriYoneticisi.SokakHayvanlari.Count > 0)
            {
                // İlk sokak hayvanını göster
                var ilkHayvan = veriYoneticisi.SokakHayvanlari[0];
                textBox2.Text = $"{ilkHayvan.Tur} - {ilkHayvan.Irk}";
                textBox3.Text = ilkHayvan.Yas.ToString();
                textBox4.Text = ilkHayvan.KulakKupesiNo ?? "Yok";
                textBox5.Text = ilkHayvan.BulunmaTarihi.ToString("dd.MM.yyyy");
                textBox6.Text = ilkHayvan.SaglikDurumu;
                textBox7.Text = $"Bölge: {ilkHayvan.BulunduguBolge}\r\nSorumlu: {ilkHayvan.SorumluAdi}";
            }
            else
            {
                // Varsayılan değerler
                textBox2.Text = "Köpek - Melez";
                textBox3.Text = "2";
                textBox4.Text = "SK-001";
                textBox5.Text = DateTime.Now.AddMonths(-3).ToString("dd.MM.yyyy");
                textBox6.Text = "Tedavi Altında";
                textBox7.Text = "Bölge: Merkez Mahallesi\r\nKısırlaştırıldı: Evet\r\nTedavi Onayı: Beklemede";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }
    }
}
