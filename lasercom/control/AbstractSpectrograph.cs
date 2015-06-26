
using lasercom.objects;
namespace lasercom.control
{
    interface AbstractSpectrograph : LuiObject, ISpectrograph
    {
        public abstract double Wavelength { get; set; }
        public abstract double SlitWidth { get; set; }
    }
}
