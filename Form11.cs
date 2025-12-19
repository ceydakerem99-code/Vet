using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeterinerProjectApp.Services;
using VeterinerProjectApp.Models;

namespace VeterinerProjectApp
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            button2.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Verileri dışa aktar
            var veriYoneticisi = VeriYoneticisi.Instance;
            DateTime secilenTarih = monthCalendar1.SelectionStart;
            
            StringBuilder rapor = new StringBuilder();
            rapor.AppendLine("═══════════════════════════════════════════════════════");
            rapor.AppendLine("        VETERİNER KLİNİK VERİ RAPORU");
            rapor.AppendLine("═══════════════════════════════════════════════════════");
            rapor.AppendLine($"Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}");
            rapor.AppendLine($"Seçilen Tarih: {secilenTarih:dd.MM.yyyy}");
            rapor.AppendLine();
            
            if (checkBox1.Checked) // Cinsi
            {
                rapor.AppendLine("─── HAYVAN TÜRLERİ ───");
                int kediSayisi = veriYoneticisi.EvcilHayvanlar.Count(h => h.Tur.ToLower().Contains("kedi"));
                int kopekSayisi = veriYoneticisi.EvcilHayvanlar.Count(h => h.Tur.ToLower().Contains("köpek") || h.Tur.ToLower().Contains("kopek"));
                int digerSayisi = veriYoneticisi.EvcilHayvanlar.Count - kediSayisi - kopekSayisi;
                
                rapor.AppendLine($"Kedi: {kediSayisi}");
                rapor.AppendLine($"Köpek: {kopekSayisi}");
                rapor.AppendLine($"Diğer: {digerSayisi}");
                rapor.AppendLine();
            }
            
            if (checkBox2.Checked) // Toplam hasta sayisi
            {
                rapor.AppendLine("─── HASTA SAYILARI ───");
                rapor.AppendLine($"Toplam Evcil Hayvan: {veriYoneticisi.EvcilHayvanlar.Count}");
                rapor.AppendLine($"Toplam Sokak Hayvanı: {veriYoneticisi.SokakHayvanlari.Count}");
                rapor.AppendLine($"Toplam Muayene: {veriYoneticisi.Muayeneler.Count}");
                rapor.AppendLine($"Günlük Muayene ({secilenTarih:dd.MM.yyyy}): {veriYoneticisi.GunlukMuayeneSayisi(secilenTarih)}");
                rapor.AppendLine();
            }
            
            if (checkBox3.Checked) // Randevu bilgileri
            {
                rapor.AppendLine("─── RANDEVU BİLGİLERİ ───");
                rapor.AppendLine($"Toplam Randevu: {veriYoneticisi.Randevular.Count}");
                rapor.AppendLine($"Bekleyen: {veriYoneticisi.BekleyenRandevular().Count}");
                rapor.AppendLine();
                
                foreach (var randevu in veriYoneticisi.Randevular)
                {
                    rapor.AppendLine($"• Randevu #{randevu.Id}: {randevu.RandevuTarihi:dd.MM.yyyy} - {randevu.DurumMetni()}");
                }
                rapor.AppendLine();
            }
            
            rapor.AppendLine("═══════════════════════════════════════════════════════");
            
            // Dosyaya kaydet
            string dosyaYolu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                                            $"VeterinerRapor_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            
            try
            {
                File.WriteAllText(dosyaYolu, rapor.ToString(), Encoding.UTF8);
                MessageBox.Show($"Rapor başarıyla oluşturuldu!\n\nDosya: {dosyaYolu}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
