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
        /// <summary>
        /// Number of detector hardware pixels in horizontal axis.
        /// </summary>
        uint Width { get; }

        /// <summary>
        /// Number of detector hardware pixels in vertical axis.
        /// </summary>
        uint Height { get; }

        /// <summary>
        /// Number of elements in acquisition data given current camera settings.
        /// </summary>
        uint AcqSize { get; }

        /// <summary>
        /// Current acquistion mode (single, sequence, etc.).
        /// </summary>
        int AcquisitionMode { get; set; }

        /// <summary>
        /// Current trigger mode (internal, external, etc.).
        /// </summary>
        int TriggerMode { get; set; }

        /// <summary>
        /// Current trigger mode of any internal timing device(s).
        /// </summary>
        int DDGTriggerMode { get; set; }

        /// <summary>
        /// Current readout mode (hardware binning, full image, etc.).
        /// </summary>
        int ReadMode { get; set; }

        /// <summary>
        /// True if camera possesses a hardware intensifier.
        /// </summary>
        bool HasIntensifier { get; }

        /// <summary>
        /// Current gain setting of hardware intensifier, if any.
        /// </summary>
        int IntensifierGain { get; set; }

        /// <summary>
        /// Minimum hardware supported gain.
        /// </summary>
        int MinIntensifierGain { get; }

        /// <summary>
        /// Maximum hardware supported gain.
        /// </summary>
        int MaxIntensifierGain { get; }

        /// <summary>
        /// True if wavelengths are increasing along the camera's horizontal access.
        /// TODO: Move functionality to Spectrograph API.
        /// </summary>
        bool CalibrationAscending { get; }

        /// <summary>
        /// Wavelength values along camera's horizontal access.
        /// TODO: Probably should be in Spectrograph API.
        /// </summary>
        double[] Calibration { get; set; }

        /// <summary>
        /// Specifies current image capture area along with any hardware binning.
        /// </summary>
        ImageArea Image { get; set; }

        /// <summary>
        /// Captures using full vertical binning. Preserves original settings.
        /// </summary>
        /// <returns>New data array.</returns>
        int[] CountsFvb();

        /// <summary>
        /// Captures a full resolution image from the hardware. Preserves original settings.
        /// </summary>
        /// <returns>New data array.</returns>
        int[] FullResolutionImage();

        /// <summary>
        /// Acquire data with current settings.
        /// </summary>
        /// <returns>New data array.</returns>
        int[] Acquire();

        /// <summary>
        /// Acquire data with current settings.
        /// </summary>
        /// <param name="DataBuffer">Existing data array to repopulate.</param>
        /// <returns>Camera status code.</returns>
        uint Acquire(int[] DataBuffer);

        /// <summary>
        /// Convert hardware status code to a string.
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Camera status string.</returns>
        string DecodeStatus(uint status);

    }
}
