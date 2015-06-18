using System;
using System.Runtime.Serialization;
using Extensions;


namespace lasercom.objects
{

    /// <summary>
    /// Nongeneric base for all instrument parameters.
    /// Permits access of fields shared by all instrument parameters without
    /// knowing the exact runtime parameters class.
    /// </summary>
    [DataContract]
    [KnownType("GetKnownTypes")]
    public abstract class LuiObjectParameters
    {
        [DataMember]
        public string Name { get; set; }

        public LuiObjectParameters Self
        {
            get
            {
                return this;
            }
        }

        private Type _Type;
        public Type Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        [DataMember]
        public string TypeName
        {
            get
            {
                return Type.FullName;
            }
            set
            {
                _Type = Type.GetType(value);
            }
        }

        public virtual LuiObjectParameters[] Dependencies
        {
            get
            {
                return new LuiObjectParameters[0];
            }
        }

        public abstract object[] ConstructorArray { get; }

        static Type[] GetKnownTypes()
        {
            return typeof(LuiObjectParameters).GetSubclasses(true).ToArray();
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            return Equals(other as LuiObjectParameters);
        }

        public virtual bool Equals(LuiObjectParameters other)
        {
            if (other == null || GetType() != other.GetType())
                return false;
            bool iseq = Type == other.Type &&
                        Name == other.Name;
            return iseq;
        }

        public override int GetHashCode()
        {
            return Util.Hash(Type, Name);
        }
    }

    /// <summary>
    /// Self-constrained generic base class for all instrument parameters.
    /// </summary>
    /// <typeparam name="P"></typeparam>
    [DataContract]
    public abstract class LuiObjectParameters<P> : LuiObjectParameters, 
        IEquatable<P> where P : LuiObjectParameters<P>
    {

        public new LuiObjectParameters<P> Self
        {
            get
            {
                return this;
            }
        }

        public LuiObjectParameters()
        {

        }

        public LuiObjectParameters(P other) : base()
        {
            Copy(other);
        }

        public LuiObjectParameters(Type t)
        {
            Type = t;
        }

        public virtual void Copy(P other)
        {
            this.Type = other.Type;
            this.Name = other.Name;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            return Equals(other as P);
        }

        public override bool Equals(LuiObjectParameters other)
        {
            return Equals(other as P);
        }

        public virtual bool Equals(P other)
        {
            bool iseq = base.Equals(other);
            return iseq;
        }

        public override int GetHashCode()
        {
            return Util.Hash(Type, Name);
        }
    }
}
