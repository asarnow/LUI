using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom.objects;

namespace lasercom.control
{
    public class BeamFlagsParameters : LuiObjectParameters
    {        

        public string PortName { get; set; }

        public BeamFlagsParameters(Type Type, string PortName) : base(Type)
        {
            this.PortName = PortName;
        }

        override public object[] ConstructorArray
        {
            get
            {
                return new object[] {PortName};           
            }
        }

        public BeamFlagsParameters(Type Type)
            : base(Type)
        {

        }

        public BeamFlagsParameters()
            : base()
        {

        }
        
    }
}
