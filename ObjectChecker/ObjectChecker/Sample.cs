using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectChecker
{
    public class Sample
    {
        private List<Attribute> attributes;
        private bool isObject;
        private double sampleWeight;

        public List<Attribute> Attributes { get => attributes; set => attributes = value; }
        public bool IsObject { get => isObject; set => isObject = value; }
        public double SampleWeight { get => sampleWeight; set => sampleWeight = value; }

        public Sample(List<Attribute> a, bool o, double w)
        {
            Attributes = a;
            IsObject = o;
            SampleWeight = w;
        }
    }
}
