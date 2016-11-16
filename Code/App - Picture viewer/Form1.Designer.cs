namespace Project_Phase1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.tbGesture = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSpeakGesture = new System.Windows.Forms.Button();
            this.tbImageName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ibPicture = new Emgu.CV.UI.ImageBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ibOriginal.Location = new System.Drawing.Point(12, 523);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(320, 240);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.LightCoral;
            this.btnPause.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(146, 228);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(164, 81);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // tbGesture
            // 
            this.tbGesture.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbGesture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGesture.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGesture.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbGesture.Location = new System.Drawing.Point(615, 612);
            this.tbGesture.Multiline = true;
            this.tbGesture.Name = "tbGesture";
            this.tbGesture.ReadOnly = true;
            this.tbGesture.Size = new System.Drawing.Size(649, 53);
            this.tbGesture.TabIndex = 4;
            this.tbGesture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(849, 581);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recognised Gesture";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 28);
            this.label2.TabIndex = 6;
            this.label2.Text = "Controlling Hand";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(597, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "Picture Viewer";
            // 
            // btnSpeakGesture
            // 
            this.btnSpeakGesture.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSpeakGesture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSpeakGesture.BackgroundImage")));
            this.btnSpeakGesture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSpeakGesture.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpeakGesture.Location = new System.Drawing.Point(1029, 220);
            this.btnSpeakGesture.Name = "btnSpeakGesture";
            this.btnSpeakGesture.Size = new System.Drawing.Size(164, 89);
            this.btnSpeakGesture.TabIndex = 8;
            this.btnSpeakGesture.UseVisualStyleBackColor = false;
            this.btnSpeakGesture.Click += new System.EventHandler(this.btnSpeakGesture_Click);
            // 
            // tbImageName
            // 
            this.tbImageName.BackColor = System.Drawing.SystemColors.ControlText;
            this.tbImageName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbImageName.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbImageName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbImageName.Location = new System.Drawing.Point(615, 699);
            this.tbImageName.Multiline = true;
            this.tbImageName.Name = "tbImageName";
            this.tbImageName.ReadOnly = true;
            this.tbImageName.Size = new System.Drawing.Size(649, 55);
            this.tbImageName.TabIndex = 11;
            this.tbImageName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label4.Location = new System.Drawing.Point(869, 668);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 28);
            this.label4.TabIndex = 12;
            this.label4.Text = "Name of Image";
            // 
            // ibPicture
            // 
            this.ibPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ibPicture.Location = new System.Drawing.Point(343, 34);
            this.ibPicture.Name = "ibPicture";
            this.ibPicture.Size = new System.Drawing.Size(640, 480);
            this.ibPicture.TabIndex = 13;
            this.ibPicture.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.textBox1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Menu;
            this.textBox1.Location = new System.Drawing.Point(339, 521);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(270, 242);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "\r\n\r\n\r\nGesture Controls-\r\n\r\nZoom In - Open Palm\r\nZoom Out - Three fingers\r\nNext Im" +
                "age - Point left \r\nPrev Image - Point right  ";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1276, 766);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ibPicture);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbImageName);
            this.Controls.Add(this.btnSpeakGesture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbGesture);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.ibOriginal);
            this.Name = "Form1";
            this.Text = "Gesture Controlled Picture Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TextBox tbGesture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSpeakGesture;
        private System.Windows.Forms.TextBox tbImageName;
        private System.Windows.Forms.Label label4;
        private Emgu.CV.UI.ImageBox ibPicture;
        private System.Windows.Forms.TextBox textBox1;
    }
}

