using System;
using System.Windows.Forms;
using VeterinerProjectApp.Services;

namespace VeterinerProjectApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UstMenuButonlariEkle();
            

            var db = VeritabaniServisi.Instance;
    
            var oturum = OturumYoneticisi.Instance;
            if (oturum.OturumAktifMi && oturum.AktifKullanici != null)
            {
                this.Text = $"Veteriner Klinik - Hoş Geldiniz {oturum.AktifKullanici.TamAdGetir()}";
            }
        }

        private void UstMenuButonlariEkle()
        {
            int startX = 50;
            int y = 10;
            int buttonWidth = 100;
            int buttonHeight = 35;
            int spacing = 10;

        
            Button btnArama = new Button();
            btnArama.Text = "🔍 Arama";
            btnArama.Location = new System.Drawing.Point(startX, y);
            btnArama.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnArama.BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
            btnArama.FlatStyle = FlatStyle.Flat;
            btnArama.Font = new System.Drawing.Font("Segoe UI", 9);
            btnArama.Click += (s, e) => {
                FormArama form = new FormArama();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnArama);

            // Bildirimler
            Button btnBildirim = new Button();
            btnBildirim.Text = "🔔 Bildirim";
            btnBildirim.Location = new System.Drawing.Point(startX + (buttonWidth + spacing) * 1, y);
            btnBildirim.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnBildirim.BackColor = System.Drawing.Color.FromArgb(255, 230, 200);
            btnBildirim.FlatStyle = FlatStyle.Flat;
            btnBildirim.Font = new System.Drawing.Font("Segoe UI", 9);
            btnBildirim.Click += (s, e) => {
                FormBildirimler form = new FormBildirimler();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnBildirim);

            // Rapor
            Button btnRapor = new Button();
            btnRapor.Text = "📊 Rapor";
            btnRapor.Location = new System.Drawing.Point(startX + (buttonWidth + spacing) * 2, y);
            btnRapor.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnRapor.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
            btnRapor.FlatStyle = FlatStyle.Flat;
            btnRapor.Font = new System.Drawing.Font("Segoe UI", 9);
            btnRapor.Click += (s, e) => {
                FormRapor form = new FormRapor();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnRapor);

            // Veritabanı 
            Button btnVeritabani = new Button();
            btnVeritabani.Text = "💾 Kaydet";
            btnVeritabani.Location = new System.Drawing.Point(startX + (buttonWidth + spacing) * 3, y);
            btnVeritabani.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnVeritabani.BackColor = System.Drawing.Color.FromArgb(230, 200, 255);
            btnVeritabani.FlatStyle = FlatStyle.Flat;
            btnVeritabani.Font = new System.Drawing.Font("Segoe UI", 9);
            btnVeritabani.Click += (s, e) => {
                VerileriKaydet();
            };
            this.Controls.Add(btnVeritabani);

            // Çıkış 
            Button btnCikis = new Button();
            btnCikis.Text = "🚪 Çıkış";
            btnCikis.Location = new System.Drawing.Point(startX + (buttonWidth + spacing) * 4, y);
            btnCikis.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnCikis.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            btnCikis.FlatStyle = FlatStyle.Flat;
            btnCikis.Font = new System.Drawing.Font("Segoe UI", 9);
            btnCikis.Click += (s, e) => {
                OturumYoneticisi.Instance.CikisYap();
                FormLogin login = new FormLogin();
                this.Hide();
                login.Show();
                login.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnCikis);
        }

        private void VerileriKaydet()
        {
            try
            {
                var veriYoneticisi = VeriYoneticisi.Instance;
                var dbServisi = VeritabaniServisi.Instance;
                
                int kayitSayisi = 0;
                
                // Hayvan ksyıt
                foreach (var hayvan in veriYoneticisi.EvcilHayvanlar)
                {
                    dbServisi.HayvanKaydet(hayvan);
                    kayitSayisi++;
                }
                
                // Randevu kayıt
                foreach (var randevu in veriYoneticisi.Randevular)
                {
                    dbServisi.RandevuKaydet(randevu);
                    kayitSayisi++;
                }
                
                MessageBox.Show(
                    $"Veriler veritabanına kaydedildi!\n\n" +
                    $"Toplam {kayitSayisi} kayıt.\n\n" +
                    $"Veritabanı: VeterinerKlinik.db",
                    "Başarılı", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Sadece Admin erişebilir
            var oturum = OturumYoneticisi.Instance;
            if (!oturum.VeterinerAdminMi())
            {
                MessageBox.Show(
                    "⚠️ Bu alana sadece Klinik Yöneticisi erişebilir!\n\n" +
                    "Lütfen yönetici hesabıyla giriş yapın.",
                    "Erişim Engellendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            Form2 yeniForm = new Form2();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 yeniForm = new Form3();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // sadece admin veya sokak hayvanı sorumlusu erişebilir
            var oturum = OturumYoneticisi.Instance;
            if (!oturum.VeterinerAdminMi() && !oturum.SokakHayvaniSorumlusuMu())
            {
                MessageBox.Show(
                    "⚠️ Bu alana sadece Patili Koruyucu veya Yönetici erişebilir!\n\n" +
                    "Pet kullanıcısı olarak bu bölüme erişim yetkiniz yok.",
                    "Erişim Engellendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            Form4 yeniForm = new Form4();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 yeniForm = new Form5();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 yeniForm = new Form6();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 yeniForm = new Form7();
            this.Hide();                   
            yeniForm.Show();               
            yeniForm.FormClosed += (s, args) => this.Close();
        }

    }
}
