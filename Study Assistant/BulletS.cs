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
    /// Class tạo đạn có 3 viên
    /// </summary>
    class BulletS : Bullet
    {
        private const int RANGE = 5;    //Khoảng cách 
        private PictureBox leftBullet = new PictureBox();
        private PictureBox centerBulelt = new PictureBox();
        private PictureBox rightBullet = new PictureBox();

        public BulletS(string direct, int posX, int posY) : base(direct, posX, posY) { }

        public override void BulletControl(PictureBox Map)
        {
            CreateBullet(leftBullet, Map);
            CreateBullet(centerBulelt, Map);
            CreateBullet(rightBullet, Map);

            centerBulelt.BackColor = leftBullet.BackColor = rightBullet.BackColor = Color.Gold;
            base.BulletControl(Map);
        }
        protected override void MoveBullet()
        {
            if (direction == "left")
            {
                centerBulelt.Left -= speed;


                leftBullet.Top += RANGE;
                leftBullet.Left -= speed;

                rightBullet.Top -= RANGE;
                rightBullet.Left -= speed;
            }
            if (direction == "right")
            {
                centerBulelt.Left += speed;

                leftBullet.Top -= RANGE;
                leftBullet.Left += speed;

                rightBullet.Top += RANGE;
                rightBullet.Left += speed;
            }
            if (direction == "up")
            {
                centerBulelt.Top -= speed;

                leftBullet.Left -= RANGE;
                leftBullet.Top -= speed;

                rightBullet.Left += RANGE;
                rightBullet.Top -= speed;
            }
            if (direction == "down")
            {
                centerBulelt.Top += speed;

                leftBullet.Left += RANGE;
                leftBullet.Top += speed;

                rightBullet.Left -= RANGE;
                rightBullet.Top += speed;
            }
            //Destroy
            if (centerBulelt.Left < 0 || centerBulelt.Left > 750 || centerBulelt.Top < 30 || centerBulelt.Top > 650)
            {
                tm.Stop();
                tm.Dispose();
                centerBulelt.Dispose();
                leftBullet.Dispose();
                rightBullet.Dispose();
                tm = null;
            }
        }
    }
}
