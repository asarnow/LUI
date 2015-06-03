using lasercom.objects;
using System;
using System.Runtime.Serialization;

namespace lasercom.camera
{
    [DataContract]
    public class CameraParameters : LuiObjectParameters<CameraParameters>
    {
        [DataMember]
        public string CalFile { get; set; }

        [DataMember]
        public string Dir { get; set; }

        [DataMember]
        public int Temperature { get; set; }

        [DataMember]
        public int InitialGain { get; set; }

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(AndorCamera))
                {
                    arr = new object[] { CalFile, Dir, InitialGain };
                }
                else if (Type == typeof(CameraTempControlled))
                {
                    arr = new object[] { CalFile, Dir, InitialGain, Temperature };
                }
                else if (Type == typeof(DummyAndorCamera))
                {
                    arr = new object[] { CalFile, InitialGain };
                }
                else if (Type == typeof(DummyCamera))
                {
                    arr = new object[0];
                }
                return arr;
            }
        }

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
            this.Type = other.Type;
            this.Name = other.Name;
            this.CalFile = other.CalFile;
            this.Dir = other.Dir;
            this.Temperature = other.Temperature;
            this.InitialGain = other.InitialGain;
        }

        public override bool Equals(CameraParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            iseq &= this.CalFile == other.CalFile;

            if (Type == typeof(AndorCamera))
            {
                iseq &= Dir == other.Dir;
                iseq &= InitialGain == other.InitialGain;
            }
            else if (Type == typeof(CameraTempControlled))
            {
                iseq &= Dir == other.Dir;
                iseq &= InitialGain == other.InitialGain;
                iseq &= Temperature == other.Temperature;
            }
            return iseq;
        }

        public override bool Equals(object other)
        {
            return Equals(other as CameraParameters);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
            //unchecked
            //{
            //    int hash = Util.Hash(Type, Name);
            //    hash = Util.Hash(hash, CalFile);
            //    if (Type == typeof(AndorCamera))
            //    {
            //        hash = Util.Hash(hash, Dir);
            //    }
            //    else if (Type == typeof(CameraTempControlled))
            //    {
            //        hash = Util.Hash(hash, Dir);
            //        hash = Util.Hash(hash, Temperature);
            //    }
            //    return hash;
            //}
        }

    }
}
