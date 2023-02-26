using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectChecker
{
    public partial class ObjectChecker : Form
    {
        private static List<Sample> MergeSort(List<Sample> unsorted, int count)
        {
            if (unsorted.Count <= 1)
                return unsorted;

            List<Sample> left = new List<Sample>();
            List<Sample> right = new List<Sample>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }

            left = MergeSort(left, count);
            right = MergeSort(right, count);
            return Merge(left, right, count);
        }

        private static List<Sample> Merge(List<Sample> left, List<Sample> right, int count)
        {
            List<Sample> result = new List<Sample>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].Attributes[count].AverageIntensityDifference <= right[0].Attributes[count].AverageIntensityDifference)  // Comparing First two elements to see which is smaller
                    {
                        result.Add(left[0]);
                        left.Remove(left[0]);      //Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(right[0]);
                        right.Remove(right[0]);
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else if (right.Count > 0)
                {
                    result.Add(right[0]);

                    right.Remove(right[0]);
                }
            }

            return result;
        }
    }
}