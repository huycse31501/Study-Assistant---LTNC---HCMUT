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
    /// Lớp trừu tượng Item
    /// </summary>
    abstract class Item
    {
        protected PictureBox item = new PictureBox();
        private Timer time = new Timer();
        protected int timeToDes;

        //Constructor
        public Item()
        {
            item.SizeMode = PictureBoxSizeMode.AutoSize;
            item.Top = item.Left = -999;
            time.Interval = 1000;
            timeToDes = 0;
            time.Tick += tm_Tick;
            time.Start();
        }

        //Tạo item
        public virtual void CreateItem(PictureBox Map)
        {
            item.Left = new Random().Next(0, 700);
            item.Top = new Random().Next(32, 550);
            Map.Controls.Add(item);
            item.BringToFront();
            CountDown();
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            --timeToDes;
        }

        //Đến ngược thời gian tồn tại của item
        protected void CountDown()
        {
            if (timeToDes <= 0)
            {
                DestroyItem();
                return;
            }
        }

        //Hàm xóa item
        public void DestroyItem()
        {
            item.Dispose();
            item = null;
            time.Stop();
            time = null;
        }
    }
}
