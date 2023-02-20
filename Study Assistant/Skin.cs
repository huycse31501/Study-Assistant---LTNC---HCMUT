using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Study_Assistant
{
    class Skin
    {
        //Properties
        public Image imleft;
        public Image imright;
        public Image imup;
        public Image imdown;
        public string[] arrSkin = new string[5];
        //Initialize
        public Skin() { }
        public Skin(string[] s)
        {
            arrSkin = s;
        }
        //Method
        //Hàm tạo ảnh nhân vật theo 4 hướng
        public void MakeAvatar()
        {
            imleft = Image.FromFile(arrSkin[0]);
            imright = Image.FromFile(arrSkin[1]);
            imup = Image.FromFile(arrSkin[2]);
            imdown = Image.FromFile(arrSkin[3]);
        }
        public void MakeSkin(string sleft, string sright, string sup, string sdown)
        {
            imleft = Image.FromFile(sleft);
            imright = Image.FromFile(sright);
            imup = Image.FromFile(sup);
            imdown = Image.FromFile(sdown);
        }
        //Tạo zombie
        //+Hàm lấy hình ảnh zombie thứ nhất
        public void MakeZombie1()
        {
            imleft = Image.FromFile("Resources\\zombie1left.png");
            imright = Image.FromFile("Resources\\zombie1right.png");
            imup = Image.FromFile("Resources\\zombie1up.png");
            imdown = Image.FromFile("Resources\\zombie1down.png");
        }
        //+Hàm lấy hình ảnh superzombie
        public void MakeZombieTiny()
        {
            imleft = Image.FromFile("Resources\\tinyleft.png");
            imright = Image.FromFile("Resources\\tinyright.png");
            imup = Image.FromFile("Resources\\tinyup.png");
            imdown = Image.FromFile("Resources\\tinydown.png");
        }
        //+Hàm lấy hình ảnh zombie thứ hai
        public void MakeZombie2()
        {
            imleft = Image.FromFile("Resources\\zombie2left.png");
            imright = Image.FromFile("Resources\\zombie2right.png");
            imup = Image.FromFile("Resources\\zombie2up.png");
            imdown = Image.FromFile("Resources\\zombie2down.png");
        }
        //Tạo nhân vật
        //+Hàm lấy hình ảnh player thứ nhất
        public void MakePlayer1()
        {
            imleft = Image.FromFile("Resources\\player3left.png");
            imright = Image.FromFile("Resources\\player3right.png");
            imup = Image.FromFile("Resources\\player3up.png");
            imdown = Image.FromFile("Resources\\player3down.png");
        }
        //+Hàm lấy hình ảnh player thứ hai
        public void MakePlayer2()
        {
            imleft = Image.FromFile("Resources\\player2left.png");
            imright = Image.FromFile("Resources\\player2right.png");
            imup = Image.FromFile("Resources\\player2up.png");
            imdown = Image.FromFile("Resources\\player2down.png");
        }
        //+hàm lấy hình ảnh super player
        public void MakeSuperPlayer()
        {
            imleft = Image.FromFile("Resources\\superHeroleft.png");
            imright = Image.FromFile("Resources\\superHeroright.png");
            imup = Image.FromFile("Resources\\superHeroup.png");
            imdown = Image.FromFile("Resources\\superHerodown.png");
        }
        public void MakePlayer4()
        {
            imleft = Image.FromFile("Resources\\player4left.png");
            imright = Image.FromFile("Resources\\player4right.png");
            imup = Image.FromFile("Resources\\player4up.png");
            imdown = Image.FromFile("Resources\\player4down.png");
        }
    }
}
