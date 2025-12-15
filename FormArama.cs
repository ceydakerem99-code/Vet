using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class FormArama : Form
    {
        private TextBox txtArama;
        private ComboBox cmbKategori;
        private ListBox lstSonuclar;
        private Button btnAra;
        private Button btnAnaSayfa;

        public FormArama()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Hayvan Arama";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(62, 166, 107);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "🔍 Hayvan Arama";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Kategori label
            Label lblKategori = new Label();
            lblKategori.Text = "Arama Kategorisi:";
            lblKategori.Location = new Point(30, 70);
            lblKategori.AutoSize = true;
            this.Controls.Add(lblKategori);

            // Kategori combobox
            cmbKategori = new ComboBox();
            cmbKategori.Location = new Point(30, 95);
            cmbKategori.Size = new Size(200, 30);
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.Items.AddRange(new[] { "Tüm Hayvanlar", "Evcil Hayvanlar", "Sokak Hayvanları", "Hayvan Adı", "Tür", "Sahip Adı" });
            cmbKategori.SelectedIndex = 0;
            this.Controls.Add(cmbKategori);

            // Arama kutusu
            txtArama = new TextBox();
            txtArama.Location = new Point(250, 95);
            txtArama.Size = new Size(350, 30);
            txtArama.Font = new Font("Segoe UI", 11);
            txtArama.PlaceholderText = "Arama yapın...";
            this.Controls.Add(txtArama);

            // Ara butonu
            btnAra = new Button();
            btnAra.Text = "Ara";
            btnAra.Location = new Point(620, 93);
            btnAra.Size = new Size(100, 35);
            btnAra.BackColor = Color.FromArgb(255, 216, 63);
            btnAra.FlatStyle = FlatStyle.Flat;
            btnAra.Click += BtnAra_Click;
            this.Controls.Add(btnAra);

            // Sonuçlar listesi
            Label lblSonuclar = new Label();
            lblSonuclar.Text = "Arama Sonuçları:";
            lblSonuclar.Location = new Point(30, 145);
            lblSonuclar.AutoSize = true;
            this.Controls.Add(lblSonuclar);

            lstSonuclar = new ListBox();
            lstSonuclar.Location = new Point(30, 170);
            lstSonuclar.Size = new Size(720, 320);
            lstSonuclar.Font = new Font("Consolas", 10);
            this.Controls.Add(lstSonuclar);

            // Ana Sayfa butonu
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(30, 510);
            btnAnaSayfa.Size = new Size(150, 40);
            btnAnaSayfa.BackColor = Color.FromArgb(255, 216, 63);
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Click += BtnAnaSayfa_Click;
            this.Controls.Add(btnAnaSayfa);

            // Form yüklendiğinde tüm hayvanları göster
            this.Load += (s, e) => TumHayvanlariGoster();
        }

        private void TumHayvanlariGoster()
        {
            var veri = VeriYoneticisi.Instance;
            lstSonuclar.Items.Clear();
            
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            lstSonuclar.Items.Add("  EVCİL HAYVANLAR");
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            
            if (veri.EvcilHayvanlar.Count == 0)
                lstSonuclar.Items.Add("  Kayıtlı evcil hayvan yok.");
            else
            {
                foreach (var h in veri.EvcilHayvanlar)
                    lstSonuclar.Items.Add($"  🐾 {h.Ad} | {h.Tur} - {h.Irk} | Yaş: {h.Yas} | Sahip: {h.SahipAdi}");
            }
            
            lstSonuclar.Items.Add("");
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            lstSonuclar.Items.Add("  SOKAK HAYVANLARI");
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            
            if (veri.SokakHayvanlari.Count == 0)
                lstSonuclar.Items.Add("  Kayıtlı sokak hayvanı yok.");
            else
            {
                foreach (var h in veri.SokakHayvanlari)
                    lstSonuclar.Items.Add($"  🐕 {h.Ad} | {h.Tur} | Bölge: {h.BulunduguBolge} | Sorumlu: {h.SorumluAdi}");
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            string aramaMetni = txtArama.Text.Trim().ToLower();
            string kategori = cmbKategori.SelectedItem?.ToString() ?? "";
            var veri = VeriYoneticisi.Instance;
            
            lstSonuclar.Items.Clear();
            lstSonuclar.Items.Add($"Arama: \"{aramaMetni}\" - Kategori: {kategori}");
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            
            int bulunan = 0;
            
            // Evcil hayvanlar
            if (kategori != "Sokak Hayvanları")
            {
                foreach (var h in veri.EvcilHayvanlar)
                {
                    bool eslesme = string.IsNullOrEmpty(aramaMetni) ||
                                   h.Ad.ToLower().Contains(aramaMetni) ||
                                   h.Tur.ToLower().Contains(aramaMetni) ||
                                   h.Irk.ToLower().Contains(aramaMetni) ||
                                   (h.SahipAdi?.ToLower().Contains(aramaMetni) ?? false);
                    
                    if (eslesme)
                    {
                        lstSonuclar.Items.Add($"🐾 {h.Ad} | {h.Tur} - {h.Irk} | Yaş: {h.Yas} | Sahip: {h.SahipAdi}");
                        bulunan++;
                    }
                }
            }
            
            // Sokak hayvanları
            if (kategori != "Evcil Hayvanlar")
            {
                foreach (var h in veri.SokakHayvanlari)
                {
                    bool eslesme = string.IsNullOrEmpty(aramaMetni) ||
                                   h.Ad.ToLower().Contains(aramaMetni) ||
                                   h.Tur.ToLower().Contains(aramaMetni) ||
                                   h.BulunduguBolge.ToLower().Contains(aramaMetni);
                    
                    if (eslesme)
                    {
                        lstSonuclar.Items.Add($"🐕 {h.Ad} | {h.Tur} | Bölge: {h.BulunduguBolge}");
                        bulunan++;
                    }
                }
            }
            
            lstSonuclar.Items.Add("═══════════════════════════════════════════════════════════════════");
            lstSonuclar.Items.Add($"Toplam {bulunan} sonuç bulundu.");
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
