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
using VeterinerProjectApp.Enums;

namespace VeterinerProjectApp
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            this.Load += Form9_Load;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            // Bekleyen onayları yükle
            BekleyenOnaylariYukle();
        }

        private void BekleyenOnaylariYukle()
        {
            checkedListBox1.Items.Clear();
            var veriYoneticisi = VeriYoneticisi.Instance;
            
            foreach (var randevu in veriYoneticisi.BekleyenRandevular())
            {
                string bilgi = $"Randevu #{randevu.Id} - {randevu.RandevuTarihi:dd.MM.yyyy} {randevu.RandevuSaati:hh\\:mm}";
                
                // Geliş sebebini göster
                if (!string.IsNullOrEmpty(randevu.RandevuNedeni))
                {
                    bilgi += $" | Sebep: {randevu.RandevuNedeni}";
                }
                else if (!string.IsNullOrEmpty(randevu.Sikayet))
                {
                    bilgi += $" | Sebep: {randevu.Sikayet}";
                }
                
                checkedListBox1.Items.Add(bilgi);
            }
            
            if (checkedListBox1.Items.Count == 0)
            {
                checkedListBox1.Items.Add("Bekleyen onay bulunmuyor.");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Onayla butonu
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Lütfen onaylamak istediğiniz randevuları seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;
            int onaylanan = 0;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                string text = item.ToString();
                if (text.StartsWith("Randevu #"))
                {
                    // ID'yi çıkar
                    int spaceIndex = text.IndexOf(" - ");
                    if (spaceIndex > 9)
                    {
                        string idStr = text.Substring(9, spaceIndex - 9);
                        if (int.TryParse(idStr, out int randevuId))
                        {
                            var randevu = veriYoneticisi.RandevuBul(randevuId);
                            if (randevu != null && randevu.Durum == RandevuDurumu.Bekliyor)
                            {
                                randevu.Onayla(1); // Admin ID: 1
                                onaylanan++;
                                
                                // Kullanıcıya bildirim gönder
                                var bildirim = BildirimServisi.Instance;
                                bildirim.RandevuOnayBildirimi($"+90532{randevu.KullaniciId}000000", randevu.RandevuTarihi);
                            }
                        }
                    }
                }
            }

            MessageBox.Show($"{onaylanan} randevu onaylandı.\nKullanıcılara bildirim gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BekleyenOnaylariYukle();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // Reddet butonu
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Lütfen reddetmek istediğiniz randevuları seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veriYoneticisi = VeriYoneticisi.Instance;
            int reddedilen = 0;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                string text = item.ToString();
                if (text.StartsWith("Randevu #"))
                {
                    int spaceIndex = text.IndexOf(" - ");
                    if (spaceIndex > 9)
                    {
                        string idStr = text.Substring(9, spaceIndex - 9);
                        if (int.TryParse(idStr, out int randevuId))
                        {
                            var randevu = veriYoneticisi.RandevuBul(randevuId);
                            if (randevu != null && randevu.Durum == RandevuDurumu.Bekliyor)
                            {
                                randevu.Reddet("Admin tarafından reddedildi.");
                                reddedilen++;
                            }
                        }
                    }
                }
            }

            MessageBox.Show($"{reddedilen} randevu reddedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BekleyenOnaylariYukle();
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
