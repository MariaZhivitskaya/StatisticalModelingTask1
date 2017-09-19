using System;
using System.Linq;

namespace Task1
{
    public class EmpiricalDistributionFunction
        : IDistributionFunction
    {
        private readonly int n;
        private readonly double[] sequence;
        private readonly double min;
        private readonly double max;

        public EmpiricalDistributionFunction(double[] sequence)
        {
            this.sequence = sequence;
            n = sequence.Length;
            min = double.MaxValue;
            max = double.MinValue;

            foreach (var element in sequence)
            {
                min = Math.Min(min, element);
                max = Math.Max(max, element);
            }
        }

        public double F(double x)
        {
            return sequence.Aggregate<double, double>(0, (current, element) => current + (element <= x ? 1 : 0)) / n;
        }

        public double Left()
        {
            return min;
        }

        public double Right()
        {
            return max;
        }
    }
}