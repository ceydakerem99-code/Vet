namespace VeterinerProjectApp
{
    partial class Form15
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form15));
            button1 = new Button();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(26, 659);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 52;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(749, 496);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(465, 110);
            textBox7.TabIndex = 66;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(749, 451);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(465, 27);
            textBox6.TabIndex = 65;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(749, 408);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(465, 27);
            textBox5.TabIndex = 64;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(749, 355);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(465, 27);
            textBox4.TabIndex = 63;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(749, 311);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(465, 27);
            textBox3.TabIndex = 62;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(749, 261);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(465, 27);
            textBox2.TabIndex = 61;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Location = new Point(630, 499);
            label7.Name = "label7";
            label7.Size = new Size(113, 20);
            label7.TabIndex = 59;
            label7.Text = "Yapılan İşlemler";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(652, 454);
            label6.Name = "label6";
            label6.Size = new Size(91, 20);
            label6.TabIndex = 58;
            label6.Text = "Son Durumu";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(633, 408);
            label5.Name = "label5";
            label5.Size = new Size(110, 20);
            label5.TabIndex = 57;
            label5.Text = "Getirildiği Tarih";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(652, 358);
            label4.Name = "label4";
            label4.Size = new Size(91, 20);
            label4.TabIndex = 56;
            label4.Text = "Mikroçip No";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(673, 318);
            label3.Name = "label3";
            label3.Size = new Size(30, 20);
            label3.TabIndex = 55;
            label3.Text = "Yaş";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(673, 268);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 54;
            label2.Text = "Cinsi";
            // 
            // Form15
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1398, 753);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Name = "Form15";
            Text = "Form15";
            Load += Form15_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox7;
        private TextBox textBox6;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}