using System.IO;

namespace image_quality_0721
{
    
    partial class ImageQualityForm
    {
        internal string imagepath = "";
        internal string datapath = "";
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loadbutton = new System.Windows.Forms.Button();
            this.gdcomboBox = new System.Windows.Forms.ComboBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.ngdtextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tablebutton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.brighttextBox = new System.Windows.Forms.TextBox();
            this.sharptextBox = new System.Windows.Forms.TextBox();
            this.brightsetlabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSelectImage = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxAVGorSD = new System.Windows.Forms.ComboBox();
            this.LoadImagegroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.LoadImagegroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadbutton
            // 
            this.loadbutton.Font = new System.Drawing.Font("標楷體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loadbutton.Location = new System.Drawing.Point(302, 31);
            this.loadbutton.Margin = new System.Windows.Forms.Padding(2);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(82, 32);
            this.loadbutton.TabIndex = 0;
            this.loadbutton.Text = "load ";
            this.loadbutton.UseVisualStyleBackColor = true;
            this.loadbutton.Click += new System.EventHandler(this.loadbutton_Click);
            // 
            // gdcomboBox
            // 
            this.gdcomboBox.FormattingEnabled = true;
            this.gdcomboBox.Items.AddRange(new object[] {
            "GD",
            "NGD"});
            this.gdcomboBox.Location = new System.Drawing.Point(175, 31);
            this.gdcomboBox.Margin = new System.Windows.Forms.Padding(2);
            this.gdcomboBox.Name = "gdcomboBox";
            this.gdcomboBox.Size = new System.Drawing.Size(92, 37);
            this.gdcomboBox.TabIndex = 1;
            this.gdcomboBox.SelectedIndexChanged += new System.EventHandler(this.gdcomboBox_SelectedIndexChanged);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(23, 127);
            this.imageBox1.Margin = new System.Windows.Forms.Padding(2);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(726, 551);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // ngdtextBox
            // 
            this.ngdtextBox.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ngdtextBox.Location = new System.Drawing.Point(871, 253);
            this.ngdtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ngdtextBox.Multiline = true;
            this.ngdtextBox.Name = "ngdtextBox";
            this.ngdtextBox.ReadOnly = true;
            this.ngdtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ngdtextBox.Size = new System.Drawing.Size(359, 395);
            this.ngdtextBox.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(230, 24);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 45);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "5";
            // 
            // tablebutton
            // 
            this.tablebutton.Font = new System.Drawing.Font("標楷體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tablebutton.Location = new System.Drawing.Point(353, 96);
            this.tablebutton.Margin = new System.Windows.Forms.Padding(2);
            this.tablebutton.Name = "tablebutton";
            this.tablebutton.Size = new System.Drawing.Size(103, 41);
            this.tablebutton.TabIndex = 8;
            this.tablebutton.Text = "產生table";
            this.tablebutton.UseVisualStyleBackColor = true;
            this.tablebutton.Click += new System.EventHandler(this.tablebutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(5, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "選擇樣品種類:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(88, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "閥值參數:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 16F);
            this.label1.Location = new System.Drawing.Point(867, 216);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "資料顯示:";
            // 
            // brighttextBox
            // 
            this.brighttextBox.Location = new System.Drawing.Point(230, 65);
            this.brighttextBox.Margin = new System.Windows.Forms.Padding(2);
            this.brighttextBox.Name = "brighttextBox";
            this.brighttextBox.Size = new System.Drawing.Size(76, 45);
            this.brighttextBox.TabIndex = 12;
            this.brighttextBox.Text = "0";
            // 
            // sharptextBox
            // 
            this.sharptextBox.Location = new System.Drawing.Point(230, 104);
            this.sharptextBox.Margin = new System.Windows.Forms.Padding(2);
            this.sharptextBox.Name = "sharptextBox";
            this.sharptextBox.Size = new System.Drawing.Size(76, 45);
            this.sharptextBox.TabIndex = 13;
            this.sharptextBox.Text = "0";
            // 
            // brightsetlabel
            // 
            this.brightsetlabel.AutoSize = true;
            this.brightsetlabel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.brightsetlabel.Location = new System.Drawing.Point(40, 72);
            this.brightsetlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.brightsetlabel.Name = "brightsetlabel";
            this.brightsetlabel.Size = new System.Drawing.Size(190, 24);
            this.brightsetlabel.TabIndex = 14;
            this.brightsetlabel.Text = "亮度閥值offset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(32, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "sharp閥值offset:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // comboBoxSelectImage
            // 
            this.comboBoxSelectImage.FormattingEnabled = true;
            this.comboBoxSelectImage.Items.AddRange(new object[] {
            "Golden Sample",
            "Normal Sample"});
            this.comboBoxSelectImage.Location = new System.Drawing.Point(633, 94);
            this.comboBoxSelectImage.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSelectImage.Name = "comboBoxSelectImage";
            this.comboBoxSelectImage.Size = new System.Drawing.Size(92, 26);
            this.comboBoxSelectImage.TabIndex = 17;
            this.comboBoxSelectImage.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectImage_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.brightsetlabel);
            this.groupBox1.Controls.Add(this.sharptextBox);
            this.groupBox1.Controls.Add(this.comboBoxAVGorSD);
            this.groupBox1.Controls.Add(this.brighttextBox);
            this.groupBox1.Controls.Add(this.tablebutton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(776, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(493, 168);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "規格表建立";
            // 
            // comboBoxAVGorSD
            // 
            this.comboBoxAVGorSD.FormattingEnabled = true;
            this.comboBoxAVGorSD.Items.AddRange(new object[] {
            "Mean",
            "Deviation"});
            this.comboBoxAVGorSD.Location = new System.Drawing.Point(353, 50);
            this.comboBoxAVGorSD.Name = "comboBoxAVGorSD";
            this.comboBoxAVGorSD.Size = new System.Drawing.Size(121, 40);
            this.comboBoxAVGorSD.TabIndex = 19;
            // 
            // LoadImagegroupBox
            // 
            this.LoadImagegroupBox.Controls.Add(this.loadbutton);
            this.LoadImagegroupBox.Controls.Add(this.gdcomboBox);
            this.LoadImagegroupBox.Controls.Add(this.label3);
            this.LoadImagegroupBox.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LoadImagegroupBox.Location = new System.Drawing.Point(40, 12);
            this.LoadImagegroupBox.Name = "LoadImagegroupBox";
            this.LoadImagegroupBox.Size = new System.Drawing.Size(416, 100);
            this.LoadImagegroupBox.TabIndex = 20;
            this.LoadImagegroupBox.TabStop = false;
            this.LoadImagegroupBox.Text = "資料讀取 ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(548, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "圖片選擇:";
            // 
            // ImageQualityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 711);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSelectImage);
            this.Controls.Add(this.LoadImagegroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ngdtextBox);
            this.Controls.Add(this.imageBox1);
            this.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImageQualityForm";
            this.Text = "OPT quality";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.LoadImagegroupBox.ResumeLayout(false);
            this.LoadImagegroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadbutton;
        private System.Windows.Forms.ComboBox gdcomboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button tablebutton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox brighttextBox;
        private System.Windows.Forms.TextBox sharptextBox;
        private System.Windows.Forms.Label brightsetlabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSelectImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxAVGorSD;
        private System.Windows.Forms.GroupBox LoadImagegroupBox;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox ngdtextBox;
        public Emgu.CV.UI.ImageBox imageBox1;
    }
}

