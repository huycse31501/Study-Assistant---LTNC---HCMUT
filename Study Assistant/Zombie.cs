using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using System.IO;

namespace Study_Assistant
{
    //Lớp zombie kế thừa từ lớp Enemy
    class Zombie : Enemy
    {
        //Properties
        private int x;
        private int y;
        //Iniatialize
        public Zombie(PictureBox MAP)
        {
            Map = MAP;
        }
        public Zombie(PictureBox MAP, int x, int y, string direc)
            : base(MAP)
        {
            this.x = x;
            this.y = y;
            direction = direc;
        }
        //Method
        //Tạo zombie
        public override void MakeAvatar()
        {
            skin.MakeZombie1();
            MakeDirection();
            Avatar.SizeMode = PictureBoxSizeMode.AutoSize;
            Avatar.Tag = "zombie";
            Avatar.Location = new Point(x, y);
            Map.Controls.Add(Avatar);
        }
        private void MakeDirection()
        {
            if (direction == "left")
                Avatar.Image = skin.imleft;
            else if (direction == "right")
                Avatar.Image = skin.imright;
            else if (direction == "up")
                Avatar.Image = skin.imup;
            else if (direction == "down")
                Avatar.Image = skin.imdown;
        }
        public void IncreaseSpeed(int level)
        {
            speed = level;
        }

    }
}
