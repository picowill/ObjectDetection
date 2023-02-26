using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public class HaarFilter
    {
        private int xPos;
        private int yPos;
        private int width;
        private int height;

        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public HaarFilter(int x, int y, int w, int h)
        {
            XPos = x;
            YPos = y;
            Width = w;
            Height = h;
        }
    }
}
