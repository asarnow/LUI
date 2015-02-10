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

        public static T[] ReadVector<T>(String FileName) 
        {
            TextReader reader = File.OpenText(FileName);
            CsvReader csv = new CsvReader(reader);
            return csv.GetRecords<T>().ToArray<T>();
        }

        public static void WriteVector<T>(String FileName, IEnumerable<T> Vector)
        {
            TextWriter writer = new StreamWriter(FileName);
            CsvWriter csv = new CsvWriter(writer);
            csv.WriteRecords(Vector);
            writer.Close();
        }
    }
}
