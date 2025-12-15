namespace VeterinerProjectApp
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            Aylık = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(29, 665);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 17;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(774, 283);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(257, 357);
            textBox1.TabIndex = 18;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1115, 283);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(257, 357);
            textBox2.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(774, 250);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 20;
            label1.Text = "Günlük";
            // 
            // Aylık
            // 
            Aylık.AutoSize = true;
            Aylık.Location = new Point(1115, 250);
            Aylık.Name = "Aylık";
            Aylık.Size = new Size(41, 20);
            Aylık.TabIndex = 21;
            Aylık.Text = "Aylık";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(255, 251, 224);
            textBox3.Location = new Point(904, 612);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 67);
            textBox3.TabIndex = 22;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(255, 251, 224);
            textBox4.Location = new Point(1231, 612);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(161, 67);
            textBox4.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(853, 632);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 24;
            label2.Text = "Toplam";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1180, 632);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 25;
            label3.Text = "Toplam";
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1395, 747);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(Aylık);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form8";
            Text = "Form8";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label Aylık;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label2;
        private Label label3;
    }
}