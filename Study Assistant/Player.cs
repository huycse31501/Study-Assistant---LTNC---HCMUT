using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace Study_Assistant
{
    class Player : Figure
    {
        //Properties
        public int bulletLevel = 0; //Lí do thêm : Quản lí cấp độ đạn hiện tại
        private bool goup;
        private bool goleft;
        private bool goright;
        private bool godown;
        public int ammo = 100;
        private int speed = 25;
        public double playerHealth = 100;
        private string facing = "left";
        public SoundPlayer amthanh = new SoundPlayer(Properties.Resources.shoot);
        private PictureBox fire = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        //Initialize
        public Player() { }
        public Player(PictureBox Map)
        {
            this.Map = Map;
        }
        //Method
        //Tạo nhân vật
        public override void MakeAvatar()
        {
            Avatar.Image = skin.imleft;
            Avatar.SizeMode = PictureBoxSizeMode.AutoSize;
            Avatar.Location = new Point(Map.Width / 2 - Avatar.Width / 2, Map.Height / 2 - Avatar.Height / 2);
            Avatar.Tag = "Avatar";
            Map.Controls.Add(Avatar);
            Avatar.BringToFront();
        }
        //Ấn phím
        public void NhanPhim(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                facing = "left";
                Avatar.Image = skin.imleft;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                facing = "right";
                Avatar.Image = skin.imright;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                facing = "up";
                Avatar.Image = skin.imup;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                facing = "down";
                Avatar.Image = skin.imdown;
            }

            FireGun(e);
        }
        private void FireGun(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                amthanh.Play();
                if (facing == "left")
                {
                    fire.Image = Properties.Resources.fireleft;
                    fire.Location = new Point(Avatar.Left - fire.Width, Avatar.Top - 14);
                }
                if (facing == "right")
                {
                    fire.Image = Properties.Resources.fireright;
                    fire.Location = new Point(Avatar.Left + Avatar.Width, Avatar.Top + 8);
                }
                if (facing == "up")
                {
                    fire.Image = Properties.Resources.fireup;
                    fire.Location = new Point(Avatar.Left + 9, Avatar.Top - fire.Height);
                }
                if (facing == "down")
                {
                    fire.Image = Properties.Resources.firedown;
                    fire.Location = new Point(Avatar.Left - 9, Avatar.Top + Avatar.Height);
                }
                Map.Controls.Add(fire);
                fire.BringToFront();
                fire.BackColor = Color.Transparent;
                fire.Parent = Map;
            }
        }

        //Nhả phím
        public void NhaPhim(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                Shoot(facing);
                //if(ammo<1)
                //{
                //    DropAmmo();
                //}
            }
            if (e.KeyCode == Keys.Space)
            {
                Map.Controls.Remove(fire);
            }
        }

        //Di chuyển nhân vật
        public void MoveAvatar(object sender, EventArgs e)
        {
            if (goleft && Avatar.Left > 0)
            {
                Avatar.Left -= speed;
            }
            if (goright && Avatar.Left + Avatar.Width < 800)
            {
                Avatar.Left += speed;
            }
            if (goup && Avatar.Top > 30)
            {
                Avatar.Top -= speed;
            }
            if (godown && Avatar.Top + Avatar.Height < 620)
            {
                Avatar.Top += speed;
            }
        }
        //Set tọa độ viên đạn được bán ra
        private void SetLocationBullet(string direct, out int X, out int Y)
        {
            if (direct == "up")
            {
                X = Avatar.Left + 35;
                Y = Avatar.Top;
            }
            else if (direct == "down")
            {
                X = Avatar.Left + 13;
                Y = Avatar.Top + 95;
            }
            else if (direct == "left")
            {
                X = Avatar.Left + 12;
                Y = Avatar.Top + 12;
            }
            else
            {
                X = Avatar.Left + 97;
                Y = Avatar.Top + 35;
            }
        }
        //Hàm bắn đạn (đã thay đổi)
        private void Shoot(string direct)
        {
            int posX;
            int posY;
            SetLocationBullet(direct, out posX, out posY);
            Bullet gun = null;
            if (bulletLevel == 0)
                gun = new BulletSimple(direct, posX, posY);
            else if (bulletLevel == 1)
                gun = new BulletTwoRays(direct, posX, posY);
            else
                gun = new BulletS(direct, posX, posY);
            gun.BulletControl(Map);
        }
        //Hòm tiếp tế (đã chuyển thành 1 class)
        //private void DropAmmo()
        //{
        //    PictureBox ammo = new PictureBox();
        //    ammo.Image = Properties.Resources.x10;
        //    ammo.SizeMode = PictureBoxSizeMode.AutoSize;
        //    ammo.Left = rd.Next(0, 700);
        //    ammo.Top = rd.Next(32, 550);
        //    ammo.Tag = "ammo";
        //    Map.Controls.Add(ammo);
        //    ammo.BringToFront();
        //    Avatar.BringToFront();
        //}
    }
}
