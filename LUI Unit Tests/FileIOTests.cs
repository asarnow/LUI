using System.IO;
using System.Linq;
using lasercom.io;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LUI_Unit_Tests
{
    [TestClass]
    public class FileIOTests
    {
        [TestMethod]
        public void TestVectorIO()
        {
            string FileName = Path.GetTempFileName();

            double[] data = Enumerable.Range(0, 1024).Select(x => (double)x).ToArray();

            FileIO.WriteVector(FileName, data);

            double[] Read = FileIO.ReadVector<double>(FileName);

            for (int i = 0; i < Read.Length; i++)
            {
                Assert.AreEqual(data[i], Read[i]);
            }

        }
    }
}
