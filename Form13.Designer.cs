namespace VeterinerProjectApp
{
    partial class Form13
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form13));
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            label5 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // textBox4
            // 
            textBox4.Location = new Point(811, 367);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(465, 27);
            textBox4.TabIndex = 31;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(811, 323);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(465, 27);
            textBox3.TabIndex = 30;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(811, 273);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(465, 27);
            textBox2.TabIndex = 29;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(811, 227);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(465, 27);
            textBox1.TabIndex = 28;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(714, 370);
            label4.Name = "label4";
            label4.Size = new Size(91, 20);
            label4.TabIndex = 24;
            label4.Text = "Mikroçip No";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(735, 330);
            label3.Name = "label3";
            label3.Size = new Size(30, 20);
            label3.TabIndex = 23;
            label3.Text = "Yaş";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(735, 280);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 22;
            label2.Text = "Cinsi";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(736, 228);
            label1.Name = "label1";
            label1.Size = new Size(32, 20);
            label1.TabIndex = 21;
            label1.Text = "Adı";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(27, 657);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 35;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(811, 430);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(335, 27);
            dateTimePicker1.TabIndex = 37;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(700, 435);
            label5.Name = "label5";
            label5.Size = new Size(105, 20);
            label5.TabIndex = 38;
            label5.Text = "Randevu Tarihi";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 216, 63);
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Location = new Point(913, 531);
            button2.Name = "button2";
            button2.Size = new Size(250, 61);
            button2.TabIndex = 39;
            button2.Text = "Onayla";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form13
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1377, 740);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(dateTimePicker1);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form13";
            Text = "Form13";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private Label label5;
        private Button button2;
    }
}