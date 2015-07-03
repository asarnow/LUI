using System;
using System.Runtime.Serialization;
using lasercom.objects;

namespace lasercom.gpib
{
    /// <summary>
    /// Stores parameters for instantiation of a GPIB provider and provides
    /// for their serialization to XML.
    /// </summary>
    [DataContract]
    public class GpibProviderParameters : LuiObjectParameters<GpibProviderParameters>
    {
        private int _BoardNumber;
        [DataMember]
        public int BoardNumber
        {
            get
            {
                return _BoardNumber;
            }
            set
            {
                _BoardNumber = value;
            }
        }

        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public int Timeout { get; set; }

        public GpibProviderParameters(Type type)
            : base(type)
        {

        }

        public GpibProviderParameters()
            : base()
        {

        }

        public GpibProviderParameters(GpibProviderParameters other)
            : base(other)
        {

        }

        //public override bool Equals(GpibProviderParameters other)
        //{
        //    bool iseq = base.Equals(other);
        //    if (!iseq) return iseq;

        //    if (Type == typeof(NIGpibProvider))
        //    {
        //        iseq &= BoardNumber == other.BoardNumber;
        //    }
        //    else if (Type == typeof(PrologixGpibProvider))
        //    {
        //        iseq &= PortName == other.PortName &&
        //                Timeout == other.Timeout;
        //    }
        //    return iseq;
        //}

        //public override bool Equals(object other)
        //{
        //    return Equals(other as GpibProviderParameters);
        //}

        //public override int GetHashCode()
        //{
        //    unchecked // Overflow is fine, just wrap
        //    {
        //        int hash = Util.Hash(Type, Name);
        //        if (Type == typeof(NIGpibProvider))
        //        {
        //            hash = Util.Hash(hash,BoardNumber);
        //        }
        //        else if (Type == typeof(PrologixGpibProvider))
        //        {
        //            hash = Util.Hash(hash, PortName);
        //            hash = Util.Hash(hash, Timeout);
        //        }
        //        return hash;
        //    }
        //}

        //public static override bool operator==(GPIBProvider p, GPIBProvider q){
        //    return p.Equals(q);
        //}

        public override void Copy(GpibProviderParameters other)
        {
            base.Copy(other);
            this.PortName = other.PortName;
            this.Timeout = other.Timeout;
            this.BoardNumber = other.BoardNumber;
        }

        public override bool NeedsReinstantiation(GpibProviderParameters other)
        {
            bool needs = base.NeedsReinstantiation(other);
            if (needs) return true;

            if (Type == typeof(NIGpibProvider))
            {
                needs |= BoardNumber != other.BoardNumber;
            }
            else if (Type == typeof(PrologixGpibProvider))
            {
                needs |= PortName != other.PortName;
            }
            return needs;
        }

        public override bool NeedsUpdate(GpibProviderParameters other)
        {
            bool needs = false;

            if (Type == typeof(PrologixGpibProvider))
            {
                needs |= other.Timeout != Timeout;
            }

            return needs;
        }
    }
}

