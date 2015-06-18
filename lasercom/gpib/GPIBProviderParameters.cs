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

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(NIGpibProvider))
                {
                    arr = new object[] { BoardNumber };
                }
                else if (Type == typeof(PrologixGpibProvider))
                {
                    arr = new object[] { PortName, Timeout };
                }
                else if (Type == typeof(DummyGpibProvider))
                {
                    arr = new object[0];
                }
                return arr;
            }
        }

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

        public override bool Equals(GpibProviderParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            if (Type == typeof(NIGpibProvider))
            {
                iseq &= BoardNumber == other.BoardNumber;
            }
            else if (Type == typeof(PrologixGpibProvider))
            {
                iseq &= PortName == other.PortName &&
                        Timeout == other.Timeout;
            }
            return iseq;
        }

        public override bool Equals(object other)
        {
            return Equals(other as GpibProviderParameters);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = Util.Hash(Type, Name);
                if (Type == typeof(NIGpibProvider))
                {
                    hash = Util.Hash(hash,BoardNumber);
                }
                else if (Type == typeof(PrologixGpibProvider))
                {
                    hash = Util.Hash(hash, PortName);
                    hash = Util.Hash(hash, Timeout);
                }
                return hash;
            }
        }

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

    }
}

