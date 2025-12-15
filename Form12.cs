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
    public partial class Form12 : Form
    {
        private static EvcilHayvan _seciliHayvan;
        
        public Form12()
        {
            InitializeComponent();
            this.Load += Form12_Load;
        }

        public static void HayvanSec(EvcilHayvan hayvan)
        {
            _seciliHayvan = hayvan;
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            // Seçili hayvanın bilgilerini göster
            if (_seciliHayvan != null)
            {
                textBox1.Text = _seciliHayvan.Ad;
                textBox2.Text = $"{_seciliHayvan.Tur} - {_seciliHayvan.Irk}";
                textBox3.Text = _seciliHayvan.Yas.ToString();
                textBox4.Text = string.IsNullOrEmpty(_seciliHayvan.ChipNumarasi) ? "Yok" : _seciliHayvan.ChipNumarasi;
                textBox5.Text = _seciliHayvan.KayitTarihi.ToString("dd.MM.yyyy");
                textBox6.Text = _seciliHayvan.SaglikDurumu;
                
                // Yapılan işlemleri göster
                StringBuilder islemler = new StringBuilder();
                islemler.AppendLine("=== MUAYENE GEÇMİŞİ ===");
                if (_seciliHayvan.MuayeneGecmisi.Count == 0)
                {
                    islemler.AppendLine("Henüz muayene kaydı yok.");
                }
                else
                {
                    foreach (var muayene in _seciliHayvan.MuayeneGecmisi)
                    {
                        islemler.AppendLine($"• {muayene.MuayeneTarihi:dd.MM.yyyy} - {muayene.Tani}");
                    }
                }
                
                islemler.AppendLine("\r\n=== AŞI GEÇMİŞİ ===");
                if (_seciliHayvan.Asilar.Count == 0)
                {
                    islemler.AppendLine("Henüz aşı kaydı yok.");
                }
                else
                {
                    foreach (var asi in _seciliHayvan.Asilar)
                    {
                        islemler.AppendLine($"• {asi.AsiTarihi:dd.MM.yyyy} - {asi.AsiAdi}");
                    }
                }
                
                textBox7.Text = islemler.ToString();
            }
            else
            {
                // Demo veri göster
                textBox1.Text = "Boncuk";
                textBox2.Text = "Kedi - Tekir";
                textBox3.Text = "3";
                textBox4.Text = "123456789";
                textBox5.Text = DateTime.Now.AddMonths(-6).ToString("dd.MM.yyyy");
                textBox6.Text = "Sağlıklı";
                textBox7.Text = "=== MUAYENE GEÇMİŞİ ===\r\n• Örnek muayene kaydı\r\n\r\n=== AŞI GEÇMİŞİ ===\r\n• Karma aşı yapıldı";
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
