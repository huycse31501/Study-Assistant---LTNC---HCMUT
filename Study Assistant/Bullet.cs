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
    //Lớp đạn: tạo ra viên đạn và di chuyển viên đạn
    abstract class Bullet
    {
        //Properties
        protected const int speed = 12;
        protected string direction;     //Hướng bắn của viên đạn
        protected Timer tm = new Timer();
        public int posX;
        public int posY;

        //Constructor
        public Bullet()
        {
            posX = posY = 0;
            direction = "left";
        }

        public Bullet(string direct, int posX, int posY)
        {
            direction = direct;
            this.posX = posX;
            this.posY = posY;
        }
        //Method
        //Tạo đạn 
        protected void CreateBullet(PictureBox bullet, PictureBox Map)
        {
            bullet.BackColor = Color.Red;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = posX;
            bullet.Top = posY;
            Map.Controls.Add(bullet);
            bullet.BringToFront();
        }
        //Đạn di chuyển sau 1 frame của timer
        public virtual void BulletControl(PictureBox Map)
        {
            tm.Interval = speed;
            tm.Tick += tm_Tick;
            tm.Start();
        }
        protected void tm_Tick(object sender, EventArgs e)
        {
            MoveBullet();
        }
        //Di chuyển đạn
        protected abstract void MoveBullet();

    }
}
