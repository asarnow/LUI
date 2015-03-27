using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom.objects;
using System.Reflection;

namespace lasercom.gpib
{
    public class GpibProviderParameters : LuiObjectParameters<GpibProviderParameters>
    {
        private int _BoardNumber;
        [System.Xml.Serialization.XmlAttribute]
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

        [System.Xml.Serialization.XmlAttribute]
        public string PortName { get; set; }

        [System.Xml.Serialization.XmlAttribute]
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

        public override bool Equals(GpibProviderParameters other)
        {
            bool iseq = base.Equals(other);
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
                int hash = Name.GetHashCode();
                if (Type == typeof(NIGpibProvider))
                {
                    hash = Util.Hash(hash,BoardNumber.GetHashCode());
                }
                else if (Type == typeof(PrologixGpibProvider))
                {
                    hash = Util.Hash(hash, PortName.GetHashCode());
                    hash = Util.Hash(hash, Timeout.GetHashCode());
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

