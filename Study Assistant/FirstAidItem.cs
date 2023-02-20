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
    /// Class item Hộp cứu thương
    /// cộng cho player 1 lượng máu  = health
    /// </summary>
    class FirstAidItem : Item
    {
        private int health;
        public int Health
        {
            get { return health; }
        }
        //Constructor
        public FirstAidItem() : base()
        {
            item.Image = Image.FromFile(Application.StartupPath + "\\Resources\\firstaid.png");
            timeToDes = 5;  //Thời gian item tồn tại
            item.Tag = "firstaid";
        }
        //Tạo item
        public override void CreateItem(PictureBox Map)
        {
            health = 30;    //Lượng máu đc cộng thêm
            base.CreateItem(Map);
        }
    }
}
