using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    internal class HorizontalStepB : HorizontalStepA
    {
        public HorizontalStepB(int x, int y, int w, int h) : base(x, y, w, h)
        {

        }

        protected override double AverageIntensityDifference(double AverageIntensityTop, double AverageIntensityBottom)
        {
            double AverageIntensityDifference = AverageIntensityTop - AverageIntensityBottom;
            return AverageIntensityDifference;
        }
    }
}
