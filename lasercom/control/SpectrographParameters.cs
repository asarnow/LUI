using lasercom.objects;

namespace lasercom.control
{
    public class SpectrographParameters : LuiObjectParameters<SpectrographParameters>
    {
        public int Device { get; set; }

        public override void Copy(SpectrographParameters other)
        {
            base.Copy(other);
            Device = other.Device;
        }

        public override bool NeedsReinstantiation(SpectrographParameters other)
        {
 	        bool needs = base.NeedsReinstantiation(other);
            if (needs) return true;
            if (Type == typeof(Shamrock))
            {
                needs |= other.Device == Device;
            }
            return needs;
        }

        public override bool NeedsUpdate(SpectrographParameters other)
        {
            return false;
        }
    }
}
