using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using lasercom;
using lasercom.objects;

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

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(AndorCamera))
                {
                    arr = new object[] { CalFile, Dir };
                }
                else if (Type == typeof(CameraTempControlled))
                {
                    arr = new object[] { CalFile, Dir, Temperature };
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

        public override void Copy(CameraParameters other)
        {
            base.Copy(other);
            this.Type = other.Type;
            this.Name = other.Name;
            this.CalFile = other.CalFile;
            this.Dir = other.Dir;
            this.Temperature = other.Temperature;
        }

        public override bool Equals(CameraParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            iseq &= this.CalFile == other.CalFile;

            if (Type == typeof(AndorCamera))
            {
                iseq &= Dir == other.Dir;
            }
            else  if (Type == typeof(CameraTempControlled))
            {
                iseq &= Dir == other.Dir;
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
            unchecked
            {
                int hash = Util.Hash(Type, Name);
                hash = Util.Hash(hash, CalFile);
                if (Type == typeof(AndorCamera))
                {
                    hash = Util.Hash(hash, Dir);
                }
                else if (Type == typeof(CameraTempControlled))
                {
                    hash = Util.Hash(hash, Dir);
                    hash = Util.Hash(hash, Temperature);
                }
                return hash;
            }
        }

    }
}
