﻿using System;
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

    }
}