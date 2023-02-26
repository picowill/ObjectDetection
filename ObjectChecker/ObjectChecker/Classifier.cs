using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public class Classifier
    {
        private int xPos;
        private int yPos;
        private int width;
        private int height;
        private HaarFilterType type;
        private double averageIntensityDifference;
        private double amountOfSay;

        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public HaarFilterType Type { get => type; set => type = value; }
        public double AverageIntensityDifference { get => averageIntensityDifference; set => averageIntensityDifference = value; }
        public double AmountOfSay { get => amountOfSay; set => amountOfSay = value; }

        public Classifier(int x, int y, int w, int h, HaarFilterType t, double av, double am)
        {
            XPos = x;
            YPos = y;
            Width = w;
            Height = h;
            Type = t;
            AverageIntensityDifference = av;
            AmountOfSay = am;
        }

    }
}
