using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Study_Assistant
{
    //Lớp animation: tạo ra hiệu ứng nổ
    class Animation
    {
        //Properties
        public int speed = 29;
        int i = 0;
        PictureBox animation = new PictureBox();
        Timer tm = new Timer();
        PictureBox Map = new PictureBox();
        //Iniatialize
        public Animation() { }
        public Animation(PictureBox Map)
        {
            this.Map = Map;
        }
        private static Image[] list
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.a1,
                    Properties.Resources.a2,
                    Properties.Resources.a3,
                    Properties.Resources.a4,
                    Properties.Resources.a5,
                    Properties.Resources.a6,
                    Properties.Resources.a7,
                    Properties.Resources.a8,
                    Properties.Resources.a9,
                    Properties.Resources.a10,
                    Properties.Resources.a11,
                    Properties.Resources.a12,
                    Properties.Resources.a13,
                    Properties.Resources.a14,
                    Properties.Resources.a15,
                    Properties.Resources.a16,
                    Properties.Resources.a17,
                    Properties.Resources.a18,
                    Properties.Resources.a19,
                    Properties.Resources.a20
                };
            }
        }
        //Method
        // ham tao animation
        public void MakeAnimation(int x, int y)
        {
            animation.SizeMode = PictureBoxSizeMode.AutoSize;
            animation.Location = new Point(x, y);
            tm.Interval = speed;
            tm.Tick += tm_Tick;
            tm.Start();
        }
        void tm_Tick(object sender, EventArgs e)
        {
            if (i < 20)
            {
                animation.Image = list[i];
                animation.BackColor = Color.Transparent;
                animation.Parent = Map;
                animation.BringToFront();
                Map.Controls.Add(animation);
            }
            i++;
            if (i == 20)
            {
                tm.Stop();
                tm.Dispose();
                animation.Dispose();
                tm = null;
                animation = null;
            }
        }
    }
}
