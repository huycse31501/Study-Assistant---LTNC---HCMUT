using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Study_Assistant
{
    //Lớp trừu tượng enemy: tạo ra kẻ thủ và cách di chuyển của chúng đối với player
    abstract class Enemy : Figure
    {
        //Properties
        public string direction;
        protected int speed = 1;
        //Initialize
        public Enemy() { }
        public Enemy(PictureBox MAP)
        {
            Map = MAP;
        }
        //Method
        //Di chuyển Enemy
        public void MoveEnemy(Player player)
        {
            //bug
            if (Avatar.Left > player.Avatar.Left)
            {
                Avatar.Left -= speed;
                Avatar.Image = skin.imleft;
            }
            if (Avatar.Left > player.Avatar.Top)
            {
                Avatar.Top -= speed;
                Avatar.Image = skin.imup;
            }
            if (Avatar.Left < player.Avatar.Left)
            {
                Avatar.Left += speed;
                Avatar.Image = skin.imright;
            }
            if (Avatar.Top < player.Avatar.Top)
            {
                Avatar.Top += speed;
                Avatar.Image = skin.imdown;
            }
        }
        //check
        bool IsColl()
        {
            foreach (Control item in Map.Controls)
            {
                if (item is PictureBox && Equals(item.Tag, "zombie") && item != Avatar)
                    if (Avatar.Bounds.IntersectsWith(item.Bounds))
                        return true;
            }
            return false;
        }
    }
}
