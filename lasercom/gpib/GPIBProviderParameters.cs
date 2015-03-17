using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    public abstract class GPIBProviderParameters
    {
        private Type _ProviderType;
        public Type ProviderType
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

        public string Name { get; set; }

        public string ProviderTypeName
        {
            get
            {
                return ProviderType.Name;
            }
        }

        public GPIBProviderParameters(Type ProviderType)
        {
            this.ProviderType = ProviderType;
        }

        virtual public object[] ToConstructorArray()
        {
            throw new NotImplementedException();
        }
    }
}
