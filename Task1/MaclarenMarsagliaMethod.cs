using System;

namespace Task1
{
    class MaclarenMarsagliaMethod
        : IGenerator
    {
        private int k;
        private double[] V;
        private MultiplicativeCongruentialMethod multiplicativeCongruentialDt;

        private readonly Random random = new Random();
        private readonly object syncLock = new object();
        private double element1;
        private double element2;
        private int index;
        private double doubleElement;

        public MaclarenMarsagliaMethod(long M, long beta, int k)
        {
            V = new double[k];
            multiplicativeCongruentialDt = new MultiplicativeCongruentialMethod(beta, M);

            for (int i = 0; i < k; i++)
            {
                V[i] = RandomDouble();
            }
        }

        public double GetElement()
        {
            element1 = multiplicativeCongruentialDt.GetElement();
            element2 = RandomDouble();
            index = (int) (element2*k);
            doubleElement = V[index];
            V[index] = element1;

            return doubleElement;
        }

        private double RandomDouble()
        {
            lock (syncLock)
            {
                return random.NextDouble();
            }
        }
    }
}
