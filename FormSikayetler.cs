using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VeterinerProjectApp
{
    /// <summary>
    /// Klinik Yöneticisi için şikayet/öneri görüntüleme formu
    /// </summary>
    public partial class FormSikayetler : Form
    {
        private ListBox lstSikayetler;
        private Button btnYenile;
        private Button btnAnaSayfa;
        private Label lblToplam;

        public FormSikayetler()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Şikayet ve Öneriler";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(62, 166, 107);

            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "📋 Şikayet ve Öneriler";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(300, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            // Toplam sayı
            lblToplam = new Label();
            lblToplam.Text = "Toplam: 0 kayıt";
            lblToplam.Font = new Font("Segoe UI", 11);
            lblToplam.ForeColor = Color.White;
            lblToplam.Location = new Point(30, 70);
            lblToplam.AutoSize = true;
            this.Controls.Add(lblToplam);

            // Şikayet listesi
            lstSikayetler = new ListBox();
            lstSikayetler.Location = new Point(30, 100);
            lstSikayetler.Size = new Size(820, 420);
            lstSikayetler.Font = new Font("Consolas", 10);
            lstSikayetler.BackColor = Color.White;
            this.Controls.Add(lstSikayetler);

            // Yenile butonu - TURUNCU
            btnYenile = new Button();
            btnYenile.Text = "🔄 Yenile";
            btnYenile.Location = new Point(30, 540);
            btnYenile.Size = new Size(150, 50);
            btnYenile.BackColor = Color.FromArgb(255, 165, 0);
            btnYenile.ForeColor = Color.White;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnYenile.Click += (s, e) => SikayetleriYukle();
            this.Controls.Add(btnYenile);

            // Ana Sayfa butonu - TURUNCU
            btnAnaSayfa = new Button();
            btnAnaSayfa.Text = "Ana Sayfa";
            btnAnaSayfa.Location = new Point(200, 540);
            btnAnaSayfa.Size = new Size(150, 50);
            btnAnaSayfa.BackColor = Color.FromArgb(255, 165, 0);
            btnAnaSayfa.ForeColor = Color.White;
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAnaSayfa.Click += (s, e) => {
                Form1 form = new Form1();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnAnaSayfa);

            this.Load += FormSikayetler_Load;
        }

        private void FormSikayetler_Load(object sender, EventArgs e)
        {
            SikayetleriYukle();
        }

        private void SikayetleriYukle()
        {
            lstSikayetler.Items.Clear();
            
            string dosyaYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeriBildirimler.txt");
            
            if (File.Exists(dosyaYolu))
            {
                try
                {
                    string[] satirlar = File.ReadAllLines(dosyaYolu);
                    int kayitSayisi = 0;
                    
                    lstSikayetler.Items.Add("═══════════════════════════════════════════════════════════════════════════════");
                    lstSikayetler.Items.Add("                         ŞİKAYET VE ÖNERİ KAYITLARI");
                    lstSikayetler.Items.Add("═══════════════════════════════════════════════════════════════════════════════");
                    lstSikayetler.Items.Add("");

                    foreach (string satir in satirlar)
                    {
                        if (!string.IsNullOrWhiteSpace(satir))
                        {
                            if (satir.StartsWith("---"))
                            {
                                lstSikayetler.Items.Add("───────────────────────────────────────────────────────────────────────────────");
                                kayitSayisi++;
                            }
                            else
                            {
                                lstSikayetler.Items.Add("  " + satir);
                            }
                        }
                    }
                    
                    lblToplam.Text = $"Toplam: {kayitSayisi} kayıt";
                }
                catch (Exception ex)
                {
                    lstSikayetler.Items.Add($"Dosya okuma hatası: {ex.Message}");
                }
            }
            else
            {
                lstSikayetler.Items.Add("");
                lstSikayetler.Items.Add("═══════════════════════════════════════════════════════════════════════════════");
                lstSikayetler.Items.Add("                    Henüz şikayet veya öneri bulunmamaktadır.");
                lstSikayetler.Items.Add("═══════════════════════════════════════════════════════════════════════════════");
                lstSikayetler.Items.Add("");
                lstSikayetler.Items.Add("  Kullanıcılar Ana Menü → Şikayet/Öneriler bölümünden geri bildirim gönderebilir.");
                lblToplam.Text = "Toplam: 0 kayıt";
            }
        }
    }
}
