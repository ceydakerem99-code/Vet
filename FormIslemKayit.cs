using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;
using System.Collections.Generic;

namespace VeterinerProjectApp
{
    /// <summary>
    /// Doktor/Admin için işlem kayıt formu
    /// </summary>
    public partial class FormIslemKayit : Form
    {
        private ComboBox cmbHayvan;
        private TextBox txtSikayet;
        private TextBox txtTani;
        private TextBox txtYapilanIslem;
        private TextBox txtRecete;
        private TextBox txtUcret;
        private Button btnKaydet;
        private Button btnAnaSayfa;
        private CheckBox chkKisir;
        private CheckBox chkAsiTam;

        public FormIslemKayit()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "İşlem Kayıt - Doktor Paneli";
            this.Size = new Size(800, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(62, 166, 107);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "🩺 Hasta İşlem Kayıt";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(280, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            int labelX = 50;
            int inputX = 200;
            int inputWidth = 500;
            int y = 80;
            int spacing = 70;

            // Hayvan seçimi
            AddLabel("Hasta Seçin:", labelX, y);
            cmbHayvan = new ComboBox();
            cmbHayvan.Location = new Point(inputX, y);
            cmbHayvan.Size = new Size(inputWidth, 30);
            cmbHayvan.Font = new Font("Segoe UI", 11);
            cmbHayvan.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cmbHayvan);

            // Şikayet
            y += spacing;
            AddLabel("Şikayet:", labelX, y);
            txtSikayet = new TextBox();
            txtSikayet.Location = new Point(inputX, y);
            txtSikayet.Size = new Size(inputWidth, 60);
            txtSikayet.Multiline = true;
            txtSikayet.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtSikayet);

            // Tanı
            y += spacing + 20;
            AddLabel("Tanı:", labelX, y);
            txtTani = new TextBox();
            txtTani.Location = new Point(inputX, y);
            txtTani.Size = new Size(inputWidth, 60);
            txtTani.Multiline = true;
            txtTani.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtTani);

            // Yapılan İşlem
            y += spacing + 20;
            AddLabel("Yapılan İşlem:", labelX, y);
            txtYapilanIslem = new TextBox();
            txtYapilanIslem.Location = new Point(inputX, y);
            txtYapilanIslem.Size = new Size(inputWidth, 100);
            txtYapilanIslem.Multiline = true;
            txtYapilanIslem.Font = new Font("Segoe UI", 10);
            txtYapilanIslem.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtYapilanIslem);

            // Reçete
            y += spacing + 60;
            AddLabel("Reçete/İlaçlar:", labelX, y);
            txtRecete = new TextBox();
            txtRecete.Location = new Point(inputX, y);
            txtRecete.Size = new Size(inputWidth, 60);
            txtRecete.Multiline = true;
            txtRecete.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtRecete);

            // Ücret
            y += spacing + 20;
            AddLabel("Ücret (TL):", labelX, y);
            txtUcret = new TextBox();
            txtUcret.Location = new Point(inputX, y);
            txtUcret.Size = new Size(150, 30);
            txtUcret.Font = new Font("Segoe UI", 11);
            txtUcret.Text = "0";
            this.Controls.Add(txtUcret);

            // Kısırlık ve Aşı Checkbox'ları - Doktor Düzenleyebilir
            y += 50;
            chkKisir = new CheckBox();
            chkKisir.Text = "Kısırlaştırıldı";
            chkKisir.Location = new Point(inputX, y);
            chkKisir.AutoSize = true;
            chkKisir.Font = new Font("Segoe UI", 10);
            chkKisir.ForeColor = Color.White;
            this.Controls.Add(chkKisir);

            chkAsiTam = new CheckBox();
            chkAsiTam.Text = "Aşıları Tamamlandı";
            chkAsiTam.Location = new Point(inputX + 150, y);
            chkAsiTam.AutoSize = true;
            chkAsiTam.Font = new Font("Segoe UI", 10);
            chkAsiTam.ForeColor = Color.White;
            this.Controls.Add(chkAsiTam);

            // Kaydet butonu
            btnKaydet = new Button();
            btnKaydet.Text = "💾 İşlemi Kaydet";
            btnKaydet.Location = new Point(inputX, y + 60);
            btnKaydet.Size = new Size(200, 50);
            btnKaydet.BackColor = Color.FromArgb(100, 200, 100);
            btnKaydet.ForeColor = Color.White;
            btnKaydet.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnKaydet.FlatStyle = FlatStyle.Flat;
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            // Ana Sayfa butonu
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(inputX + 220, y + 60);
            btnAnaSayfa.Size = new Size(150, 50);
            btnAnaSayfa.BackColor = Color.FromArgb(255, 216, 63);
            btnAnaSayfa.Font = new Font("Segoe UI", 11);
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Click += (s, e) => {
                Form1 form = new Form1();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnAnaSayfa);

            this.Load += FormIslemKayit_Load;
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 11);
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);
        }

        private void FormIslemKayit_Load(object sender, EventArgs e)
        {
            // Oturum kontrolü - kullanıcı kayıtlı mı?
            var oturum = OturumYoneticisi.Instance;
            
            if (!oturum.OturumAktifMi || oturum.AktifKullanici == null)
            {
                // Kullanıcı giriş yapmamış
                var result = MessageBox.Show(
                    "⚠️ İşlem kaydı için giriş yapmanız gerekmektedir!\n\n" +
                    "Hesabınız var mı?\n" +
                    "• Evet → Giriş ekranına yönlendirileceksiniz\n" +
                    "• Hayır → Kayıt ekranına yönlendirileceksiniz",
                    "Giriş Gerekli",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Giriş ekranına yönlendir
                    FormLogin loginForm = new FormLogin();
                    this.Hide();
                    loginForm.Show();
                    loginForm.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    // Kayıt ekranına yönlendir
                    FormKayit kayitForm = new FormKayit();
                    this.Hide();
                    kayitForm.Show();
                    kayitForm.FormClosed += (s, args) => this.Close();
                }
                return;
            }
            
            // Sadece admin veya veteriner işlem kaydı yapabilir
            if (!oturum.VeterinerAdminMi())
            {
                MessageBox.Show(
                    "⚠️ İşlem kaydı sadece Klinik Yöneticisi veya Veteriner tarafından yapılabilir!\n\n" +
                    "Pet kullanıcısı olarak bu bölüme erişim yetkiniz yoktur.",
                    "Erişim Engellendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                Form1 anaForm = new Form1();
                this.Hide();
                anaForm.Show();
                anaForm.FormClosed += (s, args) => this.Close();
                return;
            }
            
            // Hayvanları yükle
            var veri = VeriYoneticisi.Instance;
            cmbHayvan.Items.Clear();

            foreach (var h in veri.EvcilHayvanlar)
            {
                cmbHayvan.Items.Add($"[Evcil] {h.Id} - {h.Ad} ({h.Tur}) - Sahip: {h.SahipAdi}");
            }

            foreach (var h in veri.SokakHayvanlari)
            {
                cmbHayvan.Items.Add($"[Sokak] {h.Id} - {h.Ad} ({h.Tur}) - Bölge: {h.BulunduguBolge}");
            }

            if (cmbHayvan.Items.Count > 0)
                cmbHayvan.SelectedIndex = 0;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbHayvan.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir hasta seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtYapilanIslem.Text))
            {
                MessageBox.Show("Lütfen yapılan işlemi yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veri = VeriYoneticisi.Instance;
            var oturum = OturumYoneticisi.Instance;

            // Hayvan ID'sini al
            string secim = cmbHayvan.SelectedItem.ToString();
            int hayvanId = 0;
            try
            {
                string idStr = secim.Split('-')[0].Replace("[Evcil]", "").Replace("[Sokak]", "").Trim();
                hayvanId = int.Parse(idStr);
            }
            catch { }

            // Yeni muayene oluştur
            decimal ucret = 0;
            decimal.TryParse(txtUcret.Text, out ucret);

            var muayene = new Muayene();
            muayene.Id = veri.Muayeneler.Count + 1;
            muayene.HayvanId = hayvanId;
            muayene.VeterinerId = oturum.AktifKullanici?.Id ?? 0;
            muayene.MuayeneTarihi = DateTime.Now;
            muayene.Sikayet = txtSikayet.Text;
            muayene.Tani = txtTani.Text;
            muayene.Tedavi = txtYapilanIslem.Text;
            muayene.Notlar = txtRecete.Text;
            muayene.Ucret = ucret;
            muayene.TamamlandiMi = true;

            veri.MuayeneEkle(muayene);

            // Hayvan bilgilerini güncelle (kısırlık, aşı)
            if (secim.Contains("[Evcil]"))
            {
                var hayvan = veri.EvcilHayvanlar.FirstOrDefault(h => h.Id == hayvanId);
                if (hayvan != null)
                {
                    if (chkKisir.Checked && !hayvan.KisirlastirildiMi)
                        hayvan.Kisirlastir();
                }
            }
            else if (secim.Contains("[Sokak]"))
            {
                var hayvan = veri.SokakHayvanlari.FirstOrDefault(h => h.Id == hayvanId);
                if (hayvan != null)
                {
                    if (chkKisir.Checked && !hayvan.KisirlastirildiMi)
                        hayvan.Kisirlastir();
                }
            }

            MessageBox.Show(
                $"✅ İşlem başarıyla kaydedildi!\n\n" +
                $"Hasta ID: {hayvanId}\n" +
                $"İşlem: {txtYapilanIslem.Text.Substring(0, Math.Min(50, txtYapilanIslem.Text.Length))}...\n" +
                $"Kısırlık: {(chkKisir.Checked ? "Güncellendi" : "Değişiklik yok")}\n" +
                $"Ücret: {ucret:N2} TL",
                "Başarılı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Formu temizle
            txtSikayet.Clear();
            txtTani.Clear();
            txtYapilanIslem.Clear();
            txtRecete.Clear();
            txtUcret.Text = "0";
            chkKisir.Checked = false;
            chkAsiTam.Checked = false;
        }
    }
}
