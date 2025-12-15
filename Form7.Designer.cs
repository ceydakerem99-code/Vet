namespace VeterinerProjectApp
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            Şikayet = new CheckBox();
            checkBox2 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(255, 251, 224);
            textBox1.Location = new Point(40, 207);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(502, 413);
            textBox1.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(696, 207);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(197, 28);
            comboBox1.TabIndex = 1;
            // 
            // Şikayet
            // 
            Şikayet.AutoSize = true;
            Şikayet.Location = new Point(682, 288);
            Şikayet.Name = "Şikayet";
            Şikayet.Size = new Size(67, 24);
            Şikayet.TabIndex = 2;
            Şikayet.Text = "Öneri";
            Şikayet.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(682, 335);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(78, 24);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Şikayet";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(892, 502);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 17;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 216, 63);
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Location = new Point(892, 404);
            button2.Name = "button2";
            button2.Size = new Size(250, 61);
            button2.TabIndex = 18;
            button2.Text = "Gönder";
            button2.UseVisualStyleBackColor = false;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1380, 700);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox2);
            Controls.Add(Şikayet);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Name = "Form7";
            Text = "Form7";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private ComboBox comboBox1;
        private CheckBox Şikayet;
        private CheckBox checkBox2;
        private Button button1;
        private Button button2;
    }
}