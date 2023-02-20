namespace Study_Assistant
{
    partial class tuanhoc
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
            this.labTuanhoc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTuanhoc
            // 
            this.labTuanhoc.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labTuanhoc.AutoSize = true;
            this.labTuanhoc.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTuanhoc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTuanhoc.Location = new System.Drawing.Point(60, 0);
            this.labTuanhoc.Name = "labTuanhoc";
            this.labTuanhoc.Size = new System.Drawing.Size(57, 19);
            this.labTuanhoc.TabIndex = 5;
            this.labTuanhoc.Text = "Sunday";
            this.labTuanhoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tuanhoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labTuanhoc);
            this.Name = "tuanhoc";
            this.Size = new System.Drawing.Size(351, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labTuanhoc;
    }
}
