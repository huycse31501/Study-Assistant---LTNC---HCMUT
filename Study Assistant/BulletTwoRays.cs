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
    /// Class đạn có 2 viên song song
    /// kế thừa từ Class Bullet
    /// </summary>
    class BulletTwoRays : Bullet
    {
        private const int RANGE = 15;   //Khoảng cách 2 viên đạn
        private PictureBox leftBullet = new PictureBox();
        private PictureBox rightBullet = new PictureBox();

        public BulletTwoRays(string direct, int posX, int posY) : base(direct, posX, posY) { }

        public override void BulletControl(PictureBox Map)
        {
            CreateBullet(leftBullet, Map);
            CreateBullet(rightBullet, Map);
            leftBullet.BackColor = rightBullet.BackColor = Color.Orange;
            base.BulletControl(Map);
        }
        protected override void MoveBullet()
        {
            if (direction == "left")
            {
                leftBullet.Location = new Point(leftBullet.Location.X, posY + RANGE);
                rightBullet.Location = new Point(rightBullet.Location.X, posY - RANGE);
                leftBullet.Left -= speed;
                rightBullet.Left -= speed;
            }
            if (direction == "right")
            {
                leftBullet.Location = new Point(leftBullet.Location.X, posY - RANGE);
                rightBullet.Location = new Point(rightBullet.Location.X, posY + RANGE);
                leftBullet.Left += speed;
                rightBullet.Left += speed;
            }
            if (direction == "up")
            {
                leftBullet.Location = new Point(posX - RANGE, leftBullet.Location.Y);
                rightBullet.Location = new Point(posX + RANGE, rightBullet.Location.Y);
                leftBullet.Top -= speed;
                rightBullet.Top -= speed;
            }
            if (direction == "down")
            {
                leftBullet.Location = new Point(posX + RANGE, leftBullet.Location.Y);
                rightBullet.Location = new Point(posX - RANGE, rightBullet.Location.Y);

                leftBullet.Top += speed;
                rightBullet.Top += speed;
            }
            //Đạn nằm ngoài vùng nhìn thấy
            if (leftBullet.Left < 0 || leftBullet.Left > 750 || leftBullet.Top < 30 || leftBullet.Top > 650)
            {
                tm.Stop();
                tm.Dispose();
                leftBullet.Dispose();
                rightBullet.Dispose();
                tm = null;
            }
        }
    }
}
