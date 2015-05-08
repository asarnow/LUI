using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CsvHelper;

namespace lasercom.io
{
    public static class FileIO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <throws>IOException, FormatException</throws>
        /// <returns></returns>
        public static IList<double> ReadTimesFile(string filename)
        {
            IList<double> times = new List<double>();
            foreach (string line in ReadLines(filename))
            {
                line.Trim();
                times.Add(double.Parse(line));
            }
            return times;
        }

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

        public static T[] ReadVector<T>(string FileName) 
        {
            TextReader reader = File.OpenText(FileName);
            CsvReader csv = new CsvReader(reader);
            return csv.GetRecords<T>().ToArray<T>();
        }

        public static void WriteVector<T>(string FileName, IEnumerable<T> Vector)
        {
            TextWriter writer = new StreamWriter(FileName);
            CsvWriter csv = new CsvWriter(writer);
            csv.WriteRecords(Vector);
            writer.Close();
        }

        public static void WriteMatrix<T>(string FileName, T[,] Matrix)
        {
            TextWriter writer = new StreamWriter(FileName);
            CsvWriter csv = new CsvWriter(writer);
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    csv.WriteField(Matrix[i, j].ToString());
                }
                csv.NextRecord();
            }
            writer.Close();
        }
    }
}
