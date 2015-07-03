
using System.Runtime.Serialization;
namespace lasercom.camera
{
    /// <summary>
    /// Defines an image area including binning.
    /// </summary>
    [DataContract]
    public class ImageArea
    {
        [DataMember]
        public readonly int hbin, vbin, hstart, hcount, vstart, vcount;

        public ImageArea(int hbin, int vbin, int hstart, int hcount, int vstart, int vcount)
        {
            this.hbin = hbin;
            this.vbin = vbin;
            this.hstart = hstart;
            this.hcount = hcount;
            this.vstart = vstart;
            this.vcount = vcount;
        }

        public int Width
        {
            get
            {
                return hcount / hbin;
            }
        }

        public int Height
        {
            get
            {
                return vcount / vbin;
            }
        }
    }
}
