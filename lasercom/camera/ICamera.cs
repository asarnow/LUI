//  <summary>
//      Interface for a camera. Any abstract or concrete camera class should
//      implement this interface.
//  </summary>

namespace lasercom.camera
{
    /// <summary>
    /// Interface for all camera classes.
    /// Cameras which do not support certain features should return null for 
    /// all relevant properties.
    /// </summary>
    public interface ICamera
    {

        uint Width { get; }
        uint Height { get; }
        uint AcqSize { get; }
        int AcquisitionMode { get; set; }
        int TriggerMode { get; set; }
        int DDGTriggerMode { get; set; }
        int ReadMode { get; set; }
        
        bool HasIntensifier { get; }
        int IntensifierGain { get; set; }
        int MinIntensifierGain { get; }
        int MaxIntensifierGain { get; }

        int[] CountsFvb();

        int[] FullResolutionImage();

        int[] Acquire();

        uint Acquire(int[] DataBuffer);

        void Close();

    }
}
