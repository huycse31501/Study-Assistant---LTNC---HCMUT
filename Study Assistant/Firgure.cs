using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;

namespace Study_Assistant
{

    abstract class Figure
    {
        //Properties
        public PictureBox Avatar = new PictureBox();
        public PictureBox Map = new PictureBox();
        public Random rd = new Random();
        public Skin skin = new Skin();
        //Method
        public abstract void MakeAvatar();
    }
}
