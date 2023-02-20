using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Study_Assistant
{
    class LevelGame
    {
        //Properties
        private int speed = 0;
        public int level = 0;
        private Label label;
        private Timer timer = new Timer();
        //Iniatialize
        public LevelGame() { }
        public LevelGame(Label label)
        {
            this.label = label;
            timer.Interval = 10;
            timer.Tick += timer_Tick;
        }
        //Method
        public void Animation()
        {
            label.Visible = true;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (speed % 2 == 0)
                label.Text = "Level " + level.ToString();
            else
                label.Text = "";
            if (speed > 150)
            {
                speed = 0;
                timer.Stop();
                label.Visible = false;
            }
            speed++;
        }

    }
}
