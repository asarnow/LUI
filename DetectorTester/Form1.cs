using LUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetectorTester
{
    public partial class Form1 : Form
    {
        private Commander Commander;
        
        private int[] blank;
        private int[] dark;
        private int[] counts;
        private int[] image;

        public Form1(Commander commander)
        {
            Commander = commander;
            InitializeComponent();
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            image = Commander.Camera.GetFullResolutionImage();
            Util.normalizeArray(image, 255);

            Bitmap picture = new Bitmap(1024, 256);
            for (int x = 0; x < picture.Width; x++)
            {
                for (int y = 0; y < picture.Height; y++)
                {
                    Color c = Color.FromArgb(image[y * picture.Height + x], image[y * picture.Height + x], image[y * picture.Height + x]);
                    picture.SetPixel(x, y, c);
                }
            }

            imageBox.Image = picture;
        }

        private void darkButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.SetAcquisitionMode(Constants.AcqModeSingle);
            Commander.Camera.SetTriggerMode(Constants.TrigModeExternalExposure);
            Commander.Camera.SetReadMode(Constants.ReadModeFVB);
            dark = Commander.Dark();
        }

        private void blankButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.SetAcquisitionMode(Constants.AcqModeSingle);
            Commander.Camera.SetTriggerMode(Constants.TrigModeExternalExposure);
            Commander.Camera.SetReadMode(Constants.ReadModeFVB);
            blank = Commander.Flash();

            if (dark != null)
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    counts[i] -= dark[i];
                }
            }
        }

        private void specButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.SetAcquisitionMode(Constants.AcqModeSingle);
            Commander.Camera.SetTriggerMode(Constants.TrigModeExternalExposure);
            Commander.Camera.SetReadMode(Constants.ReadModeFVB);
            counts = Commander.Flash();

            if (dark != null)
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    counts[i] -= dark[i];
                }
            }

            if (blank != null)
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    counts[i] = blank[i] - counts[i];
                }
            }

            for (int i = 0; i < counts.Length; i++)
                specGraph.Series["spec"].Points.AddXY(i, counts[i]);

            specGraph.Series["spec"].ChartArea = "specArea";

            dark = null;
            blank = null;
        }


    }
}
