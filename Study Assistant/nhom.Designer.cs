namespace Study_Assistant
{
    partial class nhom
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
            this.labNhom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labNhom
            // 
            this.labNhom.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labNhom.AutoSize = true;
            this.labNhom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNhom.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labNhom.Location = new System.Drawing.Point(3, 0);
            this.labNhom.Name = "labNhom";
            this.labNhom.Size = new System.Drawing.Size(57, 19);
            this.labNhom.TabIndex = 6;
            this.labNhom.Text = "Sunday";
            // 
            // nhom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labNhom);
            this.Name = "nhom";
            this.Size = new System.Drawing.Size(62, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labNhom;
    }
}
