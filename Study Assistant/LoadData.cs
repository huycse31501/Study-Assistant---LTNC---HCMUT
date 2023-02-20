using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
namespace Study_Assistant
{
    //Lớp lấy và lưu giữ dữ liệu điểm của người chơi
    class LoadData
    {

        //Properties
        public List<int> scores = new List<int>();
        //Method
        //Hàm load điểm
        public void LoadScore(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string value = "";
            do
            {
                value = sr.ReadLine();
                if (value != null)
                    scores.Add(Convert.ToInt32(value));
            } while (value != null);
            scores.Sort();
            sr.Close();
        }
        //Hàm save điểm
        private void SaveScore(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (int x in scores)
                sw.WriteLine(x.ToString());
            sw.Close();
        }
        //Hàm kiểm tra xem điểm của người chơi có nằm trong top 4 không, có thì
        //lưu lại
        public void UpdateScore(int score)
        {
            int min = scores.Min();
            if (score > min)
                scores[0] = score;
            scores.Sort();
            SaveScore("input.txt");
        }
    }
}
