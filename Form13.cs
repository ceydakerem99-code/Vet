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
        private CheckBox chkKisir;
        private CheckBox chkAsiTam;
        private ComboBox cmbGelisSebebi;
        private Label lblGelisSebebi;

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

            
            lblGelisSebebi = new Label();
            lblGelisSebebi.Text = "Geliş Sebebi:";
            lblGelisSebebi.Location = new Point(700, 175);
            lblGelisSebebi.AutoSize = true;
            lblGelisSebebi.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblGelisSebebi.BackColor = Color.Transparent;
            this.Controls.Add(lblGelisSebebi);

           
            cmbGelisSebebi = new ComboBox();
            cmbGelisSebebi.Location = new Point(870, 172);
            cmbGelisSebebi.Size = new Size(300, 30);
            cmbGelisSebebi.Font = new Font("Segoe UI", 11);
            cmbGelisSebebi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGelisSebebi.Items.Add("Genel Muayene");
            cmbGelisSebebi.Items.Add("Aşı");
            cmbGelisSebebi.Items.Add("Kısırlaştırma");
            cmbGelisSebebi.Items.Add("Diş Bakımı");
            cmbGelisSebebi.Items.Add("Tırnak Kesimi");
            cmbGelisSebebi.Items.Add("Tüy Bakımı/Traş");
            cmbGelisSebebi.Items.Add("Ameliyat");
            cmbGelisSebebi.Items.Add("Kontrol");
            cmbGelisSebebi.Items.Add("Acil");
            cmbGelisSebebi.Items.Add("Diğer");
            cmbGelisSebebi.SelectedIndex = 0;
            this.Controls.Add(cmbGelisSebebi);

        
            chkKisir = new CheckBox();
            chkKisir.Text = "Kısırlaştırıldı mı?";
            chkKisir.Location = new Point(720, 480);
            chkKisir.AutoSize = true;
            chkKisir.Font = new Font("Segoe UI", 10);
            chkKisir.BackColor = Color.Transparent;
            this.Controls.Add(chkKisir);

        
            chkAsiTam = new CheckBox();
            chkAsiTam.Text = "Aşıları tam mı?";
            chkAsiTam.Location = new Point(920, 480);
            chkAsiTam.AutoSize = true;
            chkAsiTam.Font = new Font("Segoe UI", 10);
            chkAsiTam.BackColor = Color.Transparent;
            this.Controls.Add(chkAsiTam);
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
            var oturum = OturumYoneticisi.Instance;
            
            // hayvanı kaydet
            int hayvanId = veriYoneticisi.YeniHayvanId();
            var hayvan = new EvcilHayvan();
            hayvan.Id = hayvanId;
            hayvan.Ad = textBox1.Text;
            hayvan.Tur = textBox2.Text;
            
            int yas = 0;
            int.TryParse(textBox3.Text, out yas);
            hayvan.Yas = yas;
            
            hayvan.ChipNumarasi = textBox4.Text;
            
            // Giriş yapan kullanıcıyı sahip olarak ata
            if (oturum.AktifKullanici != null)
            {
                hayvan.SahipId = oturum.AktifKullanici.Id;
                hayvan.SahipAdi = oturum.AktifKullanici.TamAdGetir();
            }
            else
            {
                hayvan.SahipId = 1;
                hayvan.SahipAdi = "Demo Kullanıcı";
            }
            
            hayvan.SaglikDurumu = "Bilinmiyor";
            
            // Kullanıcının girdiği kısırlık bilgisi
            if (chkKisir != null && chkKisir.Checked)
                hayvan.Kisirlastir();
            
            veriYoneticisi.EvcilHayvanEkle(hayvan);
            
            // Randevu oluştur
            int randevuId = veriYoneticisi.YeniRandevuId();
            var randevu = new Randevu();
            randevu.Id = randevuId;
            randevu.HayvanId = hayvanId;
            randevu.KullaniciId = oturum.AktifKullanici?.Id ?? 1;
            randevu.RandevuTarihi = dateTimePicker1.Value;
            randevu.RandevuSaati = new TimeSpan(10, 0, 0);
            
            // Geliş sebebini kaydet
            string gelisSebebi = cmbGelisSebebi?.SelectedItem?.ToString() ?? "Genel Muayene";
            randevu.Sikayet = gelisSebebi;
            randevu.RandevuNedeni = gelisSebebi;
            
            veriYoneticisi.RandevuEkle(randevu);
            
            MessageBox.Show(
                $"Randevu başarıyla oluşturuldu!\r\n\r\n" +
                $"Geliş Sebebi: {gelisSebebi}\r\n" +
                $"Hayvan: {hayvan.Ad}\r\n" +
                $"Tür: {hayvan.Tur}\r\n" +
                $"Kısır: {(hayvan.KisirlastirildiMi ? "Evet" : "Hayır")}\r\n" +
                $"Aşı: {(chkAsiTam.Checked ? "Tam" : "Eksik")}\r\n" +
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
            if (chkKisir != null) chkKisir.Checked = false;
            if (chkAsiTam != null) chkAsiTam.Checked = false;
            if (cmbGelisSebebi != null) cmbGelisSebebi.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
        }
    }
}


