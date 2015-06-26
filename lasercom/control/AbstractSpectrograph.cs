
using lasercom.objects;
namespace lasercom.control
{
    public abstract class AbstractSpectrograph : LuiObject, ISpectrograph
    {
        public abstract double Wavelength { get; set; }
        public abstract double SlitWidth { get; set; }

        public abstract void Close();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
    }
}
