using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SA.QS.RS_LSD.Lib
{
    public class SortingAlgorithms
    {
        public int[] QuickSortLRPointers(int[] QuickSort, int firstIndex, int lastIndex, ref int compCount, ref int moveCount)
        {
            if (firstIndex < lastIndex)
            {
                int leftIndex = firstIndex, rightIndex = lastIndex, bearingPoint = QuickSort[(leftIndex + rightIndex) / 2];
                do
                {
                    while (QuickSort[leftIndex] < bearingPoint) { leftIndex++; compCount++; }
                    compCount++;
                    while (QuickSort[rightIndex] > bearingPoint) { rightIndex--; compCount++; }
                    compCount++;
                    if (leftIndex <= rightIndex)
                    {
                        int buffer = QuickSort[leftIndex];
                        QuickSort[leftIndex] = QuickSort[rightIndex];
                        QuickSort[rightIndex] = buffer;
                        leftIndex++;
                        rightIndex--;
                    }
                    if (leftIndex < rightIndex) { moveCount++; }
                } while (leftIndex <= rightIndex);
                QuickSortLRPointers(QuickSort, firstIndex, rightIndex, ref compCount, ref moveCount);
                QuickSortLRPointers(QuickSort, leftIndex, lastIndex, ref compCount, ref moveCount);
            }
            return QuickSort; 
        }


        struct KVEntry
        {
            int key;
            int value;

            public int Key
            {
                get
                {
                    return key;
                }
                set
                {
                    if (key >= 0)
                        key = value;
                    else
                        throw new Exception("Invalid key value");
                }
            }
            public int Value
            {
                get
                {
                    return value;
                }
                set
                {
                    this.value = value;
                }
            }
        }
        static KVEntry[] CountingSort(KVEntry[] ArrayA)
        {
            int[] ArrayB = new int[MaxValue(ArrayA) + 1];
            KVEntry[] ArrayC = new KVEntry[ArrayA.Length];

            for (int i = 0; i < ArrayB.Length; i++)
                ArrayB[i] = 0;

            for (int i = 0; i < ArrayA.Length; i++)
                ArrayB[ArrayA[i].Value]++;

            for (int i = 1; i < ArrayB.Length; i++)
                ArrayB[i] += ArrayB[i - 1];

            for (int i = ArrayA.Length - 1; i >= 0; i--)
            {
                int value = ArrayA[i].Value;
                int index = ArrayB[value];
                ArrayB[value]--;
                ArrayC[index - 1] = new KVEntry();
                ArrayC[index - 1].Key = i;
                ArrayC[index - 1].Value = value;
            }
            return ArrayC;
        }

        static int MaxValue(KVEntry[] arr)
        {
            int Max = arr[0].Value;
            for (int i = 1; i < arr.Length; i++)
                if (arr[i].Value > Max)
                    Max = arr[i].Value;
            return Max;
        }

        public int[] RadixSortLSD(int[] array, int digit)
        {
            bool Empty = true;
            KVEntry[] digits = new KVEntry[array.Length];//array that holds the digits;
            int[] SortedArray = new int[array.Length];//Hold the sorted array
            for (int i = 0; i < array.Length; i++)
            {
                digits[i] = new KVEntry();
                digits[i].Key = i;
                digits[i].Value = (array[i] / digit) % 10;
                if (array[i] / digit != 0)
                    Empty = false;
            }

            if (Empty)
                return array;

            KVEntry[] SortedDigits = CountingSort(digits);
            for (int i = 0; i < SortedArray.Length; i++)
                SortedArray[i] = array[SortedDigits[i].Key];
            return RadixSortLSD(SortedArray, digit * 10);
        }
    }
}
