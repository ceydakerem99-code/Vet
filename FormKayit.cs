using System;
using System.Drawing;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp
{
    public partial class FormKayit : Form
    {
        private TextBox txtAd;
        private TextBox txtSoyad;
        private TextBox txtEmail;
        private TextBox txtTelefon;
        private TextBox txtSifre;
        private TextBox txtSifreTekrar;
        private ComboBox cmbRol;
        private Button btnKayitOl;
        private Button btnGeriDon;

        public FormKayit()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Veteriner Klinik - Kayıt Ol";
            this.Size = new Size(550, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(255, 251, 224);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "📝 Yeni Hesap Oluştur";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(80, 80, 80);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(140, 20);
            this.Controls.Add(lblTitle);

            int labelX = 70;
            int inputX = 70;
            int inputWidth = 400;
            int startY = 70;
            int spacing = 55;

            // Ad
            AddLabel("Ad:", labelX, startY);
            txtAd = AddTextBox(inputX, startY + 22, inputWidth);

            // Soyad
            AddLabel("Soyad:", labelX, startY + spacing);
            txtSoyad = AddTextBox(inputX, startY + spacing + 22, inputWidth);

            // E-posta
            AddLabel("E-posta:", labelX, startY + spacing * 2);
            txtEmail = AddTextBox(inputX, startY + spacing * 2 + 22, inputWidth);

            // Telefon
            AddLabel("Telefon:", labelX, startY + spacing * 3);
            txtTelefon = AddTextBox(inputX, startY + spacing * 3 + 22, inputWidth);

            // Şifre
            AddLabel("Şifre:", labelX, startY + spacing * 4);
            txtSifre = AddTextBox(inputX, startY + spacing * 4 + 22, inputWidth, true);

            // Şifre Tekrar
            AddLabel("Şifre Tekrar:", labelX, startY + spacing * 5);
            txtSifreTekrar = AddTextBox(inputX, startY + spacing * 5 + 22, inputWidth, true);

            // Rol Seçimi
            AddLabel("Hesap Türü:", labelX, startY + spacing * 6);
            cmbRol = new ComboBox();
            cmbRol.Location = new Point(inputX, startY + spacing * 6 + 22);
            cmbRol.Size = new Size(inputWidth, 30);
            cmbRol.Font = new Font("Segoe UI", 11);
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.Items.Add("Pet Kullanıcısı (Hayvan Sahibi)");
            cmbRol.Items.Add("Patili Koruyucu (Sokak Hayvanı Sorumlusu)");
            cmbRol.SelectedIndex = 0;
            this.Controls.Add(cmbRol);

            // Kayıt Ol butonu
            btnKayitOl = new Button();
            btnKayitOl.Text = "Kayıt Ol";
            btnKayitOl.Location = new Point(70, 480);
            btnKayitOl.Size = new Size(195, 45);
            btnKayitOl.BackColor = Color.FromArgb(100, 200, 100);
            btnKayitOl.ForeColor = Color.White;
            btnKayitOl.FlatStyle = FlatStyle.Flat;
            btnKayitOl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnKayitOl.Cursor = Cursors.Hand;
            btnKayitOl.Click += BtnKayitOl_Click;
            this.Controls.Add(btnKayitOl);

            // Geri Dön butonu
            btnGeriDon = new Button();
            btnGeriDon.Text = "Geri Dön";
            btnGeriDon.Location = new Point(275, 480);
            btnGeriDon.Size = new Size(195, 45);
            btnGeriDon.BackColor = Color.LightGray;
            btnGeriDon.FlatStyle = FlatStyle.Flat;
            btnGeriDon.Font = new Font("Segoe UI", 12);
            btnGeriDon.Cursor = Cursors.Hand;
            btnGeriDon.Click += (s, e) => {
                FormLogin login = new FormLogin();
                this.Hide();
                login.Show();
                login.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnGeriDon);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10);
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);
        }

        private TextBox AddTextBox(int x, int y, int width, bool isPassword = false)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(width, 30);
            txt.Font = new Font("Segoe UI", 11);
            if (isPassword) txt.PasswordChar = '●';
            this.Controls.Add(txt);
            return txt;
        }

        private void BtnKayitOl_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (string.IsNullOrWhiteSpace(txtAd.Text) || 
                string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSifre.Text.Length < 4)
            {
                MessageBox.Show("Şifre en az 4 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Geçerli bir e-posta adresi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;

            // E-posta kontrolü
            foreach (var k in veriYoneticisi.HayvanSahipleri)
            {
                if (k.Email.ToLower() == txtEmail.Text.ToLower())
                {
                    MessageBox.Show("Bu e-posta adresi zaten kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                if (cmbRol.SelectedIndex == 0) // Pet Kullanıcısı
                {
                    int yeniId = veriYoneticisi.HayvanSahipleri.Count + veriYoneticisi.Sorumlular.Count + 10;
                    var yeniKullanici = new HayvanSahibi(
                        yeniId,
                        txtAd.Text.Trim(),
                        txtSoyad.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefon.Text.Trim(),
                        txtSifre.Text
                    );
                    veriYoneticisi.HayvanSahibiEkle(yeniKullanici);
                }
                else // Patili Koruyucu
                {
                    int yeniId = veriYoneticisi.Sorumlular.Count + veriYoneticisi.HayvanSahipleri.Count + 10;
                    var yeniSorumlu = new SokakHayvaniSorumlusu(
                        yeniId,
                        txtAd.Text.Trim(),
                        txtSoyad.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtTelefon.Text.Trim(),
                        txtSifre.Text,
                        "Genel Bölge"
                    );
                    veriYoneticisi.SorumluEkle(yeniSorumlu);
                }

                MessageBox.Show(
                    $"✅ Kayıt başarılı!\n\n" +
                    $"E-posta: {txtEmail.Text}\n\n" +
                    $"Şimdi giriş yapabilirsiniz.",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Giriş ekranına dön
                FormLogin login = new FormLogin();
                this.Hide();
                login.Show();
                login.FormClosed += (s, args) => this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
