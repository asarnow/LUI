using System;
using System.Runtime.Serialization;
using lasercom.objects;

namespace lasercom.control
{
    [DataContract]
    public class BeamFlagsParameters : LuiObjectParameters<BeamFlagsParameters>
    {        
        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public int Delay { get; set; }

        public BeamFlagsParameters(Type Type, string PortName)
            : base(Type)
        {
            this.PortName = PortName;
        }

        public BeamFlagsParameters(BeamFlagsParameters other)
            : base(other)
        {

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
            this.Delay = other.Delay;
        }

        //public override bool Equals(BeamFlagsParameters other)
        //{
        //    bool iseq = base.Equals(other);
        //    if (!iseq) return iseq;

        //    if (Type == typeof(BeamFlags))
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
        //        if (Type == typeof(BeamFlags))
        //        {
        //            hash = Util.Hash(hash, PortName);
        //        }
        //        return hash;
        //    }
        //}

        public override bool NeedsReinstantiation(BeamFlagsParameters other)
        {
            bool needs = base.NeedsReinstantiation(other);
            if (needs) return true;

            if (Type == typeof(BeamFlags) || Type.IsSubclassOf(typeof(BeamFlags)))
            {
                needs |= other.PortName != PortName;
            }
            return needs;
        }

        public override bool NeedsUpdate(BeamFlagsParameters other)
        {
            bool needs = other.Delay != Delay;
            return needs;
        }
    }
}
