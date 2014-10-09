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

        public static void Accumulate(int[] a, int[] b)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] += b[i];
        }

        public static void Dissipate(int[] a, int[] b)
        {
            for (int i = 0; i < a.Length; i++) 
                a[i] -= b[i];
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


        public static void DivideArray(int[] arr, int N)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] /= N;
            }
        }

        public static double[] OpticalDensity(int[] Data, int[] Blank, int[] Dark)
        {
            double[] OD = new double[Data.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Data[i] - Dark[i]) / (double)(Blank[i] - Dark[i]));
            return OD;
        }

        public static double[] DeltaOD(int[] Ground, int[] Trans, int[] Dark)
        {
            double[] OD = new double[Ground.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Ground[i] - Dark[i]) / (double)(Trans[i] - Dark[i]));
            return OD;
        }
    }
}
