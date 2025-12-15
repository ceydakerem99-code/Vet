using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class FormRapor : Form
    {
        private ComboBox cmbRaporTuru;
        private DateTimePicker dtpBaslangic;
        private DateTimePicker dtpBitis;
        private RichTextBox txtRapor;
        private Button btnOlustur;
        private Button btnKaydet;
        private Button btnAnaSayfa;

        public FormRapor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Rapor Oluştur";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 251, 224);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "📊 Rapor Oluşturma";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Rapor türü
            Label lblTur = new Label();
            lblTur.Text = "Rapor Türü:";
            lblTur.Location = new Point(30, 70);
            lblTur.AutoSize = true;
            this.Controls.Add(lblTur);

            cmbRaporTuru = new ComboBox();
            cmbRaporTuru.Location = new Point(30, 95);
            cmbRaporTuru.Size = new Size(200, 30);
            cmbRaporTuru.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRaporTuru.Items.AddRange(new[] { 
                "Genel Özet Raporu", 
                "Muayene Raporu", 
                "Randevu Raporu", 
                "Hayvan Listesi",
                "Aşı Takvimi",
                "Finansal Rapor"
            });
            cmbRaporTuru.SelectedIndex = 0;
            this.Controls.Add(cmbRaporTuru);

            // Tarih aralığı
            Label lblTarih = new Label();
            lblTarih.Text = "Tarih Aralığı:";
            lblTarih.Location = new Point(260, 70);
            lblTarih.AutoSize = true;
            this.Controls.Add(lblTarih);

            dtpBaslangic = new DateTimePicker();
            dtpBaslangic.Location = new Point(260, 95);
            dtpBaslangic.Size = new Size(150, 30);
            dtpBaslangic.Value = DateTime.Now.AddMonths(-1);
            this.Controls.Add(dtpBaslangic);

            Label lblTo = new Label();
            lblTo.Text = "-";
            lblTo.Location = new Point(420, 100);
            lblTo.AutoSize = true;
            this.Controls.Add(lblTo);

            dtpBitis = new DateTimePicker();
            dtpBitis.Location = new Point(440, 95);
            dtpBitis.Size = new Size(150, 30);
            this.Controls.Add(dtpBitis);

            // Oluştur butonu
            btnOlustur = new Button();
            btnOlustur.Text = "Rapor Oluştur";
            btnOlustur.Location = new Point(620, 93);
            btnOlustur.Size = new Size(120, 35);
            btnOlustur.BackColor = Color.FromArgb(255, 216, 63);
            btnOlustur.FlatStyle = FlatStyle.Flat;
            btnOlustur.Click += BtnOlustur_Click;
            this.Controls.Add(btnOlustur);

            // Rapor içeriği
            txtRapor = new RichTextBox();
            txtRapor.Location = new Point(30, 150);
            txtRapor.Size = new Size(820, 440);
            txtRapor.Font = new Font("Consolas", 10);
            txtRapor.ReadOnly = true;
            txtRapor.BackColor = Color.White;
            this.Controls.Add(txtRapor);

            // Kaydet butonu
            btnKaydet = new Button();
            btnKaydet.Text = "Dosyaya Kaydet";
            btnKaydet.Location = new Point(700, 610);
            btnKaydet.Size = new Size(150, 40);
            btnKaydet.BackColor = Color.LightGreen;
            btnKaydet.FlatStyle = FlatStyle.Flat;
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            // Ana Sayfa butonu
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(30, 610);
            btnAnaSayfa.Size = new Size(150, 40);
            btnAnaSayfa.BackColor = Color.FromArgb(255, 216, 63);
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Click += BtnAnaSayfa_Click;
            this.Controls.Add(btnAnaSayfa);
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            string raporTuru = cmbRaporTuru.SelectedItem?.ToString() ?? "";
            var veri = VeriYoneticisi.Instance;
            StringBuilder rapor = new StringBuilder();
            
            rapor.AppendLine("╔════════════════════════════════════════════════════════════════════╗");
            rapor.AppendLine($"║  VETERİNER KLİNİK - {raporTuru.ToUpper()}");
            rapor.AppendLine("╠════════════════════════════════════════════════════════════════════╣");
            rapor.AppendLine($"║  Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}");
            rapor.AppendLine($"║  Tarih Aralığı: {dtpBaslangic.Value:dd.MM.yyyy} - {dtpBitis.Value:dd.MM.yyyy}");
            rapor.AppendLine("╚════════════════════════════════════════════════════════════════════╝");
            rapor.AppendLine();

            switch (raporTuru)
            {
                case "Genel Özet Raporu":
                    GenelOzetRaporu(rapor, veri);
                    break;
                case "Muayene Raporu":
                    MuayeneRaporu(rapor, veri);
                    break;
                case "Randevu Raporu":
                    RandevuRaporu(rapor, veri);
                    break;
                case "Hayvan Listesi":
                    HayvanListesiRaporu(rapor, veri);
                    break;
                case "Aşı Takvimi":
                    AsiTakvimiRaporu(rapor, veri);
                    break;
                case "Finansal Rapor":
                    FinansalRapor(rapor, veri);
                    break;
            }

            txtRapor.Text = rapor.ToString();
        }

        private void GenelOzetRaporu(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  📊 GENEL İSTATİSTİKLER                                             │");
            rapor.AppendLine("├─────────────────────────────────────────────────────────────────────┤");
            rapor.AppendLine($"│  Toplam Evcil Hayvan      : {veri.EvcilHayvanlar.Count,5}                                │");
            rapor.AppendLine($"│  Toplam Sokak Hayvanı     : {veri.SokakHayvanlari.Count,5}                                │");
            rapor.AppendLine($"│  Toplam Hayvan Sahibi     : {veri.HayvanSahipleri.Count,5}                                │");
            rapor.AppendLine($"│  Toplam Muayene           : {veri.Muayeneler.Count,5}                                │");
            rapor.AppendLine($"│  Toplam Randevu           : {veri.Randevular.Count,5}                                │");
            rapor.AppendLine($"│  Bekleyen Randevu         : {veri.BekleyenRandevular().Count,5}                                │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
        }

        private void MuayeneRaporu(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  🩺 MUAYENE RAPORU                                                  │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
            rapor.AppendLine();
            
            if (veri.Muayeneler.Count == 0)
            {
                rapor.AppendLine("  Kayıtlı muayene bulunmuyor.");
            }
            else
            {
                foreach (var m in veri.Muayeneler)
                {
                    rapor.AppendLine($"  ► Muayene #{m.Id}");
                    rapor.AppendLine($"    Tarih: {m.MuayeneTarihi:dd.MM.yyyy}");
                    rapor.AppendLine($"    Şikayet: {m.Sikayet}");
                    rapor.AppendLine($"    Tanı: {m.Tani}");
                    rapor.AppendLine($"    Durum: {(m.TamamlandiMi ? "Tamamlandı" : "Devam Ediyor")}");
                    rapor.AppendLine();
                }
            }
        }

        private void RandevuRaporu(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  📅 RANDEVU RAPORU                                                  │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
            rapor.AppendLine();
            
            foreach (var r in veri.Randevular)
            {
                rapor.AppendLine($"  ► Randevu #{r.Id} - {r.RandevuTarihi:dd.MM.yyyy} {r.RandevuSaati:hh\\:mm}");
                rapor.AppendLine($"    Durum: {r.DurumMetni()}");
                rapor.AppendLine($"    Şikayet: {r.Sikayet}");
                rapor.AppendLine();
            }
            
            if (veri.Randevular.Count == 0)
                rapor.AppendLine("  Kayıtlı randevu bulunmuyor.");
        }

        private void HayvanListesiRaporu(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  🐾 HAYVAN LİSTESİ                                                  │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
            rapor.AppendLine();
            
            rapor.AppendLine("  EVCİL HAYVANLAR:");
            foreach (var h in veri.EvcilHayvanlar)
            {
                rapor.AppendLine($"    • {h.Ad} | {h.Tur} - {h.Irk} | Yaş: {h.Yas} | Sahip: {h.SahipAdi}");
            }
            if (veri.EvcilHayvanlar.Count == 0)
                rapor.AppendLine("    Kayıt yok.");
            
            rapor.AppendLine();
            rapor.AppendLine("  SOKAK HAYVANLARI:");
            foreach (var h in veri.SokakHayvanlari)
            {
                rapor.AppendLine($"    • {h.Ad} | {h.Tur} | Bölge: {h.BulunduguBolge}");
            }
            if (veri.SokakHayvanlari.Count == 0)
                rapor.AppendLine("    Kayıt yok.");
        }

        private void AsiTakvimiRaporu(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  💉 AŞI TAKVİMİ                                                     │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
            rapor.AppendLine();
            
            foreach (var h in veri.EvcilHayvanlar)
            {
                if (h.Asilar.Count > 0)
                {
                    rapor.AppendLine($"  🐾 {h.Ad} ({h.Tur}):");
                    foreach (var a in h.Asilar)
                    {
                        string sonraki = a.SonrakiAsiTarihi.HasValue ? a.SonrakiAsiTarihi.Value.ToString("dd.MM.yyyy") : "-";
                        rapor.AppendLine($"     • {a.AsiAdi} - Yapıldı: {a.AsiTarihi:dd.MM.yyyy} - Sonraki: {sonraki}");
                    }
                    rapor.AppendLine();
                }
            }
        }

        private void FinansalRapor(StringBuilder rapor, VeriYoneticisi veri)
        {
            rapor.AppendLine("┌─────────────────────────────────────────────────────────────────────┐");
            rapor.AppendLine("│  💰 FİNANSAL RAPOR                                                  │");
            rapor.AppendLine("└─────────────────────────────────────────────────────────────────────┘");
            rapor.AppendLine();
            
            decimal toplamGelir = 0;
            foreach (var m in veri.Muayeneler)
            {
                toplamGelir += m.Ucret;
            }
            
            rapor.AppendLine($"  Toplam Muayene Sayısı : {veri.Muayeneler.Count}");
            rapor.AppendLine($"  Toplam Gelir          : {toplamGelir:N2} TL");
            rapor.AppendLine($"  Ortalama Muayene Ücreti: {(veri.Muayeneler.Count > 0 ? toplamGelir / veri.Muayeneler.Count : 0):N2} TL");
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRapor.Text))
            {
                MessageBox.Show("Önce bir rapor oluşturun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dosyaAdi = $"Rapor_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string yol = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dosyaAdi);
            
            try
            {
                File.WriteAllText(yol, txtRapor.Text, Encoding.UTF8);
                MessageBox.Show($"Rapor kaydedildi:\n{yol}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
