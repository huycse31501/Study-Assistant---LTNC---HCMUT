namespace Study_Assistant
{
    partial class MaMH
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
            this.labMMH1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labMMH1
            // 
            this.labMMH1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labMMH1.AutoSize = true;
            this.labMMH1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMMH1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labMMH1.Location = new System.Drawing.Point(-4, 0);
            this.labMMH1.Name = "labMMH1";
            this.labMMH1.Size = new System.Drawing.Size(57, 19);
            this.labMMH1.TabIndex = 3;
            this.labMMH1.Text = "Sunday";
            // 
            // MaMH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labMMH1);
            this.Name = "MaMH";
            this.Size = new System.Drawing.Size(60, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labMMH1;
    }
}
