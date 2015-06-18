using System;
using lasercom.gpib;

namespace LUI.controls
{
    public class DummyGpibProviderConfigPanel : LuiObjectConfigPanel<GpibProviderParameters>
    {
        public override Type Target
        {
            get { return typeof(DummyGpibProvider); }
        }

        public DummyGpibProviderConfigPanel()
            : base()
        {

        }

        public override void CopyTo(GpibProviderParameters other)
        {
            
        }

        public override void CopyFrom(GpibProviderParameters other)
        {
            
        }
    }
}
