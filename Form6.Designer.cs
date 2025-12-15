namespace VeterinerProjectApp
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            listBox3 = new ListBox();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 500);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 15;
            label3.Text = "Adres";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 410);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 14;
            label2.Text = "Tel No";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 342);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 13;
            label1.Text = "İl / İlçe";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.Location = new Point(112, 471);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(407, 84);
            listBox3.TabIndex = 12;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(112, 399);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(407, 44);
            listBox2.TabIndex = 11;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(112, 331);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(407, 44);
            listBox1.TabIndex = 10;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(366, 204);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(159, 28);
            comboBox2.TabIndex = 9;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(112, 204);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(159, 28);
            comboBox1.TabIndex = 8;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(21, 659);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 16;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 128, 255);
            button2.Location = new Point(479, 589);
            button2.Name = "button2";
            button2.Size = new Size(81, 29);
            button2.TabIndex = 17;
            button2.Text = "Ara";
            button2.UseVisualStyleBackColor = false;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1384, 748);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox3);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Name = "Form6";
            Text = "Form6";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private ListBox listBox3;
        private ListBox listBox2;
        private ListBox listBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
    }
}