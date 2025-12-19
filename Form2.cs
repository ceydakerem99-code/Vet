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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // İşlem kayıt butonu ekle 
            Button btnIslemKayit = new Button();
            btnIslemKayit.Text = "🩺 İşlem Kayıt";
            btnIslemKayit.Location = new Point(20, 20);
            btnIslemKayit.Size = new Size(140, 45);
            btnIslemKayit.BackColor = Color.FromArgb(220, 50, 50);
            btnIslemKayit.ForeColor = Color.White;
            btnIslemKayit.FlatStyle = FlatStyle.Flat;
            btnIslemKayit.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnIslemKayit.Click += (s, ev) => {
                FormIslemKayit form = new FormIslemKayit();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnIslemKayit);

            // Hasta görüntüle butonu ekle 
            Button btnHastaGoruntule = new Button();
            btnHastaGoruntule.Text = "👁️ Hasta Görüntüle";
            btnHastaGoruntule.Location = new Point(170, 20);
            btnHastaGoruntule.Size = new Size(150, 45);
            btnHastaGoruntule.BackColor = Color.White;
            btnHastaGoruntule.ForeColor = Color.Black;
            btnHastaGoruntule.FlatStyle = FlatStyle.Flat;
            btnHastaGoruntule.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHastaGoruntule.Click += (s, ev) => {
                FormHastaGoruntule form = new FormHastaGoruntule();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnHastaGoruntule);

            // Şikayetler butonu ekle 
            Button btnSikayetler = new Button();
            btnSikayetler.Text = "📋 Şikayetler";
            btnSikayetler.Location = new Point(330, 20);
            btnSikayetler.Size = new Size(140, 45);
            btnSikayetler.BackColor = Color.FromArgb(255, 165, 0);
            btnSikayetler.ForeColor = Color.White;
            btnSikayetler.FlatStyle = FlatStyle.Flat;
            btnSikayetler.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSikayetler.Click += (s, ev) => {
                FormSikayetler form = new FormSikayetler();
                this.Hide();
                form.Show();
                form.FormClosed += (s2, e2) => this.Close();
            };
            this.Controls.Add(btnSikayetler);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 yeniForm = new Form9();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 yeniForm = new Form8();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 yeniForm = new Form10();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form11 yeniForm = new Form11();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 yeniForm = new Form1();
            this.Hide();
            yeniForm.Show();
            yeniForm.FormClosed += (s, args) => this.Close();
        }
    }
}
