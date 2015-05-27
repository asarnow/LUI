using lasercom.objects;
using System;
using System.Runtime.Serialization;

namespace lasercom.control
{
    public class PumpParameters : LuiObjectParameters<PumpParameters>
    {

        [DataMember]
        public string PortName { get; set; }

        public PumpParameters(Type Type, string PortName) : base(Type)
        {
            this.PortName = PortName;
        }

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(HarvardPump))
                {
                    arr = new object[] { PortName };
                }
                else if (Type == typeof(DummyPump))
                {
                    arr = new object[0];
                }
                return arr;
            }
        }

        public PumpParameters(Type Type)
            : base(Type)
        {

        }

        public PumpParameters()
            : base()
        {

        }

        public override void Copy(PumpParameters other)
        {
            base.Copy(other);
            this.PortName = other.PortName;
        }

        public override bool Equals(PumpParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            if (Type == typeof(HarvardPump))
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
                if (Type == typeof(HarvardPump))
                {
                    hash = Util.Hash(hash, PortName);
                }
                return hash;
            }
        }

    }
}
