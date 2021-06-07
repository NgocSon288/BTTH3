
namespace QRCode.UCs
{
    partial class OrderItemUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTotal.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(67)))));
            this.lblTotal.Location = new System.Drawing.Point(1636, 32);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(127, 36);
            this.lblTotal.TabIndex = 37;
            this.lblTotal.Text = "100,000";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblName.Font = new System.Drawing.Font("Consolas", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblName.Location = new System.Drawing.Point(186, 26);
            this.lblName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(258, 42);
            this.lblName.TabIndex = 35;
            this.lblName.Text = "Tên sản phẩm";
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picImage.Location = new System.Drawing.Point(32, 1);
            this.picImage.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(94, 94);
            this.picImage.TabIndex = 34;
            this.picImage.TabStop = false;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrice.Font = new System.Drawing.Font("Consolas", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrice.Location = new System.Drawing.Point(1159, 26);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(78, 42);
            this.lblPrice.TabIndex = 38;
            this.lblPrice.Text = "Giá";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCount.Font = new System.Drawing.Font("Consolas", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCount.Location = new System.Drawing.Point(1424, 26);
            this.lblCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(178, 42);
            this.lblCount.TabIndex = 39;
            this.lblCount.Text = "Số Lượng";
            // 
            // OrderItemUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(214)))), ((int)(((byte)(229)))));
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picImage);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Name = "OrderItemUC";
            this.Size = new System.Drawing.Size(1920, 100);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblCount;
    }
}
