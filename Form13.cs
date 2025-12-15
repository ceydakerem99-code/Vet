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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
            this.Load += Form13_Load;
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            // Varsayılan değerler
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Randevu oluştur
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen hayvan adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;
            
            // Önce hayvanı kaydet
            int hayvanId = veriYoneticisi.YeniHayvanId();
            var hayvan = new EvcilHayvan();
            hayvan.Id = hayvanId;
            hayvan.Ad = textBox1.Text;
            hayvan.Tur = textBox2.Text;
            
            int yas = 0;
            int.TryParse(textBox3.Text, out yas);
            hayvan.Yas = yas;
            
            hayvan.ChipNumarasi = textBox4.Text;
            hayvan.SahipId = 1; // Demo kullanıcı
            hayvan.SahipAdi = "Demo Kullanıcı";
            hayvan.SaglikDurumu = "Bilinmiyor";
            
            veriYoneticisi.EvcilHayvanEkle(hayvan);
            
            // Randevu oluştur
            int randevuId = veriYoneticisi.YeniRandevuId();
            var randevu = new Randevu();
            randevu.Id = randevuId;
            randevu.HayvanId = hayvanId;
            randevu.KullaniciId = 1;
            randevu.RandevuTarihi = dateTimePicker1.Value;
            randevu.RandevuSaati = new TimeSpan(10, 0, 0);
            randevu.Sikayet = "Genel kontrol";
            
            veriYoneticisi.RandevuEkle(randevu);
            
            MessageBox.Show(
                $"Randevu başarıyla oluşturuldu!\r\n\r\n" +
                $"Hayvan: {hayvan.Ad}\r\n" +
                $"Tarih: {randevu.RandevuTarihi:dd.MM.yyyy}\r\n" +
                $"Randevu No: {randevuId}\r\n\r\n" +
                "Randevunuz onay için klinik yöneticisine iletildi.",
                "Başarılı", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
            
            // Formu temizle
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
        }
    }
}
