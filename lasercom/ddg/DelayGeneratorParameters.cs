using System;
using System.Runtime.Serialization;
using lasercom.gpib;
using lasercom.objects;

namespace lasercom.ddg
{
    /// <summary>
    /// Stores parameters for instantiation of a DDG and provides
    /// fpr their serialization to XML.
    /// </summary>
    [DataContract]
    public class DelayGeneratorParameters : LuiObjectParameters<DelayGeneratorParameters>
    {
        [DataMember]
        public byte GpibAddress { get; set; }
        
        [DataMember]
        public GpibProviderParameters GpibProvider { get; set; }

        public override LuiObjectParameters[] Dependencies
        {
            get
            {
                if (GpibProvider != null)
                    return new LuiObjectParameters[] {GpibProvider};
                return new LuiObjectParameters[0];
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

        public DelayGeneratorParameters(DelayGeneratorParameters other) 
            : base(other)
        {

        }

        public override void Copy(DelayGeneratorParameters other)
        {
            base.Copy(other);
            this.GpibAddress = other.GpibAddress;
            this.GpibProvider = other.GpibProvider;
        }

        //public override bool Equals(DelayGeneratorParameters other)
        //{
        //    bool iseq = base.Equals(other);
        //    if (!iseq) return iseq;

        //    if (Type == typeof(DG535))
        //    {
        //        iseq &= GpibAddress == other.GpibAddress &&
        //                ( GpibProvider==other.GpibProvider || (GpibProvider !=null && GpibProvider.Equals(other.GpibProvider)) );
        //        // Equal if (addresses are the same AND (providers ref. equal OR (provider is not null AND equals other provider)).
        //        // Note that providers will be ref. equal if both are null and that due to short circuiting
        //        // GpibProvider.Equals wont be called if GpibProvider is null.
        //    }
        //    return iseq;
        //}

        //public override bool Equals(object other)
        //{
        //    return Equals(other as DelayGeneratorParameters);
        //}

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        int hash = Util.Hash(Type, Name);
        //        if (Type == typeof(DG535))
        //        {
        //            hash = Util.Hash(hash, GpibProvider);
        //            hash = Util.Hash(hash, GpibAddress);
        //        }
        //        return hash;
        //    }
        //}

        public override bool NeedsReinstantiation(DelayGeneratorParameters other)
        {
            bool needs = base.NeedsReinstantiation(other);
            if (needs) return true;

            if (Type == typeof(StanfordDigitalDelayGenerator) || Type.IsSubclassOf(typeof(StanfordDigitalDelayGenerator)))
            {
                needs |= (GpibProvider != other.GpibProvider || (GpibProvider != null && !GpibProvider.Equals(other.GpibProvider)));
            }
            return needs;
        }

        public override bool NeedsUpdate(DelayGeneratorParameters other)
        {
            bool needs = false;

            if (Type == typeof(StanfordDigitalDelayGenerator) || Type.IsSubclassOf(typeof(StanfordDigitalDelayGenerator)))
            {
                needs |= other.GpibAddress != GpibAddress;
            }

            return needs;
        }
    }
}
