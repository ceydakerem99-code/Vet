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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.Load += Form8_Load;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // Günlük ve aylık hasta sayılarını göster
            GuncelleIstatistikler();
        }

        private void GuncelleIstatistikler()
        {
            var veriYoneticisi = VeriYoneticisi.Instance;
            
            // Günlük muayeneler
            int gunlukSayi = veriYoneticisi.GunlukMuayeneSayisi(DateTime.Now);
            textBox3.Text = gunlukSayi.ToString();
            
            // Aylık muayeneler
            int aylikSayi = 0;
            for (int i = 0; i < 30; i++)
            {
                aylikSayi += veriYoneticisi.GunlukMuayeneSayisi(DateTime.Now.AddDays(-i));
            }
            textBox4.Text = aylikSayi.ToString();
            
            // Günlük liste
            textBox1.Clear();
            textBox1.AppendText("=== GÜNLÜK HASTA LİSTESİ ===\r\n");
            textBox1.AppendText($"Tarih: {DateTime.Now:dd.MM.yyyy}\r\n\r\n");
            
            foreach (var muayene in veriYoneticisi.Muayeneler)
            {
                if (muayene.MuayeneTarihi.Date == DateTime.Now.Date)
                {
                    textBox1.AppendText($"• Muayene #{muayene.Id}\r\n");
                    textBox1.AppendText($"  Şikayet: {muayene.Sikayet}\r\n");
                    textBox1.AppendText($"  Tanı: {muayene.Tani}\r\n\r\n");
                }
            }
            
            if (gunlukSayi == 0)
            {
                textBox1.AppendText("Bugün henüz hasta kaydı yok.\r\n");
            }
            
            // Aylık liste
            textBox2.Clear();
            textBox2.AppendText("=== AYLIK İSTATİSTİK ===\r\n\r\n");
            textBox2.AppendText($"Toplam Evcil Hayvan: {veriYoneticisi.EvcilHayvanlar.Count}\r\n");
            textBox2.AppendText($"Toplam Sokak Hayvanı: {veriYoneticisi.SokakHayvanlari.Count}\r\n");
            textBox2.AppendText($"Toplam Randevu: {veriYoneticisi.Randevular.Count}\r\n");
            textBox2.AppendText($"Bekleyen Randevu: {veriYoneticisi.BekleyenRandevular().Count}\r\n\r\n");
            textBox2.AppendText($"Son 30 Gün Muayene: {aylikSayi}\r\n");
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
