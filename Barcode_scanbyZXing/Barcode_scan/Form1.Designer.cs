namespace Barcode_scan
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
            this.barCodeImg = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Read1DBtn = new System.Windows.Forms.Button();
            this.OpenImgBtn = new System.Windows.Forms.Button();
            this.ImgPathTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ContentTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Create1DBtn = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.barCodeImg)).BeginInit();
            this.SuspendLayout();
            // 
            // barCodeImg
            // 
            this.barCodeImg.Location = new System.Drawing.Point(12, 109);
            this.barCodeImg.Name = "barCodeImg";
            this.barCodeImg.Size = new System.Drawing.Size(1336, 464);
            this.barCodeImg.TabIndex = 22;
            this.barCodeImg.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "图片：";
            // 
            // Read1DBtn
            // 
            this.Read1DBtn.Location = new System.Drawing.Point(442, 30);
            this.Read1DBtn.Name = "Read1DBtn";
            this.Read1DBtn.Size = new System.Drawing.Size(75, 23);
            this.Read1DBtn.TabIndex = 19;
            this.Read1DBtn.Text = "读取条形码";
            this.Read1DBtn.UseVisualStyleBackColor = true;
            this.Read1DBtn.Click += new System.EventHandler(this.Read1DBtn_Click);
            // 
            // OpenImgBtn
            // 
            this.OpenImgBtn.Location = new System.Drawing.Point(272, 57);
            this.OpenImgBtn.Name = "OpenImgBtn";
            this.OpenImgBtn.Size = new System.Drawing.Size(66, 23);
            this.OpenImgBtn.TabIndex = 18;
            this.OpenImgBtn.Text = "打开图片";
            this.OpenImgBtn.UseVisualStyleBackColor = true;
            this.OpenImgBtn.Click += new System.EventHandler(this.OpenImgBtn_Click);
            // 
            // ImgPathTxt
            // 
            this.ImgPathTxt.Location = new System.Drawing.Point(69, 59);
            this.ImgPathTxt.Name = "ImgPathTxt";
            this.ImgPathTxt.ReadOnly = true;
            this.ImgPathTxt.Size = new System.Drawing.Size(197, 21);
            this.ImgPathTxt.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "图片路径：";
            // 
            // ContentTxt
            // 
            this.ContentTxt.Location = new System.Drawing.Point(69, 30);
            this.ContentTxt.Name = "ContentTxt";
            this.ContentTxt.Size = new System.Drawing.Size(269, 21);
            this.ContentTxt.TabIndex = 14;
            this.ContentTxt.Text = "123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "条码内容：";
            // 
            // Create1DBtn
            // 
            this.Create1DBtn.Location = new System.Drawing.Point(361, 30);
            this.Create1DBtn.Name = "Create1DBtn";
            this.Create1DBtn.Size = new System.Drawing.Size(75, 23);
            this.Create1DBtn.TabIndex = 12;
            this.Create1DBtn.Text = "生成条形码";
            this.Create1DBtn.UseVisualStyleBackColor = true;
            this.Create1DBtn.Click += new System.EventHandler(this.Create1DBtn_Click);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(361, 65);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 23;
            this.btn_test.Text = "test";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 585);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.barCodeImg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Read1DBtn);
            this.Controls.Add(this.OpenImgBtn);
            this.Controls.Add(this.ImgPathTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ContentTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Create1DBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.barCodeImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox barCodeImg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Read1DBtn;
        private System.Windows.Forms.Button OpenImgBtn;
        private System.Windows.Forms.TextBox ImgPathTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ContentTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Create1DBtn;
        private System.Windows.Forms.Button btn_test;
    }
}

