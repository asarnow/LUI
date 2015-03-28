using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{
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
