using lasercom;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DetectorTester
{
    public partial class BeamFlagTestForm : Form
    {
        private Encoding AsciiEncoding = Encoding.ASCII;
        private SerialPort ComPort;

        public BeamFlagTestForm()
        {
            InitializeComponent();

            foreach (var rate in (new int[] { 300, 600, 1200, 1800, 2400, 4800, 9600, 19200, 38400, 57600, 115200 }))
            {
                BaudRate.Items.Add(rate);
            }
            BaudRate.SelectedItem = 9600;

            IList<string> ports = Util.EnumerateSerialPorts();
            foreach (var port in ports)
            {
                ComPorts.Items.Add(port);
            }

            ComPorts.SelectedIndexChanged += ComPorts_SelectedIndexChanged;
            BaudRate.SelectedIndexChanged += BaudRate_SelectedIndexChanged;

            Console.Command += Console_Command;
        }

        private void InitComPort()
        {
            if (ComPort != null)
            {
                if (ComPort.IsOpen) ComPort.Close();
                Thread.Sleep(100);
                ComPort.Dispose();
                ComPort = null; // Overkill.
            }

            ComPort = new SerialPort();
            ComPort.PortName = (string)ComPorts.SelectedItem;
            ComPort.BaudRate = (int)BaudRate.SelectedItem;
            ComPort.DataBits = 8;
            ComPort.Parity = Parity.None;
            ComPort.StopBits = StopBits.One;

            // RTS/CTS handshakings
            ComPort.Handshake = Handshake.RequestToSend;
            ComPort.DtrEnable = DtrEnable.Checked;
            ComPort.RtsEnable = RtsEnable.Checked;

            // Error handling
            ComPort.DiscardNull = false;
            ComPort.ParityReplace = 0;

            ComPort.Open();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (ComPort != null)
            {
                if (ComPort.IsOpen)
                {
                    ComPort.Close();
                    Thread.Sleep(100);
                    ComPort.Dispose();
                }
            }
            base.OnFormClosed(e);
        }

        void ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComPort();
        }

        void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComPort();
        }

        void Console_Command(object sender, CommandPrompt.CommandEventArgs e)
        {
            ComPort.Write(AsciiEncoding.GetBytes(e.Command), 0, AsciiEncoding.GetByteCount(e.Command));
            if (e.Command[2] == 'R')
            {
                int result = ComPort.ReadByte();
                byte[] bytes = BitConverter.GetBytes(result);
                e.Message = Convert.ToString(bytes[0], 2);
            }
            else
            {
                e.Message = "";
            }
        }

        private void SendZero_Click(object sender, EventArgs e)
        {
            ComPort.Write("!0SO0");
        }

        private void SendOne_Click(object sender, EventArgs e)
        {
            ComPort.Write("!0SO1");
        }

        private void SendTwo_Click(object sender, EventArgs e)
        {
            ComPort.Write("!0SO2");
        }

        private void SendThree_Click(object sender, EventArgs e)
        {
            ComPort.Write("!0SO3");
        }

        private void DtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            InitComPort();
        }

        private void RtsEnable_CheckedChanged(object sender, EventArgs e)
        {
            InitComPort();
        }
    }
}
