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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
            this.Load += Form14_Load;
            button2.Click += Button2_Click;
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Sokak hayvanı randevusu oluştur
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen hayvan cinsini girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;
            
            // Sokak hayvanı kaydet
            int hayvanId = veriYoneticisi.YeniHayvanId();
            var hayvan = new SokakHayvani();
            hayvan.Id = hayvanId;
            hayvan.Ad = "Sokak Hayvanı #" + hayvanId;
            hayvan.Tur = textBox2.Text;
            hayvan.Irk = "Melez";
            
            int yas = 0;
            int.TryParse(textBox3.Text, out yas);
            hayvan.Yas = yas;
            
            hayvan.KulakKupesiNo = textBox4.Text;
            hayvan.SorumluId = 1;
            hayvan.SorumluAdi = "Patili Koruyucu";
            hayvan.BulunduguBolge = "Belirtilmemiş";
            hayvan.SaglikDurumu = "Kontrol Bekliyor";
            
            veriYoneticisi.SokakHayvaniEkle(hayvan);
            
            // Randevu oluştur
            int randevuId = veriYoneticisi.YeniRandevuId();
            var randevu = new Randevu();
            randevu.Id = randevuId;
            randevu.HayvanId = hayvanId;
            randevu.KullaniciId = 1;
            randevu.RandevuTarihi = dateTimePicker1.Value;
            randevu.RandevuSaati = new TimeSpan(14, 0, 0);
            randevu.Sikayet = "Sokak hayvanı kontrolü";
            
            veriYoneticisi.RandevuEkle(randevu);
            
            MessageBox.Show(
                $"Sokak hayvanı randevusu oluşturuldu!\r\n\r\n" +
                $"Hayvan No: {hayvanId}\r\n" +
                $"Tür: {hayvan.Tur}\r\n" +
                $"Tarih: {randevu.RandevuTarihi:dd.MM.yyyy}\r\n\r\n" +
                "Randevunuz onay bekliyor.",
                "Başarılı", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
            
            // Temizle
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
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
