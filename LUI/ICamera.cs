//  <summary>
//      Interface for a camera. Any abstract or concrete camera class should
//      implement this interface.
//  </summary>

namespace LUI
{
    public interface ICamera
    {        

        void GetCountsFvb();

        void GetImage();

        void Close();

    }
}
