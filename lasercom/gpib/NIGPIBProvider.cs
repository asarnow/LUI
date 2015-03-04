using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NationalInstruments.NI4882;

namespace LUI.gpib
{
    class NIGPIBProvider : GPIBProvider
    {

        public Board Board { get; set; }
        //public Address Address { get; set; }
        //public Device Device { get; set; }

        public NIGPIBProvider(int _BoardNumber)
        {
            Board = new Board(_BoardNumber);
            Board.BecomeActiveController(true);
            AddressCollection Addresses = Board.FindListeners();

            //Address = new Address((byte)_Address);
            //Device = new Device(_BoardNumber, Address);
        }

        override protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Board.Dispose();
            }
        }

        override public void LoggedWrite(byte address, string command)
        {
            Log.Debug("GPIB Command: " + command);
            try
            {
                Board.Write(new Address(address), command);
                //Device.Write(command);
            }
            catch (GpibException e)
            {
                Log.Error(e.Message);
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e.InnerException.Message);
            }

        }

        override public string LoggedQuery(byte address, string command)
        {
            Log.Debug("GPIB Command: " + command);
            try
            {
                Board.Write(new Address(address), command);
                //Device.Write(command);
            }
            catch (GpibException e)
            {
                Log.Error(e.Message);
                return null;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e.InnerException.Message);
                return null;
            }
            //string response = Device.ReadString();
            string response = Board.ReadString(new Address(address));
            Log.Debug("GPIB Response: " + response);
            return response;
        }

    }
}
