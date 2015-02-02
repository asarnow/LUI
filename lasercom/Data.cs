using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom
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

        /// <summary>
        /// Computes OD from Sample and Blank counts, subtracting Dark counts from both.
        /// </summary>
        /// <param name="Sample"></param>
        /// <param name="Blank"></param>
        /// <param name="Dark"></param>
        /// <returns>Optical density</returns>
        public static double[] OpticalDensity(int[] Sample, int[] Blank, int[] Dark)
        {
            double[] OD = new double[Sample.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Sample[i] - Dark[i]) / (double)(Blank[i] - Dark[i]));
            return OD;
        }

        /// <summary>
        /// Computes OD from Sample and Blank counts. Assumes Dark has been applied or is zero.
        /// </summary>
        /// <param name="Sample"></param>
        /// <param name="Blank"></param>
        /// <returns>Optical density</returns>
        public static double[] OpticalDensity(int[] Sample, int[] Blank)
        {
            double[] OD = new double[Sample.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Sample[i]) / (double)(Blank[i]));
            return OD;
        }

        /// <summary>
        /// Computes delta OD from Ground, Trans and Dark counts.
        /// </summary>
        /// <param name="Ground"></param>
        /// <param name="Trans"></param>
        /// <param name="Dark"></param>
        /// <returns>Delta OD</returns>
        public static double[] DeltaOD(int[] Ground, int[] Trans, int[] Dark)
        {
            double[] OD = new double[Ground.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Ground[i] - Dark[i]) / (double)(Trans[i] - Dark[i]));
            return OD;
        }

        /// <summary>
        /// Computes delta OD from Ground and Trans. Assumes Dark has been applied or is zero.
        /// </summary>
        /// <param name="Ground"></param>
        /// <param name="Trans"></param>
        /// <returns>Delta OD</returns>
        public static double[] DeltaOD(int[] Ground, int[] Trans)
        {
            double[] OD = new double[Ground.Length];
            for (int i = 0; i < OD.Length; i++)
                OD[i] = Math.Log10((double)(Ground[i]) / (double)(Trans[i]));
            return OD;
        }

        public static double[] Gaussian(int n, double scale, double mean, double sigma)
        {
            double[] g = new double[n];
            for (int i = 0; i < g.Length; i++)
            {
                double x = (double)i * 1024 / n;
                g[i] = scale * Math.Exp(-Math.Pow(x - mean, 2) / (2 * Math.Pow(sigma, 2)));
            }
            return g;
        }

        public static int[] Guassian(int n, double scale, double mean, double sigma)
        {
            int[] g = new int[n];
            for (int i = 0; i < g.Length; i++)
            {
                double x = (double)i * 1024 / n;
                g[i] = (int)( scale * Math.Exp(-Math.Pow(x - mean, 2) / (2 * Math.Pow(sigma, 2))) );
            }
            return g;
        }

        public static double[] Calibrate(double[] channel, double slope, double intercept)
        {
            double[] cal = new double[channel.Length];
            for (int i = 0; i < channel.Length; i++)
            {
                cal[i] = slope * channel[i] + intercept;
            }
            return cal;
        }

        public static double[] Calibrate(int n, double slope, double intercept)
        {
            double[] cal = new double[n];
            for (int i = 0; i < n; i++)
            {
                cal[i] = slope * (i+1) + intercept;
            }
            return cal;
        }

        public static Tuple<double,double,double> LinearLeastSquares(double[] x, double[] y)
        {
            double n = x.Length;
            double xysum = 0;
            double xsum = 0;
            double xsqsum = 0;
            double ysum = 0;
            double ysqsum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                xysum += x[i] * y[i];
                xsum += x[i];
                xsqsum += Math.Pow(x[i], 2);
                ysum += y[i];
                ysqsum += Math.Pow(y[i], 2);
            }
            double xyhat = xysum / n;
            double xhat = xsum / n;
            double yhat = ysum / n;
            double xsqhat = xsqsum / n;
            double ysqhat = ysqsum / n;

            double cov = xyhat - xhat * yhat;
            double xvar = xsqhat - Math.Pow(xhat, 2);
            double yvar = ysqhat - Math.Pow(yhat, 2);

            double slope = cov / xvar;
            double yint = yhat - slope * xhat;
            double rsq = Math.Pow( cov / Math.Sqrt(xvar * yvar), 2);

            return Tuple.Create<double,double,double>(slope, yint, rsq);
        }
    }
}
