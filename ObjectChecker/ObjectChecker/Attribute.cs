using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public enum HaarFilterType
    {
        undetermined,
        HSA,
        HSB,
    }

    public class Attribute // this is each 'part' of a sample
    {
        private int xPos;
        private int yPos;
        private int width;
        private int height;
        private HaarFilterType type;
        private double averageIntensityDifference;

        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        internal HaarFilterType Type { get => type; set => type = value; }
        public double AverageIntensityDifference { get => averageIntensityDifference; set => averageIntensityDifference = value; }

        public Attribute(int x, int y, int w, int h, HaarFilterType t, double aid)
        {
            XPos = x;
            YPos = y;
            Width = w;
            Height = h;
            Type = t;
            AverageIntensityDifference = aid;
        }
    }
}
