using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.gpib;

namespace LUI.controls
{
    public class DummyGpibProviderConfigPanel : LuiObjectConfigPanel<GpibProviderParameters>
    {
        public override Type Target
        {
            get { return typeof(DummyGpibProvider); }
        }

        public override void CopyTo(GpibProviderParameters other)
        {
            
        }

        public override void CopyFrom(GpibProviderParameters other)
        {
            
        }
    }
}
