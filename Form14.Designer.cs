namespace VeterinerProjectApp
{
    partial class Form14
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form14));
            button2 = new Button();
            label5 = new Label();
            dateTimePicker1 = new DateTimePicker();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 216, 63);
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Location = new Point(832, 525);
            button2.Name = "button2";
            button2.Size = new Size(250, 61);
            button2.TabIndex = 50;
            button2.Text = "Onayla";
            button2.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(623, 388);
            label5.Name = "label5";
            label5.Size = new Size(105, 20);
            label5.TabIndex = 49;
            label5.Text = "Randevu Tarihi";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(734, 383);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(335, 27);
            dateTimePicker1.TabIndex = 48;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(734, 320);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(465, 27);
            textBox4.TabIndex = 47;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(734, 276);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(465, 27);
            textBox3.TabIndex = 46;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(734, 226);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(465, 27);
            textBox2.TabIndex = 45;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(637, 323);
            label4.Name = "label4";
            label4.Size = new Size(91, 20);
            label4.TabIndex = 43;
            label4.Text = "Mikroçip No";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(658, 283);
            label3.Name = "label3";
            label3.Size = new Size(30, 20);
            label3.TabIndex = 42;
            label3.Text = "Yaş";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(658, 233);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 41;
            label2.Text = "Cinsi";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 216, 63);
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(33, 660);
            button1.Name = "button1";
            button1.Size = new Size(250, 61);
            button1.TabIndex = 51;
            button1.Text = "Ana Sayfa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form14
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1330, 747);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "Form14";
            Text = "Form14";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Label label5;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button1;
    }
}