using lasercom.objects;
using log4net;

namespace lasercom.gpib
{
    /// <summary>
    /// Base class for all GPIB providers.
    /// </summary>
    public abstract class AbstractGpibProvider : LuiObject<GpibProviderParameters>, IGpibProvider
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        abstract public void LoggedWrite(byte address, string command);

        abstract public string LoggedQuery(byte address, string command);

        public override void Update(GpibProviderParameters p)
        {
            throw new System.NotImplementedException();
        }
    }
}
