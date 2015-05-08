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
        MatFile File;
        
        [TestMethod]
        public void TestMatFile()
        {
            File = new MatFile("C:\\Users\\da\\Documents\\Visual Studio 2012\\Projects\\LUI\\Lui Unit Tests\\test.mat");

            MatVar<double> dblmat = File.CreateVariable<double>("dblmat", 6, 10);
            double[] data10 = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
            double[] data6 = { 20, 21, 22, 23, 24, 25 };

            dblmat.WriteNext(data6, 1); // First column.

            dblmat.Cursor[0] = 2;

            dblmat.WriteNext(data10, 0); // Third row.

            double[] data2 = { 2, 4, 6, 8 };
            dblmat.Write(data2, new long[]{4, 8}, new long[]{2, 2}); // Bottom right corner.

            MatVar<int> intarr3d = File.CreateVariable<int>("intarr3d", 5, 5, 5);

            int[] intdata = Data.Uniform(25, 20);

            intarr3d.WriteNext(intdata, 2); // First 2D slice.

            double[] readbuf = new double[data10.Length];
            try
            {
                dblmat.Read(readbuf, new long[] { 2, 0 }, new long[] { 1, readbuf.Length });
                for (int i = 0; i < readbuf.Length; i++)
                    Assert.AreEqual(data10[i], readbuf[i]);
            }
            catch (AssertFailedException ex)
            {
                Assert.Fail("Read incorrect data from matrix at (" + 2 + "," + i + ")");
            }
            catch (Exception ex)
            {
                Assert.Fail("Trouble reading HDF5 file");
            }

        }

        [TestCleanup]
        public void Teardown()
        {
            File.Dispose();
        }
        
    }
}
