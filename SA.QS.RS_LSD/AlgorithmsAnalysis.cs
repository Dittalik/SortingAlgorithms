using System;
using System.Linq;
using System.IO;
using IronXL;
using Humanizer;
using SA.QS.RS_LSD.Lib;

namespace SA.QS.RS_LSD
{
    class AlgorithmsAnalysis
    {
        public static Random rng = new Random();
        private static readonly string worstCase = @"C:\CshData\SortingAlgorithms\InputData\worstCase.txt";
        private static readonly string averageCase = @"C:\CshData\SortingAlgorithms\InputData\avrgCase.txt";
        private static readonly string bestCase = @"C:\CshData\SortingAlgorithms\InputData\bestCase.txt";
        private static readonly string QSoutpCount = @"C:\CshData\SortingAlgorithms\OutputData\QuickSortOutputFile.xlsx";
        private static readonly string QSoutpFile = @"C:\CshData\SortingAlgorithms\OutputData\QuickSortOutputFile.txt";
        private static int[] worstArray = Array.ConvertAll(File.ReadAllText(worstCase).Split(','), x => int.Parse(x.Trim()));
        private static int[] averageArray = Array.ConvertAll(File.ReadAllText(averageCase).Split(','), x => int.Parse(x.Trim()));
        private static int[] bestArray = Array.ConvertAll(File.ReadAllText(bestCase).Split(','), x => int.Parse(x.Trim()));

        static void Main(string[] args)
        {
            IronXL.License.LicenseKey = "IRONSUITE.VITALYFFS.GMAIL.COM.27355-AD8DB4CE43-ABXOI2B-XDSDBZPVC6BB-GKFODQ4SGLZY-NJB25DIHIUKA-RQFCTHXQVOMY-RP2674UP627S-5FTC2O4XSIVZ-KFVCQF-TSGEMCSC2WGLEA-DEPLOYMENT.TRIAL-ZKGK6A.TRIAL.EXPIRES.23.DEC.2023";
            Console.WriteLine("QuickSort Analysis");
            SortingAlgorithms sorting = new SortingAlgorithms();

            //int[] avrgArray = Enumerable.Repeat<int>(1, 100000).Select((x, i) => new { i = i, rand = rng.Next() }).OrderBy(x => x.rand).Select(x => x.i).ToArray();

            var watch = new System.Diagnostics.Stopwatch();
            double elapsedMs;

            WorkBook book = WorkBook.Load(QSoutpCount);

            WorkSheet worstSheet = book.WorkSheets[0];
            worstSheet["A1"].Value = "Array Size";
            worstSheet["B1"].Value = "Execution Time (s)";
            worstSheet["C1"].Value = "Number of Comparisons";
            worstSheet["D1"].Value = "Number of Moving Operations";

            for (int i = 9999, j = 2, x = 10000; i < 100000; i += 1000, j++, x += 1000)
            {
                int compCount = 0, moveCount = 0;
                watch.Start();
                sorting.QuickSortLRPointers(worstArray, 0, i, ref compCount, ref moveCount);
                watch.Stop();
                elapsedMs = watch.Elapsed.TotalSeconds;
                watch.Reset();

                worstSheet[$"A{j}"].Value = x; //Array Size writting
                worstSheet[$"B{j}"].Value = elapsedMs;
                worstSheet[$"C{j}"].Value = compCount;
                worstSheet[$"D{j}"].Value = moveCount;
                Console.WriteLine($"Counting Worst Array i = {i}");
                Console.WriteLine(elapsedMs);
                book.Save();
            }

            WorkSheet averageSheet = book.WorkSheets[1];
            averageSheet["A1"].Value = "Array Size";
            averageSheet["B1"].Value = "Execution Time (s)";
            averageSheet["C1"].Value = "Number of Comparisons";
            averageSheet["D1"].Value = "Number of Moving Operations";

            for (int i = 9999, j = 2, x = 10000; i < 100000; i += 1000, j++, x += 1000)
            {
                int compCount = 0, moveCount = 0;
                watch.Start();
                sorting.QuickSortLRPointers(averageArray, 0, i, ref compCount, ref moveCount);
                watch.Stop();
                elapsedMs = watch.Elapsed.TotalSeconds;
                watch.Reset();

                Console.WriteLine(compCount + moveCount);
                averageSheet[$"A{j}"].Value = x; //Array Size writting
                averageSheet[$"B{j}"].Value = elapsedMs;
                averageSheet[$"C{j}"].Value = compCount;
                averageSheet[$"D{j}"].Value = moveCount;
                Console.WriteLine($"Counting Worst Array i = {i}");
                book.Save();
            }
            WorkSheet bestSheet = book.WorkSheets[2];
            bestSheet["A1"].Value = "Array Size";
            bestSheet["B1"].Value = "Execution Time (s)";
            bestSheet["C1"].Value = "Number of Comparisons";
            bestSheet["D1"].Value = "Number of Moving Operations";

            for (int i = 9999, j = 2, x = 10000; i <= 100000; i += 1000, j++, x += 1000)
            {
                int compCount = 0, moveCount = 0;
                watch.Start();
                sorting.QuickSortLRPointers(bestArray, 0, i, ref compCount, ref moveCount);
                watch.Stop();
                elapsedMs = watch.Elapsed.TotalSeconds; ;
                watch.Reset();

                bestSheet[$"A{j}"].Value = x; //Array Size writting
                bestSheet[$"B{j}"].Value = elapsedMs;
                bestSheet[$"C{j}"].Value = compCount;
                bestSheet[$"D{j}"].Value = moveCount;
                Console.WriteLine($"Counting Best Array i = {i}");
                book.Save();
            }
            book.Save();
        }
    }
}
