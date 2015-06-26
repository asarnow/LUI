using ATSHAMROCKCS;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

namespace lasercom.objects
{
    public sealed class AndorFactory
    {
        private static volatile AndorSDK _AndorSdkInstance;
        private static volatile ShamrockSDK _ShamrockSdkInstance;
        private static object ShamrockSdklock = new object();
        private static object AndorSdkLock = new object();

        private AndorFactory() {}

        public static AndorSDK AndorSdkInstance
        {
            get
            {
                if (_AndorSdkInstance == null)
                {
                    lock (AndorSdkLock)
                    {
                        if (_AndorSdkInstance == null)
                        {
                            _AndorSdkInstance = new AndorSDK();
                        }
                    }
                }
                return _AndorSdkInstance;
            }
        }

        public static ShamrockSDK ShamrockSdkInstance
        {
            get
            {
                if (_ShamrockSdkInstance == null)
                {
                    lock (ShamrockSdklock)
                    {
                        if (_ShamrockSdkInstance == null)
                        {
                            _ShamrockSdkInstance = new ShamrockSDK();
                        }
                    }
                }
                return _ShamrockSdkInstance;
            }
        }
    }
}
