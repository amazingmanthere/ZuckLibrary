using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary
{
    /// <summary>
    /// special the sort mode,like ascending and descending
    /// </summary>
    public enum ZKSortMode
    {
        Asc,
        Desc
    };

    public static class ZKSort
    {
        #region Bubble Sort
        public static void Sort_Bubble(int[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            bool flag = false;
            int temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] > sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] < sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
        }

        public static void Sort_Bubble(float[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            bool flag = false;
            float temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] > sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] < sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
        }

        public static void Sort_Bubble(double[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            bool flag = false;
            double temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] > sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    flag = false;
                    for (j = 0; j < length - 1 - i; j++)
                    {
                        if (sortData[j] < sortData[j + 1])
                        {
                            temp = sortData[j];
                            sortData[j] = sortData[j + 1];
                            sortData[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (flag == false) break;
                }
            }
        }
        #endregion

        #region Quicksort Sort
        public static void Sort_Quicksort(int[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            Sort_Quicksort2(sortData, 0, sortData.Length - 1, ZKSortMode);
        }

        public static void Sort_Quicksort(float[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            Sort_Quicksort2(sortData, 0, sortData.Length - 1, ZKSortMode);
        }

        public static void Sort_Quicksort(double[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            Sort_Quicksort2(sortData, 0, sortData.Length - 1, ZKSortMode);
        }

        private static void Sort_Quicksort2(int[] sortData, int begin, int end, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            if (begin >= end) return;

            int i = begin;
            int j = end;
            int key = sortData[begin];
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                while (i < j)
                {
                    while (i < j && sortData[j] >= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] <= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            else
            {
                while (i < j)
                {
                    while (i < j && sortData[j] <= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] >= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            sortData[j] = key;
            Sort_Quicksort2(sortData, begin, i - 1, ZKSortMode);
            Sort_Quicksort2(sortData, i + 1, end, ZKSortMode);
        }

        private static void Sort_Quicksort2(float[] sortData, int begin, int end, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            if (begin >= end) return;

            int i = begin;
            int j = end;
            float key = sortData[begin];
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                while (i < j)
                {
                    while (i < j && sortData[j] >= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] <= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            else
            {
                while (i < j)
                {
                    while (i < j && sortData[j] <= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] >= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            sortData[j] = key;
            Sort_Quicksort2(sortData, begin, i - 1, ZKSortMode);
            Sort_Quicksort2(sortData, i + 1, end, ZKSortMode);
        }

        private static void Sort_Quicksort2(double[] sortData, int begin, int end, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            if (begin >= end) return;

            int i = begin;
            int j = end;
            double key = sortData[begin];
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                while (i < j)
                {
                    while (i < j && sortData[j] >= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] <= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            else
            {
                while (i < j)
                {
                    while (i < j && sortData[j] <= key) j--;
                    sortData[i] = sortData[j];
                    while (i < j && sortData[i] >= key) i++;
                    sortData[j] = sortData[i];
                }
            }
            sortData[j] = key;
            Sort_Quicksort2(sortData, begin, i - 1, ZKSortMode);
            Sort_Quicksort2(sortData, i + 1, end, ZKSortMode);
        }
        #endregion

        #region Insertion Sort
        public static void Sort_Insertion(int[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            int temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp < sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
            else
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp > sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
        }

        public static void Sort_Insertion(float[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            float temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp < sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
            else
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp > sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
        }

        public static void Sort_Insertion(double[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            double temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp < sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
            else
            {
                for (i = 1; i < length; i++)
                {
                    temp = sortData[i];
                    j = i - 1;
                    while (j >= 0 && temp > sortData[j])
                    {
                        sortData[j + 1] = sortData[j];
                        j--;
                    }
                    sortData[j + 1] = temp;
                }
            }
        }
        #endregion

        #region Selection Sort
        public static void Sort_Selection(int[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            int temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] < sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] > sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
        }

        public static void Sort_Selection(float[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            float temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] < sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] > sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
        }

        public static void Sort_Selection(double[] sortData, ZKSortMode ZKSortMode = ZKSortMode.Asc)
        {
            int length = sortData.Length;
            if (length == 0)
                return;
            int i, j;
            double temp;
            if  (ZKSortMode == ZKSortMode.Asc)
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] < sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    for (j = i + 1; j < length; j++)
                    {
                        if (sortData[j] > sortData[i])
                        {
                            temp = sortData[i];
                            sortData[i] = sortData[j];
                            sortData[j] = temp;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
