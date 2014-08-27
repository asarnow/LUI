using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PumpToggle
{
    public partial class Form1 : Form
    {

        public enum State { Open, Closed }

        public State CurrentState;
        private SerialPort _port;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.Open:
                    SetClosed();
                    break;
                case State.Closed:
                    SetOpen();
                    break;
            }
        }

        private void comPort_TextChanged(object sender, EventArgs e)
        {
            DisposePort();
            String portName = "COM" + comPort.Text;
            // _port = new SerialPort(portName) { RtsEnable = true };
            _port = new SerialPort(portName) { DtrEnable = true };
            SetClosed();
        }

        public void DisposePort()
        {
            SetClosed();
            _port.Dispose();
            _port = null;
        }

        public void SetOpen()
        {
            CurrentState = State.Open;
            button1.Text = "Close";
            _port.Open(); //TODO Which is which?
        }

        public void SetClosed()
        {
            CurrentState = State.Closed;
            button1.Text = "Open";
            _port.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String portName = "COM" + comPort.Text;
            // _port = new SerialPort(portName) { RtsEnable = true };
            _port = new SerialPort(portName) { DtrEnable = true };
            SetClosed();
        }

        private void Form1_Closed(Object sender, EventArgs e)
        {
            DisposePort();
        }
    }
}
