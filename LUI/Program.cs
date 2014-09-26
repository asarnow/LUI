using System;
using System.Collections.Generic;
using System.IO;
using NDesk.Options;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

using LUI;

namespace LUI
{
    class Program
    {

        static void Main(string[] args)
        {
            int N = 1;
            string mode = "ACCUM";
            bool show_help = false;
            int temp = Constants.DefaultTemperature;
            string fname = "data.csv";
            string dir = "";
            String flagPort = "COM1";
            var p = new OptionSet() {
                {"n=", "Number of accumulations",
                    (int v) => N = v },
                { "m|mode=", "Mode selector. One of: ACCUM, SPEC",
                    v => mode = v },
                {"h|help", "Show this help text and exit.",
                    v => show_help = v != null },
                {"t|temp|temperature=", "Cooler target temperature",
                    (int v) => temp = v },
                {"o|out|output=", "Ouput file for data",
                    v => fname = v },
                {"d|dir|cameradir=", "Camera configuration directory",
                    v => dir = v },
                {"bf|beamflag|beamflagport=", "Name of serial port used by beam flags.",
                    v => flagPort = v}
            };
            List<string> extra;
            try {
                extra = p.Parse(args);
            } catch (OptionException e) {
                Console.WriteLine("Invalid arguments:");
                Console.WriteLine(e.Message);
                return;
            }

            if (show_help) {
                ShowHelp(p);
                return;
            }

            CameraTempControlled camera = new CameraTempControlled(dir);
            camera.EquilibrateTemperature(temp);

            BeamFlags beamflags = new BeamFlags(flagPort);

            switch (mode.ToUpper())
            {
                case "ACCUM":
                    camera.SetReadMode(Constants.ReadModeFVB);
                    camera.SetAcquisitionMode(Constants.AcquisitionModeAccumulate);
                    camera.SetNumberAccumulations(N);
                    camera.SetTriggerMode(Constants.TriggerModeExternalExposure);
                    beamflags.OpenFlash();
                    int[] data = camera.acquire();
                    beamflags.CloseFlash();

                    for (int i = 0; i < data.Length; i++) data[i] /= N;

                    StreamWriter writer = new StreamWriter(fname);
                    for (int i = 0; i < camera.Width; i++)
                    {
                        writer.Write(data[i]);
                        if (i == camera.Width - 1) 
                        {
                            writer.Write("\n");
                        } else 
                        {
                            writer.Write(",");
                        }
                    }
                    writer.Close();
                    break;
                case "SPEC":
                    camera.SetReadMode(Constants.ReadModeFVB);
                    camera.SetAcquisitionMode(Constants.AcquisitionModeAccumulate);
                    camera.SetNumberAccumulations(N);
                    camera.SetTriggerMode(Constants.TriggerModeExternalExposure);

                    Console.WriteLine("Press any key to collect dark current");
                    Console.ReadKey(true);
                    int[] dark = camera.acquire();

                    Console.WriteLine("Press any key to collect blank");
                    Console.ReadKey(true);
                    beamflags.OpenFlash();
                    int[] blank = camera.acquire();
                    beamflags.CloseFlash();

                    Console.WriteLine("Press any key to collect sample spectrum");
                    Console.ReadKey(true);
                    beamflags.OpenFlash();
                    int[] sample = camera.acquire();
                    beamflags.OpenFlash();

                    for (int i = 0; i < dark.Length; i++) dark[i] /= N;
                    for (int i = 0; i < blank.Length; i++) blank[i] /= N;
                    for (int i = 0; i < sample.Length; i++) sample[i] /= N;

                    double[] spectrum = new double[camera.Width];
                    for (int i = 0; i < camera.Width; i++)
                    {
                        spectrum[i] = Math.Log10( (((double)blank[i]) / ((double)sample[i])) - ((double)dark[i]) );
                    }

                    WriteSingleSpectrum(fname, spectrum);

                    break;
                default:
                    break;
            }

        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Help text.");
        }

        static void WriteSingleSpectrum(string fname, double[] spectrum)
        {
            StreamWriter writer = new StreamWriter(fname);
            for (int i = 0; i < spectrum.Length; i++)
            {
                writer.Write(spectrum[i]);
                if (i == spectrum.Length - 1)
                {
                    writer.Write("\n");
                }
                else
                {
                    writer.Write(",");
                }
            }
            writer.Close();
        }
    
    }
}
