using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study_Assistant
{
    public partial class FormMenu : Form
    {
        //Properties
        private List<PictureBox> buttonList = new List<PictureBox>();
        private LoadData data = new LoadData();
        private FormGameplay frmGameplay;
        private FormOptions frmOptions;
        private LoadingGame gameLoading;
        private MainForm mainForm;
        public int select = 0;

        //Iniatialize
        public FormMenu()
        {
            InitializeComponent();
            DrawLogin();
            data.LoadScore("input.txt");
            frmGameplay = new FormGameplay(this);
            frmOptions = new FormOptions(this);
            gameLoading = new LoadingGame(frmGameplay);
            frmOptions.Hide();
            frmGameplay.Hide();
        }
        //Method

        //+Nhóm khởi tạo các button
        private void GetImage()
        {
            buttonList[0].Image = Properties.Resources.btnPlay;
            buttonList[1].Image = Properties.Resources.btnCharacter;
            buttonList[2].Image = Properties.Resources.btnHighScore;
            buttonList[3].Image = Properties.Resources.btnAbout;
            buttonList[4].Image = Properties.Resources.btnQuit;
            buttonList[5].Image = Properties.Resources.btnReplay;
            buttonList[6].Image = Properties.Resources.btnHome;
        }
        private void InitButtons()
        {
            PictureBox picTemp = new PictureBox { Height = 0, Location = new Point(111, 20) };
            for (int i = 0; i < 7; i++)
            {
                PictureBox ptb = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    BackColor = Color.Transparent,
                    Location = new Point(picTemp.Location.X, picTemp.Location.Y + picTemp.Height + 20),
                    Tag = i.ToString()
                };
                picTemp = ptb;
                ptb.Click += ptb_Click;
                ptb.MouseHover += ptb_MouseHover;
                ptb.MouseLeave += ptb_MouseLeave;
                buttonList.Add(ptb);
            }
            GetImage();
        }

        void ptb_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Transparent;
        }

        void ptb_MouseHover(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Red;
        }
        private void DrawLogin()
        {
            InitButtons();
            for (int i = 0; i < 5; i++)
                this.Controls.Add(buttonList[i]);
        }
        //

        //+Hàm xử lý sự kiện các button được nhấn
        private void ptb_Click(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            if (Equals(btn.Tag, "0"))
            {
                this.Hide();
                frmGameplay.Show();
                gameLoading.InitEvents();
            }
            if (Equals(btn.Tag, "1"))
                ShowFormCharacter();
            if (Equals(btn.Tag, "2"))
                ShowFormHighscore();
            if (Equals(btn.Tag, "3"))
                ShowFormAbout();
            if (Equals(btn.Tag, "4"))
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thoát chứ ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    this.Close();
                    //mainForm = new MainForm();
                    //mainForm.Show();
                    
            }
            if (Equals(btn.Tag, "5"))
            {
                this.Hide();
                frmGameplay.GameStart();
            }
            if (Equals(btn.Tag, "6"))
                ShowFormLogin();
        }
        //Ham show About 
        private void ShowFormAbout()
        {
            PictureBox btnAbout = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Tag = "about"
            };
            btnAbout.Image = Properties.Resources.about21;
            this.Controls.Add(btnAbout);
            btnAbout.BringToFront();
            PictureBox btnHome = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                BackColor = Color.Red,
                Location = new Point(btnAbout.Width - 65, 5),
                Tag = "home"
            };
            btnHome.Image = Properties.Resources.picHome;
            btnHome.Click += picHome_Click;
            btnHome.MouseHover += btnHome_MouseHover;
            btnHome.MouseLeave += btnHome_MouseLeave;
            btnAbout.Controls.Add(btnHome);
        }

        void btnHome_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Transparent;
        }

        void btnHome_MouseHover(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Red;
        }
        private void ShowFormLogin()
        {
            this.BackgroundImage = Properties.Resources.bgrLG11;
            foreach (Control x in this.Controls)
                if (x is PictureBox)
                {
                    if (Equals(x.Tag, "highscore"))
                    {
                        this.Controls.Remove(x);
                    }
                }
            for (int i = 0; i < 5; i++)
                buttonList[i].Visible = true;
            this.Controls.Remove(buttonList[5]);
            this.Controls.Remove(buttonList[6]);
            frmGameplay.Hide();
            this.Show();
        }
        private void ShowFormCharacter()
        {
            frmOptions.Show();
        }
        private void ShowFormHighscore()
        {
            PictureBox btnHighscore = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Tag = "highscore"
            };
            btnHighscore.Image = Properties.Resources.picHighScore;
            this.Controls.Add(btnHighscore);
            List<Label> scoreList = new List<Label>();
            for (int i = 0; i < 4; i++)
                buttonList[i].Visible = false;
            for (int i = 0; i < 4; i++)
            {
                Label lb = new Label
                {
                    BackColor = Color.Transparent,
                    Location = new Point(buttonList[i].Location.X, buttonList[i].Location.Y),
                    Font = new Font("Times New Roman", 20.0f, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                scoreList.Add(lb);
            }
            scoreList[0].Text = "1st. " + data.scores[3].ToString();
            scoreList[1].Text = "2nd. " + data.scores[2].ToString();
            scoreList[2].Text = "3rd. " + data.scores[1].ToString();
            scoreList[3].Text = "4th. " + data.scores[0].ToString();
            buttonList[6].Location = new Point(buttonList[0].Location.X, buttonList[4].Location.Y);
            foreach (Label item in scoreList)
                btnHighscore.Controls.Add(item);
            buttonList[4].Visible = false;
            btnHighscore.Controls.Add(buttonList[6]);
        }
        public void ShowFormEnd()
        {
            this.BackgroundImage = Properties.Resources.bgEnd2;
            buttonList[5].Location = new Point(buttonList[0].Location.X, buttonList[0].Location.Y);
            buttonList[6].Location = new Point(buttonList[2].Location.X, buttonList[2].Location.Y);
            for (int i = 0; i < 5; i++)
                if (i != 1)
                    buttonList[i].Visible = false;
            this.Controls.Add(buttonList[5]);
            this.Controls.Add(buttonList[6]);
        }
        private void picHome_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.bgrLG;
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (Equals(item.Tag, "about") || Equals(item.Tag, "home")))
                {
                    item.Dispose();
                    this.Controls.Remove(item);
                }
            }
        }
        public void UpdateScore(int score)
        {
            data.UpdateScore(score);
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }

}
