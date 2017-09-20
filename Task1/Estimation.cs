using System;
using System.Linq;

namespace Task1
{
    public class Estimation
    {
        private readonly double[] argsFi =
        {
            0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9,
            1, 1.1,  1.2,  1.3,  1.4,  1.5,  1.6, 1.7, 1.8, 1.9, 2, 1000
        };

        private readonly double[] Fi =
        {
            0.5, 0.5398, 0.5793, 0.6179, 0.6554, 0.6915, 0.7257, 0.7580, 0.7881, 0.8159,
            0.8413, 0.8643, 0.8849, 0.9032, 0.9192, 0.9932, 0.9452, 0.9554, 0.9641, 0.9713, 0.9772, 0.999
        };

        private readonly double[] argsChi =
        {
            1.73493, 2.08790, 2.70039, 3.32511, 4.16816, 5.89883,
            8.34283, 11.38875, 14.68366, 16.91898, 19.02277, 21.66599, 23.58935, 1000
        };

        private readonly double[] Chi =
        {
            0.995, 0.990, 0.975, 0.950, 0.900, 0.750,
            0.500, 0.250, 0.100, 0.050, 0.025, 0.010, 0.005, 0.0001
        };

        private readonly double[] argsKolmogorov =
        {
           0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1,
           1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7
        };

        private readonly double[] Kolmogorov =
        {
           1.0, 0.9972, 0.9639, 0.8643, 0.7112, 0.5441, 0.3927, 0.2700,
           0.1777, 0.1122, 0.0681, 0.0397, 0.0222, 0.0120, 0.001
        };

        private const int K = 10;
        private int n;

        public double MomentsCoincidenceP1 { get; private set; }
        public double MomentsCoincidenceP2 { get; private set; }
        public double ChiSquaredCriteriaP { get; private set; }
        public double Dn { get; private set; }
        public double KolmogorovCriteriaP { get; private set; }

        public bool MomentsCoincidenceTest(double[] sequence, IDistributionFunction func, double eps)
        {
            n = sequence.Length;

            double c1 = Math.Sqrt(12 * n);
            double c2 = (n - 1.0) / n * Math.Pow(0.0056 * Math.Pow(1.0 / n, 1) + 0.0028 * Math.Pow(1.0 / n, 2) - 0.0083 * Math.Pow(1.0 / n, 3), -1.0 / 2);

            double m = sequence.Sum() / n;
            double s = sequence.Sum(x => Math.Pow(x - m, 2)) / n;

            double ksi1 = Math.Abs(m - 1.0 / 2.0);
            double ksi2 = Math.Abs(s - 1.0 / 12.0);

            MomentsCoincidenceP1 = 2 * (1 - GetFi(c1 * ksi1));
            MomentsCoincidenceP2 = 2 * (1 - GetFi(c2 * ksi2));

            return MomentsCoincidenceP1 > eps && MomentsCoincidenceP2 > eps;
        }

        public bool ChiSquaredCriteria(double[] sequence, IDistributionFunction func, double eps)
        {
            n = sequence.Length;
            int[] v = new int[K];
            double[] p = new double[K];
            double a;
            double b;
            double X2 = 0;

            foreach (var element in sequence)
            {
                v[(int)((element + func.Left()) / func.Right() * K)]++;
            }

            for (int i = 0; i < K; i++)
            {
                a = (1.0 * (i) / K) * (func.Right() - func.Left()) + func.Left();
                b = (1.0 * (i + 1) / K) * (func.Right() - func.Left()) + func.Left();
                p[i] = func.F(b) - func.F(a);
            }

            for (int i = 0; i < K; i++)
            {
                X2 += Math.Pow(v[i] - p[i] * n, 2) / (n * p[i]);
            }

            ChiSquaredCriteriaP = GetChi(X2);

            return ChiSquaredCriteriaP >= eps;
        }

        public bool KolmogorovCriteria(double[] sequence, IDistributionFunction func, double eps)
        {
            n = sequence.Length;
            IDistributionFunction empiricalDistributionFunction = new EmpiricalDistributionFunction(sequence);
            int numberOfPartitions = n * n;
            double x;
            Dn = 0;

            for (int i = 0; i < numberOfPartitions; i++)
            {
                x = (1.0 * i / numberOfPartitions) * (func.Right() - func.Left()) + func.Left();
                Dn = Math.Max(Dn, Math.Abs(empiricalDistributionFunction.F(x) - func.F(x)));
            }

            KolmogorovCriteriaP = GetKolmogorovDistributionFunction(Math.Sqrt(n) * Dn);

            return KolmogorovCriteriaP > eps;
        }

        private double GetKolmogorovDistributionFunction(double x)
        {
            for (int i = 0; i < argsKolmogorov.Length; i++)
            {
                if (x <= argsKolmogorov[i])
                {
                    return Kolmogorov[i];
                }
            }

            return -1;
        }

        private double GetFi(double x)
        {
            for (int i = 0; i < argsFi.Length; i++)
            {
                if (x <= argsFi[i])
                {
                    return Fi[i];
                }
            }

            return -1;
        }

        private double GetChi(double X2)
        {
            for (int i = 0; i < argsChi.Length; i++)
            {
                if (X2 <= argsChi[i])
                {
                    return Chi[i];
                }
            }

            return 0;
        }
    }
}