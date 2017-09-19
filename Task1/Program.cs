using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1000;
            int m = 24;
            long M = 2147483648;
            long beta = Handler.Beta(m);
            int k = 100;
            bool estimationResult;

            double[] MCM = Handler.Generate(new MultiplicativeCongruentialMethod(M, beta), n);
            double[] MMM = Handler.Generate(new MaclarenMarsagliaMethod(M, beta, k), n);

            string MCMFileName = "MCMGenerator.txt";
            string MMMFileName = "MMMGenerator.txt";
            string testFileNameMCM = "TestsResultsMCM.txt";
            string testFileNameMMM = "TestsResultsMMM.txt";

            Handler.WriteFile(MCM, MCMFileName, m, n);
            Handler.WriteFile(MMM, MMMFileName, m, n);

            Estimation est = new Estimation();
            EvenDistributionFunction evenDistributionFunction = new EvenDistributionFunction();
            double eps = 0.01;

            estimationResult = est.MomentsCoincidenceTest(MCM, evenDistributionFunction, eps);
            Handler.WriteFileMomentstCoincidenceTest(est.MomentsCoincidenceP1, est.MomentsCoincidenceP2, estimationResult, testFileNameMCM);
            estimationResult = est.ChiSquaredCriteria(MCM, evenDistributionFunction, eps);
            Handler.WriteFileChiSquaredCriteria(est.ChiSquaredCriteriaP, estimationResult, testFileNameMCM);
            estimationResult = est.KolmogorovCriteria(MCM, evenDistributionFunction, eps);
            Handler.WriteFileKolmogorovCriteria(est.KolmogorovCriteriaP, est.Dn, estimationResult, testFileNameMCM);

            estimationResult = est.MomentsCoincidenceTest(MMM, evenDistributionFunction, eps);
            Handler.WriteFileMomentstCoincidenceTest(est.MomentsCoincidenceP1, est.MomentsCoincidenceP2, estimationResult, testFileNameMMM);
            estimationResult = est.ChiSquaredCriteria(MMM, evenDistributionFunction, eps);
            Handler.WriteFileChiSquaredCriteria(est.ChiSquaredCriteriaP, estimationResult, testFileNameMMM);
            estimationResult = est.KolmogorovCriteria(MMM, evenDistributionFunction, eps);
            Handler.WriteFileKolmogorovCriteria(est.KolmogorovCriteriaP, est.Dn, estimationResult, testFileNameMMM);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
