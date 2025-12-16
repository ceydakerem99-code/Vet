using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeterinerProjectApp
{
    public partial class Form6 : Form
    {
        // Nöbetçi klinik verileri
        private List<NobetciKlinik> nobetciKlinikler;
        private DateTimePicker dateTimePickerGun;
        private Label lblGun;
        
        public Form6()
        {
            InitializeComponent();
            this.Load += Form6_Load;
            button2.Click += Button2_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // Nöbetçi klinik verilerini oluştur
            NobetciKlinikleriOlustur();
            
            // Gün seçimi için DateTimePicker ekle
            lblGun = new Label();
            lblGun.Text = "Gün Seçin:";
            lblGun.Location = new Point(52, 285);
            lblGun.AutoSize = true;
            lblGun.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(lblGun);
            
            dateTimePickerGun = new DateTimePicker();
            dateTimePickerGun.Location = new Point(160, 282);
            dateTimePickerGun.Size = new Size(200, 28);
            dateTimePickerGun.Format = DateTimePickerFormat.Long;
            dateTimePickerGun.Value = DateTime.Now;
            this.Controls.Add(dateTimePickerGun);
            
            // İl listesini doldur
            comboBox1.Items.Clear();
            comboBox1.Items.Add("İstanbul");
            comboBox1.Items.Add("Ankara");
            comboBox1.Items.Add("İzmir");
            comboBox1.Items.Add("Bursa");
            comboBox1.Items.Add("Antalya");
            comboBox1.Items.Add("Konya");
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen ile göre ilçe listesini güncelle
            comboBox2.Items.Clear();
            
            switch (comboBox1.SelectedItem?.ToString())
            {
                case "İstanbul":
                    comboBox2.Items.AddRange(new[] { "Anadolu Yakası", "Avrupa Yakası" });
                    break;
                case "Ankara":
                    comboBox2.Items.AddRange(new[] { "Merkez", "Çevre İlçeler" });
                    break;
                case "İzmir":
                    comboBox2.Items.AddRange(new[] { "Merkez", "Kuzey", "Güney" });
                    break;
                case "Bursa":
                    comboBox2.Items.AddRange(new[] { "Merkez", "Sahil" });
                    break;
                case "Antalya":
                    comboBox2.Items.AddRange(new[] { "Merkez", "Sahil" });
                    break;
                case "Konya":
                    comboBox2.Items.AddRange(new[] { "Merkez" });
                    break;
            }
            
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void NobetciKlinikleriOlustur()
        {
            // Her gün için farklı nöbetçi klinikler - nöbet günleri ile
            nobetciKlinikler = new List<NobetciKlinik>
            {
                // İstanbul - Pazartesi, Çarşamba, Cuma
                new NobetciKlinik("7/24 Acil Pet Hastanesi", "İstanbul", "Anadolu Yakası", "0216 999 88 77", "Kadıköy Mah. Acil Sok. No:1", true, 
                    new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday }),
                // İstanbul - Salı, Perşembe, Cumartesi
                new NobetciKlinik("Gece Nöbetçi Veteriner", "İstanbul", "Avrupa Yakası", "0212 888 77 66", "Beşiktaş Mah. Nöbet Cad. No:24", true,
                    new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }),
                // İstanbul - Hafta sonu
                new NobetciKlinik("Acil Patiler Kliniği", "İstanbul", "Anadolu Yakası", "0216 777 66 55", "Üsküdar Mah. Hayvan Sok. No:7", true,
                    new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }),
                
                // Ankara - Tek günler
                new NobetciKlinik("Başkent 7/24 Veteriner", "Ankara", "Merkez", "0312 444 33 22", "Çankaya Mah. Nöbetçi Bulvarı No:15", true,
                    new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday }),
                // Ankara - Çift günler
                new NobetciKlinik("Ankara Acil Pet", "Ankara", "Çevre İlçeler", "0312 333 22 11", "Keçiören Mah. Gece Cad. No:8", true,
                    new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }),
                
                // İzmir - Her gün
                new NobetciKlinik("Ege Acil Veteriner", "İzmir", "Merkez", "0232 555 44 33", "Konak Mah. Acil Yardım Sok. No:3", true,
                    new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }),
                new NobetciKlinik("İzmir Gece Kliniği", "İzmir", "Kuzey", "0232 444 33 22", "Karşıyaka Mah. Nöbet Sok. No:11", true,
                    new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }),
                
                // Bursa - Hafta içi
                new NobetciKlinik("Bursa 7/24 Pet", "Bursa", "Merkez", "0224 666 55 44", "Nilüfer Mah. Acil Cad. No:20", true,
                    new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday }),
                
                // Antalya - Her gün
                new NobetciKlinik("Akdeniz Acil Veteriner", "Antalya", "Merkez", "0242 777 66 55", "Muratpaşa Mah. Gece Yardım No:5", true,
                    new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday }),
                
                // Konya - Her gün
                new NobetciKlinik("Konya Nöbetçi Klinik", "Konya", "Merkez", "0332 888 77 66", "Selçuklu Mah. 7/24 Sok. No:12", true,
                    new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday })
            };
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Ara butonuna basıldığında nöbetçi kliniği göster
            string secilenIl = comboBox1.SelectedItem?.ToString() ?? "";
            string secilenBolge = comboBox2.SelectedItem?.ToString() ?? "";
            DayOfWeek secilenGun = dateTimePickerGun.Value.DayOfWeek;
            DateTime secilenTarih = dateTimePickerGun.Value;
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            
            // İl, bölge ve GÜN'e göre nöbetçi kliniği bul
            var bulunanKlinikler = nobetciKlinikler
                .Where(k => k.Il == secilenIl && k.Bolge == secilenBolge && k.AcikMi && k.NobetGunleri.Contains(secilenGun))
                .ToList();
            
            // Bölge bulunamazsa sadece il ve güne göre ara
            if (bulunanKlinikler.Count == 0)
            {
                bulunanKlinikler = nobetciKlinikler.Where(k => k.Il == secilenIl && k.AcikMi && k.NobetGunleri.Contains(secilenGun)).ToList();
            }
            
            // İl bulunamazsa sadece güne göre ara
            if (bulunanKlinikler.Count == 0)
            {
                bulunanKlinikler = nobetciKlinikler.Where(k => k.AcikMi && k.NobetGunleri.Contains(secilenGun)).ToList();
            }
            
            string gunAdi = GunAdiGetir(secilenGun);
            
            if (bulunanKlinikler.Count > 0)
            {
                foreach (var klinik in bulunanKlinikler)
                {
                    listBox1.Items.Add($"🏥 {klinik.Ad}");
                    listBox1.Items.Add($"   {klinik.Il} / {klinik.Bolge}");
                    listBox1.Items.Add("");
                }
                
                var ilkKlinik = bulunanKlinikler[0];
                listBox2.Items.Add($"📞 ACİL HAT: {ilkKlinik.Telefon}");
                listBox2.Items.Add("   (7/24 Açık)");
                
                listBox3.Items.Add($"🏠 {ilkKlinik.Adres}");
                listBox3.Items.Add($"   {ilkKlinik.Bolge}, {ilkKlinik.Il}");
                listBox3.Items.Add("");
                listBox3.Items.Add($"📅 Seçilen Gün: {gunAdi}");
                listBox3.Items.Add($"📅 Tarih: {secilenTarih:dd.MM.yyyy}");
                listBox3.Items.Add($"✅ {bulunanKlinikler.Count} klinik nöbetçi");
            }
            else
            {
                listBox1.Items.Add($"❌ {gunAdi} günü için nöbetçi klinik bulunamadı.");
                listBox1.Items.Add("   Lütfen başka bir gün seçin.");
            }
        }
        
        private string GunAdiGetir(DayOfWeek gun)
        {
            switch (gun)
            {
                case DayOfWeek.Monday: return "Pazartesi";
                case DayOfWeek.Tuesday: return "Salı";
                case DayOfWeek.Wednesday: return "Çarşamba";
                case DayOfWeek.Thursday: return "Perşembe";
                case DayOfWeek.Friday: return "Cuma";
                case DayOfWeek.Saturday: return "Cumartesi";
                case DayOfWeek.Sunday: return "Pazar";
                default: return gun.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }
        
        // İç sınıf - Nöbetçi Klinik bilgileri
        private class NobetciKlinik
        {
            public string Ad { get; set; }
            public string Il { get; set; }
            public string Bolge { get; set; }
            public string Telefon { get; set; }
            public string Adres { get; set; }
            public bool AcikMi { get; set; }
            public DayOfWeek[] NobetGunleri { get; set; }
            
            public NobetciKlinik(string ad, string il, string bolge, string telefon, string adres, bool acikMi, DayOfWeek[] nobetGunleri = null)
            {
                Ad = ad;
                Il = il;
                Bolge = bolge;
                Telefon = telefon;
                Adres = adres;
                AcikMi = acikMi;
                NobetGunleri = nobetGunleri ?? new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            }
        }
    }
}
