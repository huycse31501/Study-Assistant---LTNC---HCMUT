namespace Study_Assistant
{
    partial class FormGameplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameplay));
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.hp = new System.Windows.Forms.ProgressBar();
            this.lbHealth = new System.Windows.Forms.Label();
            this.lbKills = new System.Windows.Forms.Label();
            this.lbAmmo = new System.Windows.Forms.Label();
            this.lbPause = new System.Windows.Forms.Label();
            this.lbLevel = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.btnResum = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.MAP = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnQuit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAP)).BeginInit();
            this.SuspendLayout();
            // 
            // timerGame
            // 
            this.timerGame.Enabled = true;
            this.timerGame.Interval = 20;
            this.timerGame.Tick += new System.EventHandler(this.LoadFrames);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.hp);
            this.panel1.Controls.Add(this.lbHealth);
            this.panel1.Controls.Add(this.lbKills);
            this.panel1.Controls.Add(this.lbAmmo);
            this.panel1.Location = new System.Drawing.Point(1, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 37);
            this.panel1.TabIndex = 2;
            // 
            // hp
            // 
            this.hp.Location = new System.Drawing.Point(740, 2);
            this.hp.Margin = new System.Windows.Forms.Padding(4);
            this.hp.Name = "hp";
            this.hp.Size = new System.Drawing.Size(301, 33);
            this.hp.TabIndex = 3;
            this.hp.Click += new System.EventHandler(this.hp_Click);
            // 
            // lbHealth
            // 
            this.lbHealth.AutoSize = true;
            this.lbHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHealth.ForeColor = System.Drawing.Color.White;
            this.lbHealth.Location = new System.Drawing.Point(616, 1);
            this.lbHealth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHealth.Name = "lbHealth";
            this.lbHealth.Size = new System.Drawing.Size(108, 31);
            this.lbHealth.TabIndex = 2;
            this.lbHealth.Text = "Health:";
            // 
            // lbKills
            // 
            this.lbKills.AutoSize = true;
            this.lbKills.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKills.ForeColor = System.Drawing.Color.White;
            this.lbKills.Location = new System.Drawing.Point(329, 4);
            this.lbKills.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbKills.Name = "lbKills";
            this.lbKills.Size = new System.Drawing.Size(102, 31);
            this.lbKills.TabIndex = 2;
            this.lbKills.Text = "Kills: 0";
            // 
            // lbAmmo
            // 
            this.lbAmmo.AutoSize = true;
            this.lbAmmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAmmo.ForeColor = System.Drawing.Color.White;
            this.lbAmmo.Location = new System.Drawing.Point(33, 4);
            this.lbAmmo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAmmo.Name = "lbAmmo";
            this.lbAmmo.Size = new System.Drawing.Size(128, 31);
            this.lbAmmo.TabIndex = 1;
            this.lbAmmo.Text = "Ammo: 0";
            // 
            // lbPause
            // 
            this.lbPause.BackColor = System.Drawing.Color.DarkGreen;
            this.lbPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPause.ForeColor = System.Drawing.Color.Yellow;
            this.lbPause.Location = new System.Drawing.Point(354, 263);
            this.lbPause.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPause.Name = "lbPause";
            this.lbPause.Size = new System.Drawing.Size(365, 112);
            this.lbPause.TabIndex = 8;
            this.lbPause.Text = "PAUSE";
            this.lbPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPause.Visible = false;
            // 
            // lbLevel
            // 
            this.lbLevel.BackColor = System.Drawing.Color.Transparent;
            this.lbLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLevel.ForeColor = System.Drawing.Color.Yellow;
            this.lbLevel.Location = new System.Drawing.Point(405, 91);
            this.lbLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(259, 74);
            this.lbLevel.TabIndex = 9;
            this.lbLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLevel.Visible = false;
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.DarkGreen;
            this.btnQuit.Image = global::Study_Assistant.Properties.Resources.Exit__1_;
            this.btnQuit.Location = new System.Drawing.Point(612, 409);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(107, 98);
            this.btnQuit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnQuit.TabIndex = 7;
            this.btnQuit.TabStop = false;
            this.btnQuit.Visible = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.DarkGreen;
            this.btnHome.Image = global::Study_Assistant.Properties.Resources.Home__1_;
            this.btnHome.Location = new System.Drawing.Point(481, 409);
            this.btnHome.Margin = new System.Windows.Forms.Padding(4);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(107, 98);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 6;
            this.btnHome.TabStop = false;
            this.btnHome.Visible = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnResum
            // 
            this.btnResum.BackColor = System.Drawing.Color.DarkGreen;
            this.btnResum.Image = global::Study_Assistant.Properties.Resources.Reload__1_;
            this.btnResum.Location = new System.Drawing.Point(353, 409);
            this.btnResum.Margin = new System.Windows.Forms.Padding(4);
            this.btnResum.Name = "btnResum";
            this.btnResum.Size = new System.Drawing.Size(107, 98);
            this.btnResum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnResum.TabIndex = 5;
            this.btnResum.TabStop = false;
            this.btnResum.Visible = false;
            this.btnResum.Click += new System.EventHandler(this.btnResum_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.BackColor = System.Drawing.Color.Black;
            this.btnPause.Image = global::Study_Assistant.Properties.Resources.Pause__1_;
            this.btnPause.Location = new System.Drawing.Point(963, 44);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(67, 62);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 3;
            this.btnPause.TabStop = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // MAP
            // 
            this.MAP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MAP.BackgroundImage")));
            this.MAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MAP.Location = new System.Drawing.Point(1, 4);
            this.MAP.Margin = new System.Windows.Forms.Padding(4);
            this.MAP.Name = "MAP";
            this.MAP.Size = new System.Drawing.Size(1041, 750);
            this.MAP.TabIndex = 1;
            this.MAP.TabStop = false;
            // 
            // FormGameplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1045, 752);
            this.Controls.Add(this.lbLevel);
            this.Controls.Add(this.lbPause);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.btnResum);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MAP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zombie Shooter";
            this.Load += new System.EventHandler(this.FormGameplay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnQuit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.PictureBox MAP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar hp;
        private System.Windows.Forms.Label lbHealth;
        private System.Windows.Forms.Label lbKills;
        private System.Windows.Forms.Label lbAmmo;
        private System.Windows.Forms.PictureBox btnPause;
        private System.Windows.Forms.PictureBox btnResum;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.PictureBox btnQuit;
        private System.Windows.Forms.Label lbPause;
        private System.Windows.Forms.Label lbLevel;
    }
}

