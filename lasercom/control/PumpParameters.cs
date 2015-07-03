using System;
using System.Runtime.Serialization;
using lasercom.objects;

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

        public PumpParameters(Type Type)
            : base(Type)
        {

        }

        public PumpParameters()
            : base()
        {

        }

        public PumpParameters(PumpParameters other)
            : base(other)
        {

        }

        public override void Copy(PumpParameters other)
        {
            base.Copy(other);
            this.PortName = other.PortName;
        }

        //public override bool Equals(PumpParameters other)
        //{
        //    bool iseq = base.Equals(other);
        //    if (!iseq) return iseq;

        //    if (Type == typeof(HarvardPump))
        //    {
        //        iseq &= this.PortName == other.PortName;
        //    }
        //    return iseq;
        //}

        //public override int GetHashCode()
        //{
        //    unchecked // Overflow is fine, just wrap
        //    {
        //        int hash = Util.Hash(Type, Name);
        //        if (Type == typeof(HarvardPump))
        //        {
        //            hash = Util.Hash(hash, PortName);
        //        }
        //        return hash;
        //    }
        //}

        public override bool NeedsReinstantiation(PumpParameters other)
        {
            bool needs =  base.NeedsReinstantiation(other);
            if (needs) return true;

            if (Type == typeof(HarvardPump) || Type.IsSubclassOf(typeof(HarvardPump)))
            {
                needs |= other.PortName != PortName;
            }
            return needs;
        }

        public override bool NeedsUpdate(PumpParameters other)
        {
            throw new NotImplementedException();
        }
    }
}
