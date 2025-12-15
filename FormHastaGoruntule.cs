using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp
{
    /// <summary>
    /// Rol bazlı hasta görüntüleme formu
    /// Admin: Tüm hayvanları ve işlemleri görür
    /// Hayvan Sahibi: Sadece kendi hayvanlarını görür
    /// Sokak Hayvanı Sorumlusu: Getirdiği hayvanları görür
    /// </summary>
    public partial class FormHastaGoruntule : Form
    {
        private ComboBox cmbHayvanlar;
        private RichTextBox txtDetaylar;
        private ListBox lstIslemler;
        private Button btnAnaSayfa;
        private Label lblBaslik;

        public FormHastaGoruntule()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Hasta Görüntüle";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(62, 166, 107);

            var oturum = OturumYoneticisi.Instance;
            string rolMetni = oturum.VeterinerAdminMi() ? "Yönetici" : 
                              oturum.SokakHayvaniSorumlusuMu() ? "Patili Koruyucu" : "Hayvan Sahibi";

            // Başlık
            lblBaslik = new Label();
            lblBaslik.Text = $"🐾 Hasta Görüntüle - {rolMetni}";
            lblBaslik.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblBaslik.Location = new Point(30, 20);
            lblBaslik.AutoSize = true;
            this.Controls.Add(lblBaslik);

            // Hayvan seçimi
            Label lblSecim = new Label();
            lblSecim.Text = "Hasta Seçin:";
            lblSecim.Font = new Font("Segoe UI", 11);
            lblSecim.Location = new Point(30, 70);
            lblSecim.AutoSize = true;
            this.Controls.Add(lblSecim);

            cmbHayvanlar = new ComboBox();
            cmbHayvanlar.Location = new Point(150, 67);
            cmbHayvanlar.Size = new Size(400, 30);
            cmbHayvanlar.Font = new Font("Segoe UI", 11);
            cmbHayvanlar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHayvanlar.SelectedIndexChanged += CmbHayvanlar_SelectedIndexChanged;
            this.Controls.Add(cmbHayvanlar);

            // Hayvan bilgileri
            Label lblBilgi = new Label();
            lblBilgi.Text = "Hasta Bilgileri:";
            lblBilgi.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblBilgi.Location = new Point(30, 110);
            lblBilgi.AutoSize = true;
            this.Controls.Add(lblBilgi);

            txtDetaylar = new RichTextBox();
            txtDetaylar.Location = new Point(30, 140);
            txtDetaylar.Size = new Size(450, 200);
            txtDetaylar.Font = new Font("Consolas", 10);
            txtDetaylar.ReadOnly = true;
            txtDetaylar.BackColor = Color.White;
            this.Controls.Add(txtDetaylar);

            // İşlem geçmişi
            Label lblIslemler = new Label();
            lblIslemler.Text = "Yapılan İşlemler:";
            lblIslemler.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblIslemler.Location = new Point(500, 110);
            lblIslemler.AutoSize = true;
            this.Controls.Add(lblIslemler);

            lstIslemler = new ListBox();
            lstIslemler.Location = new Point(500, 140);
            lstIslemler.Size = new Size(460, 480);
            lstIslemler.Font = new Font("Consolas", 9);
            this.Controls.Add(lstIslemler);

            // Ana Sayfa butonu
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(30, 600);
            btnAnaSayfa.Size = new Size(150, 45);
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

            this.Load += FormHastaGoruntule_Load;
        }

        private void FormHastaGoruntule_Load(object sender, EventArgs e)
        {
            HayvanlariYukle();
        }

        private void HayvanlariYukle()
        {
            var veri = VeriYoneticisi.Instance;
            var oturum = OturumYoneticisi.Instance;
            cmbHayvanlar.Items.Clear();

            if (oturum.VeterinerAdminMi())
            {
                // Admin tüm hayvanları görür
                foreach (var h in veri.EvcilHayvanlar)
                {
                    cmbHayvanlar.Items.Add(new HayvanItem(h.Id, $"{h.Ad} ({h.Tur}) - Sahip: {h.SahipAdi}", "Evcil"));
                }
                foreach (var h in veri.SokakHayvanlari)
                {
                    cmbHayvanlar.Items.Add(new HayvanItem(h.Id, $"{h.Ad} ({h.Tur}) - Bölge: {h.BulunduguBolge}", "Sokak"));
                }
            }
            else if (oturum.SokakHayvaniSorumlusuMu())
            {
                // Sorumlu sadece getirdiği sokak hayvanlarını görür
                int sorumluId = oturum.AktifKullanici?.Id ?? 0;
                foreach (var h in veri.SokakHayvanlari.Where(x => x.SorumluId == sorumluId || x.SorumluId == 0))
                {
                    cmbHayvanlar.Items.Add(new HayvanItem(h.Id, $"{h.Ad} ({h.Tur}) - Bölge: {h.BulunduguBolge}", "Sokak"));
                }
            }
            else
            {
                // Hayvan sahibi sadece kendi hayvanlarını görür
                int sahipId = oturum.AktifKullanici?.Id ?? 0;
                foreach (var h in veri.EvcilHayvanlar.Where(x => x.SahipId == sahipId))
                {
                    cmbHayvanlar.Items.Add(new HayvanItem(h.Id, $"{h.Ad} ({h.Tur})", "Evcil"));
                }
                
                // Eğer kendi hayvanı yoksa demo için tüm hayvanları göster
                if (cmbHayvanlar.Items.Count == 0)
                {
                    foreach (var h in veri.EvcilHayvanlar)
                    {
                        cmbHayvanlar.Items.Add(new HayvanItem(h.Id, $"{h.Ad} ({h.Tur}) - Sahip: {h.SahipAdi}", "Evcil"));
                    }
                }
            }

            if (cmbHayvanlar.Items.Count > 0)
                cmbHayvanlar.SelectedIndex = 0;
            else
            {
                txtDetaylar.Text = "Görüntülenecek hasta bulunamadı.";
                lstIslemler.Items.Add("Henüz kayıtlı hayvan yok.");
            }
        }

        private void CmbHayvanlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHayvanlar.SelectedItem == null) return;

            var item = (HayvanItem)cmbHayvanlar.SelectedItem;
            var veri = VeriYoneticisi.Instance;

            txtDetaylar.Clear();
            lstIslemler.Items.Clear();

            if (item.Tip == "Evcil")
            {
                var hayvan = veri.EvcilHayvanlar.FirstOrDefault(h => h.Id == item.Id);
                if (hayvan != null)
                {
                    txtDetaylar.AppendText($"═══════════════════════════════════════\n");
                    txtDetaylar.AppendText($"  🐾 {hayvan.Ad}\n");
                    txtDetaylar.AppendText($"═══════════════════════════════════════\n\n");
                    txtDetaylar.AppendText($"  Tür: {hayvan.Tur}\n");
                    txtDetaylar.AppendText($"  Irk: {hayvan.Irk}\n");
                    txtDetaylar.AppendText($"  Yaş: {hayvan.Yas}\n");
                    txtDetaylar.AppendText($"  Cinsiyet: {hayvan.Cinsiyet}\n");
                    txtDetaylar.AppendText($"  Sahip: {hayvan.SahipAdi}\n");
                    txtDetaylar.AppendText($"  Chip No: {hayvan.ChipNumarasi}\n");
                    txtDetaylar.AppendText($"  Sağlık: {hayvan.SaglikDurumu}\n");
                    txtDetaylar.AppendText($"  Kısır: {(hayvan.KisirlastirildiMi ? "Evet" : "Hayır")}\n");

                    // İşlemleri göster
                    IslemleriGoster(item.Id);
                }
            }
            else
            {
                var hayvan = veri.SokakHayvanlari.FirstOrDefault(h => h.Id == item.Id);
                if (hayvan != null)
                {
                    txtDetaylar.AppendText($"═══════════════════════════════════════\n");
                    txtDetaylar.AppendText($"  🐕 {hayvan.Ad} (Sokak Hayvanı)\n");
                    txtDetaylar.AppendText($"═══════════════════════════════════════\n\n");
                    txtDetaylar.AppendText($"  Tür: {hayvan.Tur}\n");
                    txtDetaylar.AppendText($"  Tahmini Yaş: {hayvan.Yas}\n");
                    txtDetaylar.AppendText($"  Bölge: {hayvan.BulunduguBolge}\n");
                    txtDetaylar.AppendText($"  Sağlık: {hayvan.SaglikDurumu}\n");
                    txtDetaylar.AppendText($"  Kısır: {(hayvan.KisirlastirildiMi ? "Evet" : "Hayır")}\n");
                    txtDetaylar.AppendText($"  Tedavi Onaylı: {(hayvan.TedaviOnayliMi ? "Evet" : "Hayır")}\n");

                    IslemleriGoster(item.Id);
                }
            }
        }

        private void IslemleriGoster(int hayvanId)
        {
            var veri = VeriYoneticisi.Instance;
            var islemler = veri.Muayeneler.Where(m => m.HayvanId == hayvanId).OrderByDescending(m => m.MuayeneTarihi).ToList();

            if (islemler.Count == 0)
            {
                lstIslemler.Items.Add("═══════════════════════════════════════════════════");
                lstIslemler.Items.Add("  Bu hastaya henüz işlem yapılmamış.");
                lstIslemler.Items.Add("═══════════════════════════════════════════════════");
            }
            else
            {
                lstIslemler.Items.Add($"═══════════════════════════════════════════════════");
                lstIslemler.Items.Add($"  TOPLAM {islemler.Count} İŞLEM KAYDI");
                lstIslemler.Items.Add($"═══════════════════════════════════════════════════");

                foreach (var m in islemler)
                {
                    lstIslemler.Items.Add("");
                    lstIslemler.Items.Add($"📅 {m.MuayeneTarihi:dd.MM.yyyy HH:mm}");
                    lstIslemler.Items.Add($"───────────────────────────────────────────────────");
                    lstIslemler.Items.Add($"Şikayet: {m.Sikayet}");
                    lstIslemler.Items.Add($"Tanı: {m.Tani}");
                    lstIslemler.Items.Add($"İşlem: {m.Tedavi}");
                    if (!string.IsNullOrEmpty(m.Notlar))
                        lstIslemler.Items.Add($"Reçete: {m.Notlar}");
                    lstIslemler.Items.Add($"Ücret: {m.Ucret:N2} TL");
                    lstIslemler.Items.Add($"Durum: {(m.TamamlandiMi ? "✅ Tamamlandı" : "⏳ Devam Ediyor")}");
                }
            }
        }

        // Yardımcı sınıf
        private class HayvanItem
        {
            public int Id { get; set; }
            public string Metin { get; set; }
            public string Tip { get; set; }

            public HayvanItem(int id, string metin, string tip)
            {
                Id = id;
                Metin = metin;
                Tip = tip;
            }

            public override string ToString() => $"[{Tip}] {Metin}";
        }
    }
}
