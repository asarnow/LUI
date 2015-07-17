using System;
using System.Runtime.Serialization;
using lasercom.objects;

namespace lasercom.camera
{
    [DataContract]
    public class CameraParameters : LuiObjectParameters<CameraParameters>
    {
        private string _CalFile;
        [DataMember]
        public string CalFile
        {
            get
            {
                return _CalFile;
            }
            set
            {
                _CalFile = value;
            }
        }

        [DataMember]
        public string Dir { get; set; }

        [DataMember]
        public int Temperature { get; set; }

        [DataMember]
        public int InitialGain { get; set; }

        [DataMember]
        public int SaturationLevel { get; set; }

        public int HBin = -1;
        public int VBin = -1;
        public int HStart = -1;
        public int HCount = -1;
        public int VStart = -1;
        public int VCount = -1;

        [DataMember]
        //private ImageArea _Image = new ImageArea(-1, -1, -1, -1, -1, -1);
        public ImageArea Image
        {
            get
            {
                return new ImageArea(HBin, VBin, HStart, HCount, VStart, VCount);
            }
            set
            {
                //_Image = value;
                HBin = value.hbin;
                VBin = value.vbin;
                HStart = value.hstart;
                HCount = value.hcount;
                VStart = value.vstart;
                VCount = value.vcount;
            }
        }

        [DataMember]
        public int ReadMode { get; set; }

        public CameraParameters(Type Type)
            : base(Type)
        {

        }

        public CameraParameters()
            : base()
        {
            
        }

        public CameraParameters(CameraParameters other) 
            : base(other)
        {

        }

        public override void Copy(CameraParameters other)
        {
            base.Copy(other);
            //this.Type = other.Type;
            //this.Name = other.Name;
            this.CalFile = other.CalFile;
            this.Dir = other.Dir;
            this.Temperature = other.Temperature;
            this.InitialGain = other.InitialGain;
            this.Image = other.Image;
            this.ReadMode = other.ReadMode;
            this.SaturationLevel = other.SaturationLevel;
        }

        public override bool NeedsReinstantiation(CameraParameters other)
        {
            bool needs = base.NeedsReinstantiation(other); // Type is different.
            if (needs) return true;

            if (Type == typeof(AndorCamera) || Type.IsSubclassOf(typeof(AndorCamera)))
            {
                needs |= other.Dir != Dir; // Or if Dir is different.
            }

            return needs;
        }

        public override bool NeedsUpdate(CameraParameters other)
        {
            bool iseq = this.CalFile == other.CalFile;

            if (Type == typeof(AndorCamera) || Type.IsSubclassOf(typeof(AndorCamera)))
            {
                iseq &= other.InitialGain == InitialGain;
                iseq &= other.ReadMode == ReadMode;
                iseq &= other.HBin == HBin;
                iseq &= other.VBin == VBin;
                iseq &= other.HStart == HStart;
                iseq &= other.HCount == HCount;
                iseq &= other.VStart == VStart;
                iseq &= other.VCount == VCount;
                iseq &= other.SaturationLevel == SaturationLevel;
            }
            if (Type == typeof(CameraTempControlled))
            {
                iseq &= Temperature == other.Temperature;
            }
            return !iseq; // True if any of these field differ.
        }

    }
}
