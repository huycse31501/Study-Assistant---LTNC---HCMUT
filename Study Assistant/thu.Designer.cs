namespace Study_Assistant
{
    partial class thu
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
            this.labThu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labThu
            // 
            this.labThu.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labThu.AutoSize = true;
            this.labThu.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labThu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labThu.Location = new System.Drawing.Point(2, 0);
            this.labThu.Name = "labThu";
            this.labThu.Size = new System.Drawing.Size(57, 19);
            this.labThu.TabIndex = 7;
            this.labThu.Text = "Sunday";
            // 
            // thu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labThu);
            this.Name = "thu";
            this.Size = new System.Drawing.Size(46, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labThu;
    }
}
