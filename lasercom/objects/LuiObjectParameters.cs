using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{
    public abstract class LuiObjectParameters
    {

    }

    public abstract class LuiObjectParameters<T> : LuiObjectParameters, 
        IEquatable<T> where T : LuiObjectParameters<T>
    {
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

        public string Name { get; set; }

        public string TypeName
        {
            get
            {
                return Type.Name;
            }
            set
            {
                _Type = Type.GetType(value);
            }
        }

        public abstract object[] ConstructorArray { get; }

        public LuiObjectParameters()
        {

        }

        public LuiObjectParameters(Type t)
        {
            Type = t;
        }

        public virtual void Copy(T other)
        {
            this.Type = other.Type;
            this.Name = other.Name;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            return Equals(other as T);
        }

        public virtual bool Equals(T other)
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
