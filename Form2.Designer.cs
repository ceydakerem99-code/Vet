namespace VeterinerProjectApp
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 251, 224);
            button1.Location = new Point(760, 287);
            button1.Name = "button1";
            button1.Size = new Size(157, 85);
            button1.TabIndex = 0;
            button1.Text = "Günlük / Aylık \r\nHasta Sayısı";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 251, 224);
            button2.Location = new Point(1003, 287);
            button2.Name = "button2";
            button2.Size = new Size(157, 85);
            button2.TabIndex = 1;
            button2.Text = "Bekleyen \r\nOnaylar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(255, 251, 224);
            button3.Location = new Point(751, 527);
            button3.Name = "button3";
            button3.Size = new Size(157, 85);
            button3.TabIndex = 2;
            button3.Text = "Kayıtlı Kullanıcı \r\nListesi";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(255, 251, 224);
            button4.Location = new Point(994, 527);
            button4.Name = "button4";
            button4.Size = new Size(157, 85);
            button4.TabIndex = 3;
            button4.Text = "Verileri Dışarı\r\nAktar";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(255, 216, 63);
            button5.BackgroundImageLayout = ImageLayout.Zoom;
            button5.Location = new Point(60, 617);
            button5.Name = "button5";
            button5.Size = new Size(250, 61);
            button5.TabIndex = 4;
            button5.Text = "Ana Sayfa";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1408, 716);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}