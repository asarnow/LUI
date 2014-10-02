using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI
{
    public static class Data
    {

        public static void NormalizeArray(int[] arr, int maxval)
        {
            int max = AbsMax(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                int denom = max * maxval;
                arr[i] = denom == 0 ? 0 : arr[i] / denom;
            }
        }

        public static int[] DummySpectrum(double t)
        {
            int[] data = new int[1024];
            return data;
        }

        public static int AbsMax(int[] arr)
        {
            int curmax = int.MinValue;
            foreach (int i in arr)
                curmax = Math.Abs(arr[i]) > curmax ? Math.Abs(arr[i]) : curmax;
            return curmax;
        }

    }
}
