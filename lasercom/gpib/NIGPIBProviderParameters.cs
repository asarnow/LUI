using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    class NIGPIBProviderParameters : GPIBProviderParameters
    {
        private int _BoardNumber;
        public int BoardNumber
        {
            get
            {
                return _BoardNumber;
            }
            private set
            {
                _BoardNumber = value;
            }
        }

        public NIGPIBProviderParameters(string ProviderType, int BoardNumber) : base(ProviderType)
        {
            this.BoardNumber = BoardNumber;
        }

        public NIGPIBProviderParameters(int BoardNumber)
            : this("NIGPIBProvider", BoardNumber)
        {

        }

        public object[] ToConstructorArray()
        {
            return new object[] { BoardNumber };
        }
    }
}
