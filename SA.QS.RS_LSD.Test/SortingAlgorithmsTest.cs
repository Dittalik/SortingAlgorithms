using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SA.QS.RS_LSD.Lib;

namespace SA.QS.RS_LSD.Test
{
    [TestClass]
    public class SortingAlgorithmsTest
    {
        readonly SortingAlgorithms sorting = new SortingAlgorithms();

        [TestMethod]
        public void QuickSortTest()
        {
            
            int[] TestArray = new int[] { 6, 8, 2, 3, 4, 1, 5, 7, 9, 10 };
            int[,] sortedAr = sorting.QuickSortLRPointers(TestArray, 0, TestArray.Length - 1);
            int[] sortedArray = new int[10];
            for (int i = 0; i < 10; i++)
            {
                sortedArray[i] = sortedAr[0, i];
            }
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, sortedArray);
        }

        [TestMethod]
        public void RadixSortTest()
        {
            int[] TestArray = new int[] { 321, 312, 434, 123, 433, 1, 2, 4, 3, 5, 6 };
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 123, 312, 321, 433, 434 }, sorting.RadixSortLSD(TestArray, 1));
        }
    }
}
