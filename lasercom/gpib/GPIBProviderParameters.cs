using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI.gpib
{
    public abstract class GPIBProviderParameters
    {
        private string _ProviderType;
        public string ProviderType
        {
            get
            {
                return _ProviderType;
            }
            private set
            {
                _ProviderType = value;
            }
        }

        public GPIBProviderParameters(string ProviderType)
        {
            this.ProviderType = ProviderType;
        }

        virtual public object[] ToConstructorArray()
        {
            throw new NotImplementedException();
        }
    }
}
