
#if x64
using ATMCD64CS;
#else
using System;
using System.Diagnostics;
using System.Linq;
using ATMCD32CS;
using lasercom.objects;


#endif

namespace lasercom.camera
{
    public class DummyAndorCamera : AndorCamera
    {
        new AndorSDK AndorSdk = null;

        private int _ReadMode;
        override public int ReadMode
        {
            get { return _ReadMode; }
            set
            {
                _ReadMode = value;
            }
        }

        private int _AcquisitionMode;
        override public int AcquisitionMode
        {
            get { return _AcquisitionMode; }
            set
            {
                _AcquisitionMode = value;
            }
        }

        private int _TriggerMode;
        override public int TriggerMode
        {
            get { return _TriggerMode; }
            set
            {
                _TriggerMode = value;
            }
        }

        private int _TriggerInvert;
        override public int TriggerInvert
        {
            get { return _TriggerInvert; }
            set
            {
                _TriggerInvert = value;
            }
        }

        private float _TriggerLevel;
        override public float TriggerLevel
        {
            get { return _TriggerLevel; }
            set
            {
                _TriggerLevel = value;
            }
        }

        private int _DDGTriggerMode;
        override public int DDGTriggerMode
        {
            get { return _DDGTriggerMode; }
            set
            {
                _DDGTriggerMode = value;
            }
        }

        private int _GateMode;
        override public int GateMode
        {
            get { return _GateMode; }
            set
            {
                _GateMode = value;
            }
        }

        override public bool HasIntensifier
        {
            get
            {
                return true;
            }
        }

        private int _MCPGating;
        override public int MCPGating
        {
            get { return _MCPGating; }
            set
            {
                _MCPGating = value;
            }
        }

        private int _MCPGain;
        override public int IntensifierGain
        {
            get { return _MCPGain; }
            set
            {
                _MCPGain = value;
            }
        }

        private int _NumberAccumulations;
        override public int NumberAccumulations
        {
            get { return _NumberAccumulations; }
            set
            {
                _NumberAccumulations = value;
            }
        }

        private ImageArea _Image;
        public override ImageArea Image
        {
            get { return _Image; }
            set
            {
                int hbin, vbin, hstart, hcount, vstart, vcount;

                if (value.hcount == -1)
                {
                    hcount = _Image.hcount;
                }
                else
                {
                    hcount = Math.Max(1, value.hcount); // At least 1.
                    hcount = Math.Min(Width, hcount); // At most Width.
                }

                if (value.vcount == -1)
                {
                    vcount = _Image.vcount;
                }
                else
                {
                    vcount = Math.Max(1, value.vcount); // At least 1.
                    vcount = Math.Min(Height, vcount); // At most Height.
                }

                if (value.hstart == -1)
                {
                    hstart = _Image.hstart;
                }
                else
                {
                    hstart = Math.Max(0, value.hstart); // At least 0.
                    hstart = Math.Min(hstart, Width - 1); // At most Width - 1.
                }

                if (value.vstart == -1)
                {
                    vstart = _Image.vstart;
                }
                else
                {
                    vstart = Math.Max(0, value.vstart); // At least 0.
                    vstart = Math.Min(vstart, Height - 1); // At most Height - 1.
                }

                if (value.hbin == -1)
                {
                    hbin = _Image.hbin;
                }
                else
                {
                    hbin = Math.Max(1, value.hbin); // At least 1.
                    hbin = Math.Min(hbin, hcount); // At most image width.
                }

                if (value.vbin == -1)
                {
                    vbin = _Image.vbin;
                }
                else
                {
                    vbin = Math.Max(1, value.vbin); // At least 1.
                    vbin = Math.Min(vbin, vcount); // At most image height.
                }

                _Image = new ImageArea(hbin, vbin,
                    hstart, hcount,
                    vstart, vcount);
            }
        }

        public DummyAndorCamera(LuiObjectParameters p) : this(p as CameraParameters) { }

        public DummyAndorCamera(CameraParameters p)
        {
            if (p == null) 
                throw new ArgumentException("Non-null CameraParameters instance required");

            _Width = 1024;
            _Height = 256;
            _Image = new ImageArea(1, 1, 0, Width, 0, Height);
            Image = p.Image;
            Calibration = Enumerable.Range(0, Width).Select(x => (double)x).ToArray();
            LoadCalibration(p.CalFile);
            MinIntensifierGain = 0;
            MaxIntensifierGain = 4095;
            IntensifierGain = p.InitialGain;
            ReadMode = p.ReadMode;

            p.Image = Image;
            p.ReadMode = ReadMode;
        }

        public override int[] FullResolutionImage()
        {
            return null;
        }

        public override int[] CountsFvb()
        {
            return null;
        }

        public override int[] Acquire()
        {
            int[] data = new int[Width];
            Acquire(data);
            return data;
        }

        public override uint Acquire(int[] DataBuffer)
        {
            System.Threading.Thread.Sleep(250);
            var frame = 1;
            string caller = (new StackFrame(frame, true)).GetFileName();
            if (caller.Contains("DummyAndorCamera"))
            {
                frame++;
            }
            caller = (new StackFrame(frame, true)).GetMethod().Name;
            while (caller == "DoAcq" || caller == "TryAcquire") caller = (new StackFrame(++frame, true)).GetMethod().Name;
            caller = (new StackFrame(frame, true)).GetFileName();
            int line = (new StackFrame(frame,true)).GetFileLineNumber();
            int[] data = null;
            if (caller.Contains("SpecControl"))
            {
                data = Spec(line);
            }
            else if (caller.Contains("TroaControl"))
            {
                data = Troa(line);
            }
            else if (caller.Contains("CalibrateControl"))
            {
                data = Calibrate(line);
            }
            else if (caller.Contains("ResidualsControl"))
            {
                data = Residuals(line);
            }
            else if (caller.Contains("LaserPowerControl"))
            {
                data = LaserPower(line);
            }

            if (data != null) data.CopyTo(DataBuffer, 0);
            return AndorSDK.DRV_SUCCESS;
        }

        private int[] Dark(int scale = 1000)
        {
            return ReadMode == ReadModeImage ?
                DarkImage(scale) : Data.Uniform(Image.Width, scale);
        }

        private int[] DarkImage(int scale)
        {
            int[] data = new int[AcqSize];
            for (int i = 0; i < Image.Height; i++)
            {
                int[] row = Data.Uniform(Image.Width, scale);
                for (int j = 0; j < Image.Width; j++)
                {
                    data[i * Image.Width + j] = row[j];
                }
            }
            return data;
        }

        private int[] Blank(int scale = 55000)
        {
            return ReadMode == ReadModeFVB ? BlankFvb(scale) : BlankImage(scale);
        }

        private int[] BlankFvb(int scale)
        {
            int[] data = Data.Uniform(Width, 1000);
            for (int i = 1; i < 10; i++)
            {
                Data.Accumulate(data, Data.Gaussian(Width, scale, Width * i / 10D, Width / 10D));
            }
            return data;
        }

        private int[] BlankImage(int scale)
        {
            int[] data = new int[AcqSize];
            for (int i = 0; i < Image.Height; i++)
            {
                int ymax = Image.Height / 2;
                int yscale = (int)(scale * (1 - Math.Abs((double)i - ymax) / ymax));
                int[] row = BlankFvb(yscale);
                for (int j = 0; j < Image.Width; j++)
                {
                    data[i * Image.Width + j] = row[j];
                }
            }
            return data;
        }

        private int[] SampleData(double centerNormalized = 0.5, int scale = 32000)
        {
            return ReadMode == ReadModeImage ?
                GenerateDataImage(centerNormalized, scale) :
                GenerateData(centerNormalized, scale);
        }

        private int[] GenerateData(double centerNormalized, int scale)
        {
            int[] data = Data.Gaussian(Width, scale, Width * centerNormalized, Width / 10D);
            return data;
        }

        private int[] GenerateDataImage(double centerNormalized, int scale)
        {
            int[] data = new int[AcqSize];
            for (int i = 0; i < Image.Height; i++)
            {
                int[] row = Data.Gaussian(Image.Width, scale, Image.Width * centerNormalized, Width / 10);
                for (int j = 0; j < Image.Width; j++)
                {
                    data[i * Image.Width + j] = row[j];
                }
            }
            return data;
        }

        int[] Spec(int line)
        {
            int[] data = null;
            if (line == 161) // Dark.
            {
                data = Dark(1000);
            }
            else if (line == 177) // Blank.
            {
                data = Blank(55000);
            }
            else if (line == 205) // Excited.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.5, 32000));
                for (int i = 0; i < data.Length; i++) data[i] += 1000;
            }
            return data;
        }

        int[] Troa(int line)
        {
            int[] data = null;
            if (line == 600) // Dark.
            {
                data = Dark(1000);
            }
            else if (line == 609 || line == 626 || line == 631) // Ground.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.3333, 32000));
                for (int i = 0; i < data.Length; i++) data[i] += 1000;
            }
            else // Excited.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.6667, 32000));
                for (int i = 0; i < data.Length; i++) data[i] += 1000;
            }
            return data;
        }

        int[] Calibrate(int line)
        {
            int[] data = null;
            if (line == 207) // Dark.
            {
                data = Dark(1000);
            }
            else if (line == 223) // Blank.
            {
                data = Blank(55000);
            }
            else // Sample.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.5, 32000));
            }
            return data;
        }

        int[] Residuals(int line)
        {
            return Blank(55000);
        }

        int[] LaserPower(int line)
        {
            int[] data = null;
            if (line == 132) // Dark.
            {
                data = Dark(1000);
            }
            else if (line == 154) // Ground.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.3333, 32000));
                for (int i = 0; i < data.Length; i++) data[i] += 1000;
            }
            else // Excited.
            {
                data = Blank(55000);
                Data.Dissipate(data, SampleData(0.6667, 32000));
                for (int i = 0; i < data.Length; i++) data[i] += 1000;
            }
            return data;
        }

        public override void Close()
        {
        }
    }
}
