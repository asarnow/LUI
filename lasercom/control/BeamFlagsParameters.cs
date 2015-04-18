using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using lasercom.objects;

namespace lasercom.control
{
    [DataContract]
    public class BeamFlagsParameters : LuiObjectParameters<BeamFlagsParameters>
    {        
        [DataMember]
        public string PortName { get; set; }

        public BeamFlagsParameters(Type Type, string PortName) : base(Type)
        {
            this.PortName = PortName;
        }

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(BeamFlags))
                {
                    arr = new object[] { PortName };
                }
                else if (Type == typeof(DummyBeamFlags))
                {
                    arr = new object[0];
                }
                return arr;
            }
        }

        public BeamFlagsParameters(Type Type)
            : base(Type)
        {

        }

        public BeamFlagsParameters()
            : base()
        {

        }

        public override void Copy(BeamFlagsParameters other)
        {
            base.Copy(other);
            this.PortName = other.PortName;
        }

        public override bool Equals(BeamFlagsParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            if (Type == typeof(BeamFlags))
            {
                iseq &= this.PortName == other.PortName;
            }
            return iseq;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = Util.Hash(Type, Name);
                if (Type == typeof(BeamFlags))
                {
                    hash = Util.Hash(hash, PortName);
                }
                return hash;
            }
        }

    }
}
