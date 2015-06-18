using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lasercom;

namespace DetectorTester
{
    public partial class DelayGeneratorTestForm : Form
    {
        #region Constants
        const string GpibTerminator = "\r\n";

        const string USBTerminator = "\r\n";
        const char ControllerEscape = (char)27; // ESC character
        const string ControllerCommandInitiator = "++";
        const string AddressCommand = "addr";
        const string AssertControllerInChargeCommand = "ifc";
        const string ModeCommand = "mode";
        const string EOICommand = "eoi";
        const string EOSCommand = "eos";
        const string ReadCommand = "read";
        const string ReadTimeoutCommand = "read_tmo_ms";

        const string DeviceMode = "0";
        const string ControllerMode = "1";
        const string DisableEOI = "0";
        const string EnableEOI = "1";
        const string EOSCRLF = "0";
        const string EOSCR = "1";
        const string EOSLF = "2";
        const string EOSNone = "3";
        #endregion

        private Encoding AsciiEncoding = Encoding.ASCII;
        private SerialPort ComPort;
        private BackgroundWorker worker;
        private bool UsePadding;
        private char PadChar;

        public DelayGeneratorTestForm()
        {
            InitializeComponent();

            SelectPrologix.CheckedChanged += SelectPrologix_CheckedChanged;
            SelectPrologix.Checked = true;

            SelectNi.Enabled = false;

            foreach(var rate in (new int[]{300, 600, 1200, 1800, 2400, 4800, 9600, 19200, 38400, 57600, 115200 }))
            {
                BaudRate.Items.Add(rate);
            }
            BaudRate.SelectedItem = 9600;

            foreach (var padding in (new string[] { "None", "Space", "Null" }))
            {
                Padding.Items.Add(padding);
            }
            Padding.SelectedItem = "Space";
            Padding.SelectedIndexChanged += Padding_SelectedIndexChanged;

            foreach (var address in Enumerable.Range(0, 31)) GpibAddress.Items.Add(address);
            GpibAddress.SelectedItem = 15;
            GpibAddress.SelectionChangeCommitted += GpibAddress_SelectionChangeCommitted;

            ComPorts.SelectedIndexChanged += ComPorts_SelectedIndexChanged;
            BaudRate.SelectedIndexChanged += BaudRate_SelectedIndexChanged;

            Console.Command += Console_Command;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (ComPort.IsOpen) ComPort.Close();
            ComPort.Dispose();
        }

        void SelectPrologix_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectPrologix.Checked)
            {
                PrologixConfigFlowLeft.Show();
                IList<string> ports = Util.EnumerateSerialPorts();
                var last = ComPorts.SelectedItem;
                ComPorts.Items.Clear();
                foreach (var port in ports)
                {
                    ComPorts.Items.Add(port);
                }
                ComPorts.SelectedItem = last;
            }
            else
            {
                PrologixConfigFlowLeft.Hide();
            }
        }

        void GpibAddress_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SelectPrologix.Checked)
            {
                try
                {
                    if (!ComPort.IsOpen) ComPort.Open();
                    ControllerCommand(AddressCommand, GpibAddress.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    Console.AddMessage(ex.Message);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        void ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComPort();
        }

        void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComPort();
        }

        void Padding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)Padding.SelectedItem == "None")
            {
                UsePadding = false;
            }
            else
            {
                UsePadding = true;
                if ((string)Padding.SelectedItem == "Space")
                {
                    PadChar = ' ';
                }
                else if ((string)Padding.SelectedItem == "Null")
                {
                    PadChar = '\0';
                }
            }
        }

        private void InitComPort()
        {
            if (ComPort != null)
            {
                if (ComPort.IsOpen) ComPort.Close();
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
            ComPort.DtrEnable = true;

            // Error handling
            ComPort.DiscardNull = false;
            ComPort.ParityReplace = 0;
        }

        /// <summary>
        /// Read from addressed device until timeout reached between read characters.
        /// Note GPIB data is stored in 1 character buffer, then sent to 4K USB buffer.
        /// Thus there are two effective timeout required, one for reading GPIB and one
        /// for reading from the serial port (USB buffer).
        /// </summary>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        string ReadWithTimeout()
        {
            if (!ComPort.IsOpen) throw new InvalidOperationException("The specified port is not open");

            ControllerCommand(ReadTimeoutCommand, Timeout.Value.ToString());
            ControllerCommand(ReadCommand, "eoi"); // Read from GPIB until eoi or timeout.

            StringBuilder builder = new StringBuilder();
            DateTime lastRead = DateTime.Now;
            TimeSpan elapsedTime = new TimeSpan();

            // 2 second timespan
            TimeSpan TimeSpan = new TimeSpan(0, 0, 0, 0, (int)Timeout.Value);

            // Read from port until TIMEOUT time has elapsed since
            // last successful read
            while (TimeSpan.CompareTo(elapsedTime) > 0)
            {
                string buffer = ComPort.ReadExisting();

                if (buffer.Length > 0)
                {
                    builder.Append(buffer);
                    lastRead = DateTime.Now;
                }
                elapsedTime = DateTime.Now - lastRead;
            }
            return builder.ToString().Trim("\r\n\t ".ToCharArray());
        }

        /// <summary>
        /// Escapes GPIB command string for use with Prologix controller.
        /// CR (13), LF (10), ESC (27), + (43) characters will be escaped.
        /// </summary>
        /// <param name="s">GPIB command</param>
        /// <returns>Escaped string</returns>
        private string EscapeString(string s)
        {
            StringBuilder builder = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == (char)10 || s[i] == (char)13 || s[i] == (char)27 || s[i] == (char)43)
                {
                    builder.Append(ControllerEscape);
                }
                builder.Append(s[i]);
                if (UsePadding)
                {
                    builder.Append(PadChar); // Workaround for every-other-character problem.
                }
            }
            return builder.ToString();
        }

        private string EscapeAndTerminate(string s)
        {
            return EscapeString(s) + USBTerminator;
        }

        private void WriteAsciiBytes(string data)
        {
            ComPort.Write(AsciiEncoding.GetBytes(data), 0, AsciiEncoding.GetByteCount(data));
        }

        void ControllerCommand(string command)
        {
            string data = ControllerCommandInitiator + command + USBTerminator;
            WriteAsciiBytes(data);
        }

        void ControllerCommand(string command, params string[] args)
        {
            string arglist = String.Join(" ", args);
            string data = ControllerCommandInitiator + command + " " + arglist + USBTerminator;
            WriteAsciiBytes(data);
        }

        void IssuePrologixCommand(object sender, CommandPrompt.CommandEventArgs e)
        {
            try
            {
                if (!ComPort.IsOpen) ComPort.Open();

                if (e.Command.StartsWith("++"))
                {
                    WriteAsciiBytes(e.Command + USBTerminator);
                    string result = ReadWithTimeout();
                    e.Message = result;
                    if (e.Command == "++addr")
                    {
                        GpibAddress.SelectedItem = int.Parse(e.Message);
                    }
                }
                else
                {
                    WriteAsciiBytes(EscapeAndTerminate(e.Command + GpibTerminator));
                    string result = ReadWithTimeout();
                    e.Message = result;
                }
            }
            catch (Exception ex)
            {
                e.Message = ex.Message;
            }
        }

        void Console_Command(object sender, CommandPrompt.CommandEventArgs e)
        {
            if (SelectPrologix.Checked)
            {
                IssuePrologixCommand(sender, e);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            TestButton.Enabled = ConfigBox.Enabled = false;
            AbortButton.Enabled = true;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!ComPort.IsOpen) ComPort.Open();

            for (int i = 0; i < 1000; i++)
            {
                double Delay;

                if (i % 2 == 0)
                {
                    Delay = 1.5E-5D;
                }
                else
                {
                    Delay = 2.5E-3D;
                }

                string WriteDelayCommand = "DT 2,1," + Delay.ToString();

                WriteAsciiBytes(EscapeAndTerminate(WriteDelayCommand + GpibTerminator));

                string ReadDelayCommand = "DT 2";
                WriteAsciiBytes(ReadDelayCommand + GpibTerminator);

                string result = ReadWithTimeout();

                double DgDelay = Double.Parse(result.Split(',')[1]);

                string Report = Delay.ToString() + (Delay==DgDelay ? " == " : " != ") + DgDelay.ToString();

                worker.ReportProgress(0, Report);
            }

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.AddMessage(e.UserState as string);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TestButton.Enabled = ConfigBox.Enabled = true;
            AbortButton.Enabled = false;
        }

    }
}
