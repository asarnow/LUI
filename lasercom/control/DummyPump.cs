
namespace lasercom.control
{
    public class DummyPump : AbstractPump
    {
        public DummyPump()
        {
            SetClosed();
        }

        protected override void Dispose(bool disposing)
        {
            // Nothing to dispose.
        }
    }
}
