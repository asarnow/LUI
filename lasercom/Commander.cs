using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

using log4net;
using NationalInstruments.NI4882;

namespace LUI
{

    public class Commander
    {
        public CameraTempControlled Camera { get; set; }
        public AbstractBeamFlags BeamFlags { get; set; }
        public IDigitalDelayGenerator DDG { get; set; }
        public AbstractPump Pump { get; set; }
        public List<Double> Delays { get; set; }
        public Dictionary<Double, DataPoint> Data { get; set; }
        public int[] Calibration { get; set; }
        public Stack<IJob> JobStack = new Stack<IJob>();

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Commander()
        {
            Camera = new CameraTempControlled(".");
            //BeamFlags = new BeamFlags("COM1");
            BeamFlags = new DummyBeamFlags();
            int address = 0;
            //DDG = new DDG535(address);
            DDG = new DummyDigitalDelayGenerator();
            //Pump = new HarvardPump("COM3");
            Data = new Dictionary<double, DataPoint>();
        }

        public void SetDelays(string file)
        {
            // read file
            Delays = new List<double>();
        }

        public int[] Collect(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Camera.CountsFvb();
            }
            return null;
        }

        public int[] Dark()
        {
            BeamFlags.CloseLaserAndFlash();
            return Camera.Acquire();
        }

        public int[] Flash()
        {
            BeamFlags.CloseLaserAndFlash();
            BeamFlags.OpenFlash();
            int[] data = Camera.Acquire();
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

        public int[] Trans()
        {
            BeamFlags.OpenLaserAndFlash();
            int[] data = Camera.Acquire();
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

        public void RunJobs()
        {
            Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Camera.ReadMode = Constants.ReadModeFVB;

            while (JobStack.Count > 0)
            {
                IJob job = JobStack.Pop();
                if (job == null)
                {
                    Log.Warn("Manual abort triggered");
                    break; // Abort
                }

                if (job is DarkJob)
                {
                    RunDarkJob(job as DarkJob);
                }

                if (job is OAJob)
                {
                    RunOAJob(job as OAJob);
                }

                if (job is TROAJob)
                {
                    RunTROAJob(job as TROAJob);
                }
                
            }
        }

        private void RunDarkJob(DarkJob job)
        {
            Log.Info("Dark job");

            StoreData( Constants.DarkState, Dark()) ;
        }

        private void RunOAJob(OAJob job)
        {
            Log.Info("OA Job");

            StoreData( Constants.GroundState, Flash() );

        }

        private void RunTROAJob(TROAJob job)
        {
            Log.Info("TROA Job: " + job.GetDelay().ToString() + " s");

            DDG.SetBDelay( job.GetDelay() );

            StoreData( job.GetDelay(), Trans() );
        }


        private void StoreData(Double delay, int[] data)
        {
            DataPoint value;
            if (Data.TryGetValue(delay, out value))
            {
                value.Store(data);
            } 
            else
            {
                Data[delay] = new DataPoint(delay, (int)Camera.Width);
                Data[delay].Store(data);
            }

        }

        public void Abort()
        {
            JobStack.Push(null);
        }

    }
}
