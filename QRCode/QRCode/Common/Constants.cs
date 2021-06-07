using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCode.Common
{
    public static class Constants
    {
        public static readonly string SEPERATE_CHAR = "CS511";

        public static readonly Color MAIN_BACK_COLOR = Color.FromArgb(188, 206, 229);
        public static readonly Color MAIN_FORE_COLOR = Color.FromArgb(19, 15, 64);
        public static readonly Color BORDER_MENU_LEFT_COLOR = Color.FromArgb(249, 88, 155);
        public static readonly Color CONTROLS_WINDOW_MOUSE_OVER_BACK_COLOR = Color.FromArgb(168, 192, 221);
        public static readonly Color COLOR_NO_ACTIVE = Color.FromArgb(200, 214, 229);
        public static readonly Color COLOR_ACTIVE = Color.FromArgb(170, 190, 215);



        public static fMain MainForm = null;
    }
}
