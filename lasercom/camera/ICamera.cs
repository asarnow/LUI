//  <summary>
//      Interface for a camera. Any abstract or concrete camera class should
//      implement this interface.
//  </summary>

namespace LUI
{
    public interface ICamera
    {        

        int[] CountsFvb();

        int[] FullResolutionImage();

        int[] Acquire();

        void Close();

    }
}
