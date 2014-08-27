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
            CreatePort();
            SetClosed();
        }

        public void CreatePort()
        {
            String portName = "COM" + comPort.Text;
            if (rtsCheck.Checked && dtrCheck.Checked)
            {
                _port = new SerialPort(portName) { RtsEnable = true, DtrEnable = true };
            }
            else if (rtsCheck.Checked)
            {
                _port = new SerialPort(portName) { RtsEnable = true };
            }
            else if (dtrCheck.Checked)
            {
                _port = new SerialPort(portName) { DtrEnable = true };
            }
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
            toggleButton.Text = "Close";
            _port.Open(); //TODO Which is which?
        }

        public void SetClosed()
        {
            CurrentState = State.Closed;
            toggleButton.Text = "Open";
            _port.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreatePort();
            SetClosed();
        }

        private void Form1_Closed(Object sender, EventArgs e)
        {
            DisposePort();
        }

    }
}
