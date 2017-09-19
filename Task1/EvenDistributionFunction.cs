using System;

namespace Task1
{
    public class EvenDistributionFunction
        : IDistributionFunction
    {
        public double F(double x)
        {
            return Math.Max(0, Math.Min(1, x));
        }

        public double Left()
        {
            return 0;
        }

        public double Right()
        {
            return 1;
        }
    }
}