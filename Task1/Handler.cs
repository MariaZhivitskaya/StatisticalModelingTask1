using System;
using System.IO;

namespace Task1
{
    public class Handler
    {
        public static long Beta(int m)
        {
            return (long) (Math.Pow(2, m) + 3);
        }

        public static double[] Generate(IGenerator generator, int n)
        {
            double[] sequence = new double[n];

            for (int i = 0; i < n; i++)
            {
                sequence[i] = generator.GetElement();
            }

            return sequence;
        }

        public static void WriteFile(double[] sequence, string fileName, int m, int n)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine("m = {0}, n = {1}\n", m, n);
                foreach (var element in sequence)
                {
                    file.Write("{0} ", element);
                }
                file.WriteLine("\n");
            }
        }

        public static void WriteFileMomentstCoincidenceTest(double p1, double p2, bool isCorrect, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine("Momentst Coincidence Test");
                file.WriteLine("p1 = {0}, p2 = {1}\n", p1, p2);
                file.WriteLine(WriteResult(isCorrect));
                file.WriteLine("-----------------------------------------");
            }
        }

        public static void WriteFileChiSquaredCriteria(double p, bool isCorrect, string fileName)
        {
            using (var file = new StreamWriter(fileName, true))
            {
                file.WriteLine("Chi Squared Criteria");
                file.WriteLine("p = {0}\n", p);
                file.WriteLine(WriteResult(isCorrect));
                file.WriteLine("-----------------------------------------");
            }
        }

        public static void WriteFileKolmogorovCriteria(double p, double Dn, bool isCorrect, string fileName)
        {
            using (var file = new StreamWriter(fileName, true))
            {
                file.WriteLine("Kolmogorov Criteria");
                file.WriteLine("Dn = {0}, p = {1}\n", Dn, p);
                file.WriteLine(WriteResult(isCorrect));
                file.WriteLine("-----------------------------------------");
            }
        }

        private static string WriteResult(bool isCorrect)
        {
            return "Random selection is " + (isCorrect ? "correct" : "not correct");
        }
    }
}
