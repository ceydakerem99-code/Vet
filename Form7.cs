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

namespace VeterinerProjectApp
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.Load += Form7_Load;
            button2.Click += Button2_Click;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // şikayet/öneri konuları
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Randevu Sistemi");
            comboBox1.Items.Add("Muayene Hizmetleri");
            comboBox1.Items.Add("Aşı ve Tedavi");
            comboBox1.Items.Add("Personel Davranışı");
            comboBox1.Items.Add("Temizlik ve Hijyen");
            comboBox1.Items.Add("Bekleme Süresi");
            comboBox1.Items.Add("Fiyatlandırma");
            comboBox1.Items.Add("Acil Servis");
            comboBox1.Items.Add("Çalışma Saatleri");
            comboBox1.Items.Add("Park Alanı");
            comboBox1.Items.Add("Online Hizmetler");
            comboBox1.Items.Add("İletişim ve Bilgilendirme");
            comboBox1.Items.Add("Sokak Hayvanları Hizmeti");
            comboBox1.Items.Add("Diğer");
            
            comboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Gönder 
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen mesajınızı yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Şikayet.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("Lütfen Öneri veya Şikayet seçeneklerinden birini işaretleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tur = Şikayet.Checked ? "Öneri" : "Şikayet";
            string konu = comboBox1.SelectedItem?.ToString() ?? "Genel";
            string mesaj = textBox1.Text;

            // Geri bildirimi kaydet
            StringBuilder geriBildirim = new StringBuilder();
            geriBildirim.AppendLine("═══════════════════════════════════════");
            geriBildirim.AppendLine($"TARİH: {DateTime.Now:dd.MM.yyyy HH:mm}");
            geriBildirim.AppendLine($"TÜR: {tur}");
            geriBildirim.AppendLine($"KONU: {konu}");
            geriBildirim.AppendLine("═══════════════════════════════════════");
            geriBildirim.AppendLine("MESAJ:");
            geriBildirim.AppendLine(mesaj);
            geriBildirim.AppendLine("═══════════════════════════════════════");
            geriBildirim.AppendLine();

            try
            {
                string dosyaYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeriBildirimler.txt");
                File.AppendAllText(dosyaYolu, geriBildirim.ToString(), Encoding.UTF8);
                
                MessageBox.Show(
                    $"Geri bildiriminiz başarıyla gönderildi!\n\n" +
                    $"Tür: {tur}\n" +
                    $"Konu: {konu}\n\n" +
                    "Değerli görüşleriniz için teşekkür ederiz.",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Formu temizle
                textBox1.Clear();
                Şikayet.Checked = false;
                checkBox2.Checked = false;
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gönderim sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
