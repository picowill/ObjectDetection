using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public class HorizontalStepA : HaarFilter
    {
        public HorizontalStepA(int x, int y, int w, int h) : base(x, y, w, h)
        {

        }

        public double Apply(double[,] IntegralImageArray)
        {
            double IntensityTotal;
            double IntensityTop;
            if ((XPos - (Width - 1) == 0) && (YPos - (Height - 1) == 0))
            {
                IntensityTotal = IntegralImageArray[XPos, YPos];
                IntensityTop = IntegralImageArray[XPos, YPos - (Height / 2)];
            }
            else if (YPos - (Height - 1) == 0)
            {
                IntensityTotal = IntegralImageArray[XPos, YPos] - IntegralImageArray[XPos - Width, YPos];
                IntensityTop = IntegralImageArray[XPos, YPos - (Height / 2)] - IntegralImageArray[XPos - Width, YPos - (Height / 2)];
            }
            else if (XPos - (Width - 1) == 0)
            {
                IntensityTotal = IntegralImageArray[XPos, YPos] - IntegralImageArray[XPos, YPos - Height];
                IntensityTop = IntegralImageArray[XPos, YPos - (Height / 2)] - IntegralImageArray[XPos, YPos - Height];
            }
            else
            {
                IntensityTotal = IntegralImageArray[XPos, YPos] - IntegralImageArray[XPos, YPos - Height] - IntegralImageArray[XPos - Width, YPos] + IntegralImageArray[XPos - Width, YPos - Height];
                IntensityTop = IntegralImageArray[XPos, YPos - (Height / 2)] - IntegralImageArray[XPos - Width, YPos - (Height / 2)] - IntegralImageArray[XPos, YPos - Height] + IntegralImageArray[XPos - Width, YPos - Height];
            }

            double AverageIntensityTop = IntensityTop / (Width * (Height / 2));
            double IntensityBottom = IntensityTotal - IntensityTop;
            double AverageIntensityBottom = IntensityBottom / (Width * (Height / 2));


            return AverageIntensityDifference(AverageIntensityTop, AverageIntensityBottom);
        }

        protected virtual double AverageIntensityDifference(double AIT, double AIB)
        {
            double AverageIntensityDifference = AIB - AIT;
            return AverageIntensityDifference;
        }
    }
}
