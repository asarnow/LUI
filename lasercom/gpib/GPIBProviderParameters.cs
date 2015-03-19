using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom.objects;

namespace lasercom.gpib
{
    public class GpibProviderParameters : LuiObjectParameters
    {
        private int _BoardNumber;
        public int BoardNumber
        {
            get
            {
                return _BoardNumber;
            }
            set
            {
                _BoardNumber = value;
            }
        }

        public string PortName { get; set; }
        public int Timeout { get; set; }

        override public object[] ConstructorArray
        {
            get
            {
                object[] arr = null;
                if (Type == typeof(NIGpibProvider))
                {
                    arr = new object[] { BoardNumber };
                }
                else if (Type == typeof(PrologixGpibProvider))
                {
                    arr = new object[] { PortName, Timeout };
                }
                return arr;
            }
        }

        public GpibProviderParameters(Type type)
            : base(type)
        {

        }

        public GpibProviderParameters()
            : base()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            GpibProviderParameters p = (GpibProviderParameters)obj;
            bool iseq = Type == p.Type &&
                        Name == p.Name;
            if (Type == typeof(NIGpibProvider))
            {
                iseq &= BoardNumber == p.BoardNumber;
            }
            else if (Type == typeof(PrologixGpibProvider))
            {
                iseq &= PortName == p.PortName &&
                        Timeout == p.Timeout;
            }
            return iseq;
        }

        public override int GetHashCode()
        {
            if (Type == typeof(NIGpibProvider))
            {
                return Name.GetHashCode() * BoardNumber.GetHashCode();
            }
            else if (Type == typeof(PrologixGpibProvider))
            {
                return Name.GetHashCode() * PortName.GetHashCode() * Timeout.GetHashCode();
            }
            return 0;
        }

        //public static override bool operator==(GPIBProvider p, GPIBProvider q){
        //    return p.Equals(q);
        //}

    }
}

