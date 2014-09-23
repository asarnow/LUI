using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LUI
{
    public class TROAJob:IJob
    {
        private readonly double Delay;
        private readonly int NumAvg;

        public TROAJob(String jobSpec)
        {

        }

        public TROAJob(double delay, int numavg)
        {
            Delay = delay;
            NumAvg = numavg;
        }

        public double GetDelay()
        {
            return Delay;
        }

        public int GetNumAvg()
        {
            return NumAvg;
        }
    }
}
