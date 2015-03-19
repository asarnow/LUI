using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{
    public abstract class LuiObjectParameters
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
        }

        public abstract object[] ConstructorArray { get; }

        public LuiObjectParameters()
        {

        }

        public LuiObjectParameters(Type t)
        {
            Type = t;
        }

    }
}
