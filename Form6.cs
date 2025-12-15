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
            // Her gün için farklı nöbetçi klinikler
            nobetciKlinikler = new List<NobetciKlinik>
            {
                // İstanbul
                new NobetciKlinik("7/24 Acil Pet Hastanesi", "İstanbul", "Anadolu Yakası", "0216 999 88 77", "Kadıköy Mah. Acil Sok. No:1", true),
                new NobetciKlinik("Gece Nöbetçi Veteriner", "İstanbul", "Avrupa Yakası", "0212 888 77 66", "Beşiktaş Mah. Nöbet Cad. No:24", true),
                new NobetciKlinik("Acil Patiler Kliniği", "İstanbul", "Anadolu Yakası", "0216 777 66 55", "Üsküdar Mah. Hayvan Sok. No:7", false),
                
                // Ankara
                new NobetciKlinik("Başkent 7/24 Veteriner", "Ankara", "Merkez", "0312 444 33 22", "Çankaya Mah. Nöbetçi Bulvarı No:15", true),
                new NobetciKlinik("Ankara Acil Pet", "Ankara", "Çevre İlçeler", "0312 333 22 11", "Keçiören Mah. Gece Cad. No:8", false),
                
                // İzmir
                new NobetciKlinik("Ege Acil Veteriner", "İzmir", "Merkez", "0232 555 44 33", "Konak Mah. Acil Yardım Sok. No:3", true),
                new NobetciKlinik("İzmir Gece Kliniği", "İzmir", "Kuzey", "0232 444 33 22", "Karşıyaka Mah. Nöbet Sok. No:11", false),
                
                // Bursa
                new NobetciKlinik("Bursa 7/24 Pet", "Bursa", "Merkez", "0224 666 55 44", "Nilüfer Mah. Acil Cad. No:20", true),
                
                // Antalya
                new NobetciKlinik("Akdeniz Acil Veteriner", "Antalya", "Merkez", "0242 777 66 55", "Muratpaşa Mah. Gece Yardım No:5", true),
                
                // Konya
                new NobetciKlinik("Konya Nöbetçi Klinik", "Konya", "Merkez", "0332 888 77 66", "Selçuklu Mah. 7/24 Sok. No:12", true)
            };
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Ara butonuna basıldığında nöbetçi kliniği göster
            string secilenIl = comboBox1.SelectedItem?.ToString() ?? "";
            string secilenBolge = comboBox2.SelectedItem?.ToString() ?? "";
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            
            // İl ve bölgeye göre nöbetçi kliniği bul
            var bulunanKlinikler = nobetciKlinikler
                .Where(k => k.Il == secilenIl && k.Bolge == secilenBolge && k.AcikMi)
                .ToList();
            
            if (bulunanKlinikler.Count == 0)
            {
                bulunanKlinikler = nobetciKlinikler.Where(k => k.Il == secilenIl && k.AcikMi).ToList();
            }
            
            if (bulunanKlinikler.Count == 0)
            {
                bulunanKlinikler = nobetciKlinikler.Where(k => k.AcikMi).ToList();
            }
            
            if (bulunanKlinikler.Count > 0)
            {
                var klinik = bulunanKlinikler[0];
                
                listBox1.Items.Add($"🏥 {klinik.Ad}");
                listBox1.Items.Add($"   {klinik.Il} / {klinik.Bolge}");
                
                listBox2.Items.Add($"📞 ACİL HAT: {klinik.Telefon}");
                listBox2.Items.Add("   (7/24 Açık)");
                
                listBox3.Items.Add($"🏠 {klinik.Adres}");
                listBox3.Items.Add($"   {klinik.Bolge}, {klinik.Il}");
                listBox3.Items.Add("");
                listBox3.Items.Add("⏰ NÖBETÇİ: 7/24 AÇIK");
                listBox3.Items.Add($"📅 Tarih: {DateTime.Now:dd.MM.yyyy}");
            }
            else
            {
                listBox1.Items.Add("Nöbetçi klinik bulunamadı.");
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
            
            public NobetciKlinik(string ad, string il, string bolge, string telefon, string adres, bool acikMi)
            {
                Ad = ad;
                Il = il;
                Bolge = bolge;
                Telefon = telefon;
                Adres = adres;
                AcikMi = acikMi;
            }
        }
    }
}
