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
    public partial class Form5 : Form
    {
        // Rastgele klinik verileri
        private List<Klinik> klinikler;
        
        public Form5()
        {
            InitializeComponent();
            this.Load += Form5_Load;
            button2.Click += Button2_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Klinik verilerini oluştur
            KlinikleriOlustur();
            
            // İl listesini doldur
            comboBox1.Items.Clear();
            comboBox1.Items.Add("İstanbul");
            comboBox1.Items.Add("Ankara");
            comboBox1.Items.Add("İzmir");
            comboBox1.Items.Add("Bursa");
            comboBox1.Items.Add("Antalya");
            comboBox1.Items.Add("Çorum");
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen ile göre ilçe listesini güncelle
            comboBox2.Items.Clear();
            
            switch (comboBox1.SelectedItem?.ToString())
            {
                case "İstanbul":
                    comboBox2.Items.AddRange(new[] { "Kadıköy", "Beşiktaş", "Üsküdar", "Bakırköy", "Şişli", "Maltepe" });
                    break;
                case "Ankara":
                    comboBox2.Items.AddRange(new[] { "Çankaya", "Keçiören", "Yenimahalle", "Mamak", "Etimesgut" });
                    break;
                case "İzmir":
                    comboBox2.Items.AddRange(new[] { "Konak", "Karşıyaka", "Bornova", "Buca", "Alsancak" });
                    break;
                case "Bursa":
                    comboBox2.Items.AddRange(new[] { "Osmangazi", "Nilüfer", "Yıldırım", "Mudanya" });
                    break;
                case "Antalya":
                    comboBox2.Items.AddRange(new[] { "Muratpaşa", "Kepez", "Konyaaltı", "Lara" });
                    break;
                case "Çorum":
                    comboBox2.Items.AddRange(new[] { "Sungurlu", "Alaca", "Merkez" });
                    break;
            }
            
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void KlinikleriOlustur()
        {
            klinikler = new List<Klinik>
            {
                new Klinik("Patiler Veteriner Kliniği", "İstanbul", "Kadıköy", "0216 345 67 89", "Caferağa Mah. Moda Cad. No:45"),
                new Klinik("Dostlar Pet Kliniği", "İstanbul", "Beşiktaş", "0212 234 56 78", "Sinanpaşa Mah. Beşiktaş Cad. No:12"),
                new Klinik("Hayat Veteriner", "İstanbul", "Üsküdar", "0216 456 78 90", "Acıbadem Mah. Köprülü Sok. No:8"),
                new Klinik("Can Dostum Kliniği", "İstanbul", "Bakırköy", "0212 567 89 01", "Ataköy 7-8 Mah. E-5 Yan Yol No:22"),
                new Klinik("VetPlus Kliniği", "İstanbul", "Şişli", "0212 345 12 34", "Mecidiyeköy Mah. Büyükdere Cad. No:88"),
                new Klinik("Sevgi Pati", "Ankara", "Çankaya", "0312 456 23 45", "Kızılay Mah. Atatürk Bulvarı No:120"),
                new Klinik("Başkent Veteriner", "Ankara", "Keçiören", "0312 567 34 56", "Etlik Mah. Keçiören Cad. No:55"),
                new Klinik("Ege Veteriner", "İzmir", "Konak", "0232 345 45 67", "Alsancak Mah. Kıbrıs Şehitleri Cad. No:32"),
                new Klinik("Karşıyaka Pet", "İzmir", "Karşıyaka", "0232 456 56 78", "Bostanlı Mah. Cemal Gürsel Cad. No:15"),
                new Klinik("Yeşil Patiler", "Bursa", "Nilüfer", "0224 234 67 89", "Özlüce Mah. Prof. Dr. Turgut Noyan Sok. No:5"),
                new Klinik("Akdeniz Veteriner", "Antalya", "Muratpaşa", "0242 345 78 90", "Güllük Mah. Atatürk Cad. No:78"),
                new Klinik("Çorum Pet Kliniği", "Çorum", "Merkez", "0364 234 89 01", "Mimar Sinan Mah. Atatürk Cad. No:44")
            };
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Ara butonuna basıldığında en yakın kliniği göster
            string secilenIl = comboBox1.SelectedItem?.ToString() ?? "";
            string secilenIlce = comboBox2.SelectedItem?.ToString() ?? "";
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            
            // Önce ilçeye göre ara
            var bulunanKlinik = klinikler.FirstOrDefault(k => k.Ilce == secilenIlce && k.Il == secilenIl);
            
            // İlçede yoksa ile göre ara
            if (bulunanKlinik == null)
            {
                bulunanKlinik = klinikler.FirstOrDefault(k => k.Il == secilenIl);
            }
            
            // Hala yoksa rastgele bir tane seç
            if (bulunanKlinik == null && klinikler.Count > 0)
            {
                Random rnd = new Random();
                bulunanKlinik = klinikler[rnd.Next(klinikler.Count)];
            }
            
            if (bulunanKlinik != null)
            {
                listBox1.Items.Add($"📍 {bulunanKlinik.Ad}");
                listBox1.Items.Add($"   {bulunanKlinik.Il} / {bulunanKlinik.Ilce}");
                
                listBox2.Items.Add($"📞 {bulunanKlinik.Telefon}");
                
                listBox3.Items.Add($"🏠 {bulunanKlinik.Adres}");
                listBox3.Items.Add($"   {bulunanKlinik.Ilce}, {bulunanKlinik.Il}");
                listBox3.Items.Add("");
                listBox3.Items.Add("⏰ Çalışma Saatleri: 09:00 - 21:00");
            }
            else
            {
                listBox1.Items.Add("Klinik bulunamadı.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }
        
        // Klinik bilgileri
        private class Klinik
        {
            public string Ad { get; set; }
            public string Il { get; set; }
            public string Ilce { get; set; }
            public string Telefon { get; set; }
            public string Adres { get; set; }
            
            public Klinik(string ad, string il, string ilce, string telefon, string adres)
            {
                Ad = ad;
                Il = il;
                Ilce = ilce;
                Telefon = telefon;
                Adres = adres;
            }
        }
    }
}
