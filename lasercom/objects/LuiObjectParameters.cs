using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{

    /// <summary>
    /// Nongeneric base for all instrument parameters.
    /// Permits access of fields shared by all instrument parameters without
    /// knowing the exact runtime parameters class.
    /// </summary>
    public abstract class LuiObjectParameters
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string ParametersTypeName
        {
            get
            {
                return this.GetType().AssemblyQualifiedName;
            }
            set
            {

            }
        }

        private Type _Type;
        [System.Xml.Serialization.XmlIgnore]
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

        [System.Xml.Serialization.XmlAttribute]
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
    }

    /// <summary>
    /// Self-constrained generic base class for all instrument parameters.
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public abstract class LuiObjectParameters<P> : LuiObjectParameters, 
        IEquatable<P> where P : LuiObjectParameters<P>
    {
        public abstract object[] ConstructorArray { get; }

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
            return Util.Hash(Type.GetHashCode(), Name.GetHashCode());
        }
    }
}
