using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI
{
    public class DataPoint
    {
        private double Delay;
        private int NumAvg;
        private int[] Data;

        public double GetDelay()
        {
            return Delay;
        }

        public int GetNumAvg()
        {
            return NumAvg;
        }

        public int[] GetData()
        {
            return Data;
        }

        public DataPoint(double delay, int size)
        {
            NumAvg = 0;
            Delay = delay;
            Data = new int[size];
        }

        public void Store(int[] data)
        {
            NumAvg++;
            for (int i = 0; i < data.Length; i++) Data[i] += data[i];
        }
    }
}
