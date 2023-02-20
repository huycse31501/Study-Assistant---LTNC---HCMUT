using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace Study_Assistant
{
    /// <summary>
    /// Class item hộp tiếp đạn
    /// x5,x10,x20
    /// </summary>
    class AmmoItem : Item
    {
        private int numberAmmo;
        public int NumberAmmo
        {
            get { return numberAmmo; }
        }

        public AmmoItem() : base()
        {
            item.Tag = "ammo";
            timeToDes = 10;
        }


        //Tạo item
        public override void CreateItem(PictureBox Map)
        {
            numberAmmo = new Random().Next(0, 100);
            //có 30% đạn được tạo là x5
            if (numberAmmo < 30)
            {
                numberAmmo = 5;
                item.Image = Image.FromFile(Application.StartupPath + "\\Resources\\x5.png");
            }
            //có 55% đạn được tạo là x10
            else if (numberAmmo >= 30 && numberAmmo < 85)
            {
                numberAmmo = 10;
                item.Image = Image.FromFile(Application.StartupPath + "\\Resources\\x10.png");
            }
            //có 15% đạn đc tạo là x20
            else
            {
                numberAmmo = 20;
                item.Image = Image.FromFile(Application.StartupPath + "\\Resources\\x20.png");
            }
            base.CreateItem(Map);
        }
    }
}
