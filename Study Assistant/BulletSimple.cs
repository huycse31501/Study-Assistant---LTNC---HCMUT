using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Study_Assistant
{
    /// <summary>
    /// Class tạo đạn 1 viên
    /// Kế thừa từ class bullet
    /// </summary>
    class BulletSimple : Bullet
    {
        private PictureBox bullet = new PictureBox();

        public BulletSimple(string direct, int posX, int posY) : base(direct, posX, posY) { }

        public override void BulletControl(PictureBox Map)
        {
            CreateBullet(bullet, Map);
            base.BulletControl(Map);
        }
        protected override void MoveBullet()
        {
            if (direction == "left")
            {
                bullet.Left -= 10;
            }
            if (direction == "right")
            {
                bullet.Left += speed;
            }
            if (direction == "up")
            {
                bullet.Top -= speed;
            }
            if (direction == "down")
            {
                bullet.Top += speed;
            }
            //Destroy
            if (bullet.Left < 0 || bullet.Left > 750 || bullet.Top < 30 || bullet.Top > 650)
            {
                tm.Stop();
                tm.Dispose();
                bullet.Dispose();
                tm = null;
                bullet = null;
            }
        }
    }
}
