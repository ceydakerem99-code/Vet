using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class FormBildirimler : Form
    {
        private ListBox lstBildirimler;
        private Button btnAnaSayfa;
        private Button btnTemizle;

        public FormBildirimler()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Bildirimler";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(62, 166, 107);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "🔔 Bildirimler ve Hatırlatmalar";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Bildirim listesi
            lstBildirimler = new ListBox();
            lstBildirimler.Location = new Point(30, 70);
            lstBildirimler.Size = new Size(620, 380);
            lstBildirimler.Font = new Font("Consolas", 10);
            this.Controls.Add(lstBildirimler);

            // Ana Sayfa butonu
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(30, 465);
            btnAnaSayfa.Size = new Size(150, 40);
            btnAnaSayfa.BackColor = Color.FromArgb(255, 216, 63);
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Click += BtnAnaSayfa_Click;
            this.Controls.Add(btnAnaSayfa);

            // Temizle butonu
            btnTemizle = new Button();
            btnTemizle.Text = "Bildirimleri Temizle";
            btnTemizle.Location = new Point(500, 465);
            btnTemizle.Size = new Size(150, 40);
            btnTemizle.BackColor = Color.LightCoral;
            btnTemizle.FlatStyle = FlatStyle.Flat;
            btnTemizle.Click += (s, e) => { lstBildirimler.Items.Clear(); lstBildirimler.Items.Add("Bildirimler temizlendi."); };
            this.Controls.Add(btnTemizle);

            this.Load += (s, e) => BildirimleriYukle();
        }

        private void BildirimleriYukle()
        {
            var veri = VeriYoneticisi.Instance;
            lstBildirimler.Items.Clear();
            
            DateTime bugun = DateTime.Now;
            
            // Başlık
            lstBildirimler.Items.Add("═══════════════════════════════════════════════════════════════");
            lstBildirimler.Items.Add($"  📅 Tarih: {bugun:dd.MM.yyyy HH:mm}");
            lstBildirimler.Items.Add("═══════════════════════════════════════════════════════════════");
            lstBildirimler.Items.Add("");
            
            // Bekleyen randevular
            var bekleyenRandevular = veri.BekleyenRandevular();
            if (bekleyenRandevular.Count > 0)
            {
                lstBildirimler.Items.Add("⏳ BEKLEYEN RANDEVULAR");
                lstBildirimler.Items.Add("───────────────────────────────────────────────────────────────");
                foreach (var r in bekleyenRandevular)
                {
                    lstBildirimler.Items.Add($"  ⚠️ Randevu #{r.Id} - {r.RandevuTarihi:dd.MM.yyyy} - Onay bekliyor");
                }
                lstBildirimler.Items.Add("");
            }
            
            // Yaklaşan randevular 
            lstBildirimler.Items.Add("📆 YAKLAŞAN RANDEVULAR (3 gün içinde)");
            lstBildirimler.Items.Add("───────────────────────────────────────────────────────────────");
            int yaklasanSayisi = 0;
            foreach (var r in veri.Randevular)
            {
                int kalanGun = (r.RandevuTarihi.Date - bugun.Date).Days;
                if (kalanGun >= 0 && kalanGun <= 3 && r.Durum == Enums.RandevuDurumu.Onaylandi)
                {
                    string mesaj = kalanGun == 0 ? "BUGÜN!" : $"{kalanGun} gün kaldı";
                    lstBildirimler.Items.Add($"  🕐 Randevu #{r.Id} - {r.RandevuTarihi:dd.MM.yyyy} - {mesaj}");
                    yaklasanSayisi++;
                }
            }
            if (yaklasanSayisi == 0)
                lstBildirimler.Items.Add("  ✓ Yaklaşan randevu yok.");
            lstBildirimler.Items.Add("");
            
            // Aşı hatırlatmaları
            lstBildirimler.Items.Add("💉 AŞI HATIRLATIMLARI");
            lstBildirimler.Items.Add("───────────────────────────────────────────────────────────────");
            int asiHatirlatmaSayisi = 0;
            
            foreach (var hayvan in veri.EvcilHayvanlar)
            {
                foreach (var asi in hayvan.Asilar)
                {
                    if (asi.HatirlatmaAktifMi && asi.SonrakiAsiTarihi.HasValue)
                    {
                        int kalanGun = (asi.SonrakiAsiTarihi.Value.Date - bugun.Date).Days;
                        if (kalanGun >= -7 && kalanGun <= 7)
                        {
                            string durum = kalanGun < 0 ? "GEÇMİŞ!" : (kalanGun == 0 ? "BUGÜN!" : $"{kalanGun} gün kaldı");
                            lstBildirimler.Items.Add($"  💉 {hayvan.Ad} - {asi.AsiAdi} - {durum}");
                            asiHatirlatmaSayisi++;
                        }
                    }
                }
            }
            if (asiHatirlatmaSayisi == 0)
                lstBildirimler.Items.Add("  ✓ Yaklaşan aşı hatırlatması yok.");
            lstBildirimler.Items.Add("");
            
            // Tedavi altındaki hayvanlar
            lstBildirimler.Items.Add("🏥 TEDAVİ ALTINDAKİ HAYVANLAR");
            lstBildirimler.Items.Add("───────────────────────────────────────────────────────────────");
            int tedaviSayisi = 0;
            
            foreach (var hayvan in veri.EvcilHayvanlar)
            {
                if (hayvan.SaglikDurumu == "Tedavi Altında" || hayvan.SaglikDurumu == "Kritik")
                {
                    lstBildirimler.Items.Add($"  🏥 {hayvan.Ad} - Durum: {hayvan.SaglikDurumu}");
                    tedaviSayisi++;
                }
            }
            foreach (var hayvan in veri.SokakHayvanlari)
            {
                if (!hayvan.TedaviOnayliMi && hayvan.SaglikDurumu != "Sağlıklı")
                {
                    lstBildirimler.Items.Add($"  ⚠️ {hayvan.Ad} (Sokak) - Tedavi onayı bekliyor");
                    tedaviSayisi++;
                }
            }
            if (tedaviSayisi == 0)
                lstBildirimler.Items.Add("  ✓ Tedavi altında hayvan yok.");
            
            lstBildirimler.Items.Add("");
            lstBildirimler.Items.Add("═══════════════════════════════════════════════════════════════");
        }

        private void BtnAnaSayfa_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
            form.FormClosed += (s, args) => this.Close();
        }
    }
}
