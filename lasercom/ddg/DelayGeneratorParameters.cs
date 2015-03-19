using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom.gpib;
using lasercom.objects;

namespace lasercom.ddg
{
    public class DelayGeneratorParameters : LuiObjectParameters
    {
        public byte GPIBAddress { get; set; }
        public string GPIBProviderName { get; set; }
        
        [System.Xml.Serialization.XmlIgnore]
        public IGpibProvider GPIBProvider { get; set; }

        public override object[] ConstructorArray
        {
            get
            { 
                object[] arr = null;
                if (Type == typeof(DDG535)){
                    arr = new object[] { GPIBProvider, GPIBAddress };
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
    }
}
