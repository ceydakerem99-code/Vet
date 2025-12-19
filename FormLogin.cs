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
            this.Text = "Veteriner Klinik - Giriş";
            this.Size = new Size(1390, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(62, 166, 107);

            // Başlık - ORTALI
            Label lblTitle = new Label();
            lblTitle.Text = "🐾 VETERİNER KLİNİK";
            lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(480, 100);
            this.Controls.Add(lblTitle);

            // Alt başlık - SİYAH
            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Sisteme Giriş Yapın";
            lblSubtitle.Font = new Font("Segoe UI", 14);
            lblSubtitle.ForeColor = Color.Black;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(580, 160);
            this.Controls.Add(lblSubtitle);

            // E-posta label
            Label lblEmail = new Label();
            lblEmail.Text = "E-posta:";
            lblEmail.Font = new Font("Segoe UI", 11);
            lblEmail.Location = new Point(470, 220);
            lblEmail.AutoSize = true;
            this.Controls.Add(lblEmail);

            // E-posta textbox
            txtEmail = new TextBox();
            txtEmail.Location = new Point(470, 250);
            txtEmail.Size = new Size(450, 40);
            txtEmail.Font = new Font("Segoe UI", 14);
            this.Controls.Add(txtEmail);

            // Şifre label
            Label lblPassword = new Label();
            lblPassword.Text = "Şifre:";
            lblPassword.Font = new Font("Segoe UI", 11);
            lblPassword.Location = new Point(470, 310);
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);

            // Şifre textbox
            txtPassword = new TextBox();
            txtPassword.Location = new Point(470, 340);
            txtPassword.Size = new Size(450, 40);
            txtPassword.Font = new Font("Segoe UI", 14);
            txtPassword.PasswordChar = '●';
            this.Controls.Add(txtPassword);

            // Giriş butonu
            btnLogin = new Button();
            btnLogin.Text = "Giriş Yap";
            btnLogin.Location = new Point(470, 420);
            btnLogin.Size = new Size(450, 55);
            btnLogin.BackColor = Color.FromArgb(255, 216, 63);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Kayıt Ol butonu
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
                FormKayit kayitForm = new FormKayit();
                this.Hide();
                kayitForm.Show();
                kayitForm.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnKayitOl);
        }

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;



        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string sifre = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen e-posta ve şifre girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;
            var oturumYoneticisi = OturumYoneticisi.Instance;

            // Admin kontrolü
            foreach (var admin in veriYoneticisi.Veterinerler)
            {
                if (oturumYoneticisi.GirisYap(admin, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {admin.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            // Kullanıcı kontrolü
            foreach (var sahip in veriYoneticisi.HayvanSahipleri)
            {
                if (oturumYoneticisi.GirisYap(sahip, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {sahip.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            // Sorumlu kontrolü
            foreach (var sorumlu in veriYoneticisi.Sorumlular)
            {
                if (oturumYoneticisi.GirisYap(sorumlu, email, sifre))
                {
                    MessageBox.Show($"Hoş geldiniz, {sorumlu.TamAdGetir()}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnaFormAc();
                    return;
                }
            }

            MessageBox.Show("E-posta veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void AnaFormAc()
        {
            Form1 anaForm = new Form1();
            this.Hide();
            anaForm.Show();
            anaForm.FormClosed += (s, args) => this.Close();
        }
    }
}
