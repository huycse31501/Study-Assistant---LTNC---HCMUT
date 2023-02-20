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
    public partial class FormOptions : Form
    {
        //Properties
        private FormMenu frmMenu;
        //Initialize
        public FormOptions()
        {
            InitializeComponent();
        }
        public FormOptions(FormMenu form)
        {
            InitializeComponent();
            this.frmMenu = form;
            SetTransparent();
        }
        //Method
        //+Hàm làm trong suốt background của các picturebox so với hình nền
        private void SetTransparent()
        {
            this.picPlayer1.Parent = picBackground;
            this.picPlayer2.Parent = picBackground;
            this.picPlayer3.Parent = picBackground;
            this.picPlayer4.Parent = picBackground;

            this.rdPlayer1.Parent = picBackground;
            this.rdPlayer2.Parent = picBackground;
            this.rdPlayer3.Parent = picBackground;
            this.rdPlayer4.Parent = picBackground;

            this.picOK.Parent = picBackground;
        }
        //Hàm xử lý sự kiện khi nhấn button home
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMenu.Show();
        }
        //Hàm xử lý xự kiện khi nhấn các radio button
        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (rdPlayer1.Checked == true)
                frmMenu.select = 0;
            else if (rdPlayer2.Checked == true)
                frmMenu.select = 1;
            else if (rdPlayer3.Checked == true)
                frmMenu.select = 2;
            else if (rdPlayer4.Checked == true)
                frmMenu.select = 3;
        }

        private void picOK_MouseHover(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Red;
        }

        private void picOK_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            btn.BackColor = Color.Transparent;
        }


        private void rdPlayer4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void picPlayer1_Click_1(object sender, EventArgs e)
        {

        }

        private void rdPlayer4_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void FormOptions_Load(object sender, EventArgs e)
        {

        }

        private void picOK_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMenu.Show();
        }
    }
}
