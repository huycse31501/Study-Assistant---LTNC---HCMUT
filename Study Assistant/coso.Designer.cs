﻿namespace Study_Assistant
{
    partial class coso
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
            this.labCoso = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labCoso
            // 
            this.labCoso.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labCoso.AutoSize = true;
            this.labCoso.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCoso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labCoso.Location = new System.Drawing.Point(3, 0);
            this.labCoso.Name = "labCoso";
            this.labCoso.Size = new System.Drawing.Size(57, 19);
            this.labCoso.TabIndex = 8;
            this.labCoso.Text = "Sunday";
            // 
            // coso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labCoso);
            this.Name = "coso";
            this.Size = new System.Drawing.Size(68, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labCoso;
    }
}
