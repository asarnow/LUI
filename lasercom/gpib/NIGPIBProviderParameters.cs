using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    public class NIGPIBProviderParameters : GPIBProviderParameters
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

        public NIGPIBProviderParameters()
            : base(typeof(NIGPIBProvider))
        {

        }

        public NIGPIBProviderParameters(Type ProviderType, int BoardNumber) : base(ProviderType)
        {
            this.BoardNumber = BoardNumber;
        }

        public NIGPIBProviderParameters(int BoardNumber)
            : this(typeof(NIGPIBProvider), BoardNumber)
        {

        }

        public object[] ToConstructorArray()
        {
            return new object[] { BoardNumber };
        }
    }
}
