namespace ImageClipper
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.select_img = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // select_img
            // 
            this.select_img.Location = new System.Drawing.Point(164, 427);
            this.select_img.Name = "select_img";
            this.select_img.Size = new System.Drawing.Size(171, 58);
            this.select_img.TabIndex = 0;
            this.select_img.Text = "Select Image";
            this.select_img.UseVisualStyleBackColor = true;
            this.select_img.Click += new System.EventHandler(this.select_img_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(35, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(441, 376);
            this.listBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 532);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.select_img);
            this.Name = "Form1";
            this.Text = "ImageClipper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button select_img;
        private System.Windows.Forms.ListBox listBox1;
    }
}

