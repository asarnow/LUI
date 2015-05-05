using System;
using System.Reflection;
using lasercom;
using lasercom.io;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LUI_Unit_Tests
{
    [TestClass]
    public class MatFileTests
    {
        [TestMethod]
        public void TestMatFile()
        {
            MatFile File = new MatFile("test.mat");

            MatVar<double> dblmat = File.CreateVariable<double>("dblmat", 10, 10);
            double[] data = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };

            dblmat.WriteNext(data, 1); // First column.

            dblmat.Cursor[0] = 3;

            dblmat.WriteNext(data, 0); // Third row.

            double[] data2 = { 2, 4, 6, 8 };
            dblmat.Write(data2, new long[]{8, 8}, new long[]{2, 2}); // Bottom right corner.

            MatVar<int> intarr3d = File.CreateVariable<int>("intarr3d", 5, 5, 5);

            int[] intdata = Data.Uniform(25, 20);

            intarr3d.WriteNext(intdata, 2); // First 2D slice.

            File.Dispose();
        }

    }
}
