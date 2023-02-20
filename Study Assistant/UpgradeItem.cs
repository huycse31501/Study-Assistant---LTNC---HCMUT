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
    /// Class item nâng cấp đạn
    /// </summary>
    class UpgradeItem : Item
    {
        public UpgradeItem() : base()
        {
            item.Image = Image.FromFile(Application.StartupPath + "\\Resources\\upgrade.png");
            item.Tag = "upgrade";
            timeToDes = 15;
        }
        public override void CreateItem(PictureBox Map)
        {
            base.CreateItem(Map);
        }
    }
}
