namespace VeterinerProjectApp
{
    partial class Form11
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            button1 = new Button();
            monthCalendar1 = new MonthCalendar();
            label1 = new Label();
            button2 = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(25, 658);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 20;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(1097, 327);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(961, 405);
            label1.Name = "label1";
            label1.Size = new Size(112, 40);
            label1.TabIndex = 22;
            label1.Text = "Verileri Almak \r\nİstediğiniz Tarih";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 216, 63);
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Location = new Point(1109, 618);
            button2.Name = "button2";
            button2.Size = new Size(250, 61);
            button2.TabIndex = 23;
            button2.Text = "Verileri Dışa Aktar";
            button2.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(862, 565);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(62, 24);
            checkBox1.TabIndex = 24;
            checkBox1.Text = "Cinsi";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(862, 607);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(164, 24);
            checkBox2.TabIndex = 25;
            checkBox2.Text = "Toplam Hasta Sayısı";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(862, 655);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(143, 24);
            checkBox3.TabIndex = 26;
            checkBox3.Text = "Randevu Bilgileri";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // Form11
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1403, 744);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(monthCalendar1);
            Controls.Add(button1);
            Name = "Form11";
            Text = "Form11";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private MonthCalendar monthCalendar1;
        private Label label1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}