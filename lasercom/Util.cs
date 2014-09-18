using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace LUI
{
    public class Util
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Might throw IOException
        public static List<IJob> ReadTimesFile(String filename)
        {
            List<IJob> times = new List<IJob>();
            foreach (string line in ReadLines(filename))
            {
                line.Trim();
            }
            return times;
        }

        // <summary>
        // Throws IOException.
        // </summary>
        public static IEnumerable<string> ReadLines(string filename)
        {
            using (TextReader tr = new StreamReader(filename))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static int[] DummySpectrum(double t)
        {
            int[] data = new int[1024];




            return data;
        }

        public static void normalizeArray(int[] arr, int maxval)
        {
            int max = arr.Max();
            for (int i = 0; i < arr.Length; i++ )
            {
                arr[i] = arr[i] / max * maxval;
            }
        }
    }
}
