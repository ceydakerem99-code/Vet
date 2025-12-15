using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            this.Load += Form10_Load;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // Kayıtlı kullanıcı ve hayvanları listele
            KullanicilariYukle();
        }

        private void KullanicilariYukle()
        {
            listBox1.Items.Clear();
            var veriYoneticisi = VeriYoneticisi.Instance;
            
            listBox1.Items.Add("═══════════════════════════════════════");
            listBox1.Items.Add("         KAYITLI HAYVAN SAHİPLERİ       ");
            listBox1.Items.Add("═══════════════════════════════════════");
            
            if (veriYoneticisi.HayvanSahipleri.Count == 0)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("  Henüz kayıtlı kullanıcı bulunmuyor.");
            }
            else
            {
                foreach (var sahip in veriYoneticisi.HayvanSahipleri)
                {
                    listBox1.Items.Add("");
                    listBox1.Items.Add($"► {sahip.TamAdGetir()}");
                    listBox1.Items.Add($"  E-posta: {sahip.Email}");
                    listBox1.Items.Add($"  Telefon: {sahip.Telefon}");
                    listBox1.Items.Add($"  Hayvan Sayısı: {sahip.HayvanSayisi}");
                    
                    foreach (var hayvan in sahip.Hayvanlar)
                    {
                        listBox1.Items.Add($"    • {hayvan.Ad} ({hayvan.Tur} - {hayvan.Irk})");
                    }
                }
            }
            
            listBox1.Items.Add("");
            listBox1.Items.Add("═══════════════════════════════════════");
            listBox1.Items.Add("         EVCİL HAYVANLAR                ");
            listBox1.Items.Add("═══════════════════════════════════════");
            
            if (veriYoneticisi.EvcilHayvanlar.Count == 0)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("  Henüz kayıtlı evcil hayvan yok.");
            }
            else
            {
                foreach (var hayvan in veriYoneticisi.EvcilHayvanlar)
                {
                    listBox1.Items.Add("");
                    listBox1.Items.Add($"► {hayvan.Ad}");
                    listBox1.Items.Add($"  Tür: {hayvan.Tur} | Irk: {hayvan.Irk}");
                    listBox1.Items.Add($"  Yaş: {hayvan.Yas} | Cinsiyet: {hayvan.Cinsiyet}");
                    listBox1.Items.Add($"  Sahip: {hayvan.SahipAdi}");
                    listBox1.Items.Add($"  Sağlık: {hayvan.SaglikDurumu}");
                }
            }
            
            listBox1.Items.Add("");
            listBox1.Items.Add("═══════════════════════════════════════");
            listBox1.Items.Add("         SOKAK HAYVANLARI               ");
            listBox1.Items.Add("═══════════════════════════════════════");
            
            if (veriYoneticisi.SokakHayvanlari.Count == 0)
            {
                listBox1.Items.Add("");
                listBox1.Items.Add("  Henüz kayıtlı sokak hayvanı yok.");
            }
            else
            {
                foreach (var hayvan in veriYoneticisi.SokakHayvanlari)
                {
                    listBox1.Items.Add("");
                    listBox1.Items.Add($"► {hayvan.Ad}");
                    listBox1.Items.Add($"  Tür: {hayvan.Tur} | Bölge: {hayvan.BulunduguBolge}");
                    listBox1.Items.Add($"  Kısırlaştırıldı: {(hayvan.KisirlastirildiMi ? "Evet" : "Hayır")}");
                    listBox1.Items.Add($"  Sorumlu: {hayvan.SorumluAdi}");
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili öğe detayları gösterilebilir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }
    }
}
