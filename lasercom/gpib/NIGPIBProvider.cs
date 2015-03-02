using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NationalInstruments.NI4882;

namespace LUI.gpib
{
    class NIGPIBProvider : IGPIBProvider
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Board Board { get; set; }
        public Address Address { get; set; }
        public Device Device { get; set; }

        public NIGPIBProvider(int _Address, int _BoardNumber)
        {
            Board = new Board(_BoardNumber);
            Board.BecomeActiveController(true);
            AddressCollection Addresses = Board.FindListeners();
            


            Address = new Address((byte)_Address);
            Device = new Device(_BoardNumber, Address);
        }

        public void LoggedWrite(string command)
        {
            Log.Debug("GPIB Command: " + command);
            try
            {
                Device.Write(command);
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

        public string LoggedQuery(string command)
        {
            Log.Debug("GPIB Command: " + command);

            try
            {
                Device.Write(command);
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

            string response = Device.ReadString();
            Log.Debug("GPIB Response: " + response);
            return response;
        }

    }
}
