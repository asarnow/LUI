using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom.gpib;
using lasercom.objects;

namespace lasercom.ddg
{
    /// <summary>
    /// Stores parameters for instantiation of a DDG and provides
    /// fpr their serialization to XML.
    /// </summary>
    public class DelayGeneratorParameters : LuiObjectParameters<DelayGeneratorParameters>
    {
        [System.Xml.Serialization.XmlAttribute]
        public byte GpibAddress { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string GpibProviderName { get; set; }
        
        [System.Xml.Serialization.XmlIgnore]
        public IGpibProvider GpibProvider { get; set; }

        public override object[] ConstructorArray
        {
            get
            { 
                object[] arr = null;
                if (Type == typeof(DDG535)){
                    arr = new object[] { GpibProvider, GpibAddress };
                }
                return arr;
            }
        }

        public DelayGeneratorParameters(Type Type)
            : base(Type)
        {

        }

        public DelayGeneratorParameters()
            : base()
        {

        }

        public override void Copy(DelayGeneratorParameters other)
        {
            base.Copy(other);
            this.GpibAddress = other.GpibAddress;
            this.GpibProviderName = other.GpibProviderName;
        }

        public override bool Equals(DelayGeneratorParameters other)
        {
            bool iseq = base.Equals(other);
            if (!iseq) return iseq;

            if (Type == typeof(DDG535))
            {
                iseq &= GpibAddress == other.GpibAddress &&
                        GpibProviderName == other.GpibProviderName;
            }
            return iseq;
        }
        public override bool Equals(object other)
        {
            return Equals(other as DelayGeneratorParameters);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Util.Hash(Type.GetHashCode(), Name.GetHashCode());
                if (Type == typeof(DDG535))
                {
                    hash = Util.Hash(hash, GpibProviderName.GetHashCode());
                    hash = Util.Hash(hash, GpibAddress.GetHashCode());
                }
                return hash;
            }
        }
    }
}
