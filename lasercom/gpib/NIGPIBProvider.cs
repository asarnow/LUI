using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NationalInstruments.NI4882;

namespace lasercom.gpib
{
    /// <summary>
    /// Provide GPIB using NI 488.2 PCI card.
    /// </summary>
    public class NIGpibProvider : AbstractGpibProvider
    {
        const string GpibTerminator = "\r\n";

        public Board Board { get; set; }

        public NIGpibProvider(int _BoardNumber)
        {
            Board = new Board(_BoardNumber);
            Board.SendInterfaceClear();
            Board.BecomeActiveController(true);
            Board.SetRemoteEnableLine();
            Board.SetEndOnWrite = true; // Assert EOI on write.
            Board.SetEndOnEndOfString = false;
            Board.IOTimeout = TimeoutValue.T100ms;
            Board.EndOfStringCharacter = 13; // CR
            Board.TerminateReadOnEndOfString = true;
            //AddressCollection Addresses = Board.FindListeners();
        }

        protected override void Dispose(bool disposing)
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
                Board.Write(new Address(address), command + GpibTerminator);
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
            string response = null;
            try
            {
                Board.Write(new Address(address), command + GpibTerminator);
                response = Board.ReadString(new Address(address));
                response = response.TrimEnd(GpibTerminator.ToCharArray());
            }
            catch (GpibException ex)
            {
                Log.Error(ex);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            Log.Debug("GPIB Response: " + response);
            return response;
        }

    }
}
