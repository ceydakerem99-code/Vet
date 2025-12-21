using System;
using System.Drawing;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form ayarlarını yapıyoruz
            this.Text = "Veteriner Klinik - Giriş";
            this.Size = new Size(1390, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false; // Tam ekran yapılmasını engelle
            this.BackColor = Color.FromArgb(62, 166, 107); // Yeşil arka plan rengi

            // Başlık Label'ı - Sayfanın en üstündeki yazı
            Label lblBaslik = new Label();
            lblBaslik.Text = "🐾 VETERİNER KLİNİK";
            lblBaslik.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(480, 100);
            this.Controls.Add(lblBaslik);

            // Alt başlık - Kullanıcıya ne yapacağını söyleyen yazı
            Label lblAltBaslik = new Label();
            lblAltBaslik.Text = "Sisteme Giriş Yapın";
            lblAltBaslik.Font = new Font("Segoe UI", 14);
            lblAltBaslik.ForeColor = Color.Black;
            lblAltBaslik.AutoSize = true;
            lblAltBaslik.Location = new Point(580, 160);
            this.Controls.Add(lblAltBaslik);

            // E-posta etiketi
            Label lblEposta = new Label();
            lblEposta.Text = "E-posta:";
            lblEposta.Font = new Font("Segoe UI", 11);
            lblEposta.Location = new Point(470, 220);
            lblEposta.AutoSize = true;
            this.Controls.Add(lblEposta);

            // E-posta girilecek kutucuk
            txtEposta = new TextBox();
            txtEposta.Location = new Point(470, 250);
            txtEposta.Size = new Size(450, 40);
            txtEposta.Font = new Font("Segoe UI", 14);
            this.Controls.Add(txtEposta);

            // Şifre etiketi
            Label lblSifre = new Label();
            lblSifre.Text = "Şifre:";
            lblSifre.Font = new Font("Segoe UI", 11);
            lblSifre.Location = new Point(470, 310);
            lblSifre.AutoSize = true;
            this.Controls.Add(lblSifre);

            // Şifre girilecek kutucuk
            txtSifre = new TextBox();
            txtSifre.Location = new Point(470, 340);
            txtSifre.Size = new Size(450, 40);
            txtSifre.Font = new Font("Segoe UI", 14);
            txtSifre.PasswordChar = '●'; // Şifreyi gizlemek için nokta koy
            this.Controls.Add(txtSifre);

            // Giriş yapma butonu
            btnGirisYap = new Button();
            btnGirisYap.Text = "Giriş Yap";
            btnGirisYap.Location = new Point(470, 420);
            btnGirisYap.Size = new Size(450, 55);
            btnGirisYap.BackColor = Color.FromArgb(255, 216, 63); // Sarı renk
            btnGirisYap.FlatStyle = FlatStyle.Flat;
            btnGirisYap.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnGirisYap.Cursor = Cursors.Hand; // Üzerine gelince el işareti çıksın
            btnGirisYap.Click += BtnGirisYap_Click; // Tıklanınca çalışacak fonksiyon
            this.Controls.Add(btnGirisYap);

            // Kayıt Ol butonu - Yeni kullanıcılar için
            Button btnKayitOl = new Button();
            btnKayitOl.Text = "Kayıt Ol";
            btnKayitOl.Location = new Point(470, 490);
            btnKayitOl.Size = new Size(450, 45);
            btnKayitOl.BackColor = Color.FromArgb(100, 200, 100);
            btnKayitOl.ForeColor = Color.White;
            btnKayitOl.FlatStyle = FlatStyle.Flat;
            btnKayitOl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnKayitOl.Cursor = Cursors.Hand;
            btnKayitOl.Click += (s, ev) => {
                // Kayıt formuna geçiş yapıyoruz
                FormKayit kayitForm = new FormKayit();
                this.Hide(); // Bu formu gizle
                kayitForm.Show(); // Kayıt formunu aç
                kayitForm.FormClosed += (s2, e2) => this.Close(); // Kayıt formu kapanınca uygulamayı kapat
            };
            this.Controls.Add(btnKayitOl);
        }

        // Form elemanlarını burada tanımladım
        private TextBox txtEposta;
        private TextBox txtSifre;
        private Button btnGirisYap;


        // Giriş butonuna tıklandığında bu metod çalışır
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            string email = txtEposta.Text.Trim(); // Boşlukları temizle
            string sifre = txtSifre.Text;

            // Eğer alanlardan biri boşsa uyarı ver
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen e-posta ve şifre girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verileri ve oturum yöneticisini alıyoruz
            var veriYoneticisi = VeriYoneticisi.Instance;
            var oturumYoneticisi = OturumYoneticisi.Instance;

            // 1. Admin girişi kontrolü
            foreach (var admin in veriYoneticisi.Veterinerler)
            {
                if (oturumYoneticisi.GirisYap(admin, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {admin.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            // 2. Hayvan Sahibi girişi kontrolü
            foreach (var sahip in veriYoneticisi.HayvanSahipleri)
            {
                if (oturumYoneticisi.GirisYap(sahip, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {sahip.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            // 3. Sorumlu personeli girişi kontrolü
            foreach (var sorumlu in veriYoneticisi.Sorumlular)
            {
                if (oturumYoneticisi.GirisYap(sorumlu, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {sorumlu.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            // Eğer hiçbir kullanıcı bulunamazsa hata mesajı göster
            MessageBox.Show("E-posta veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // Ana sayfayı açan yardımcı metod
        private void AnaFormAc()
        {
            Form1 anaForm = new Form1();
            this.Hide();
            anaForm.Show();
            anaForm.FormClosed += (s, args) => this.Close(); 
        }
    }
}
