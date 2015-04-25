using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using lasercom.extensions;


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

        public virtual bool Equals(P other)
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
}
