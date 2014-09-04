using System;
using NationalInstruments.NI4882;
using log4net;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace LUI
{
    public abstract class AbstractDigitalDelayGenerator:IDigitalDelayGenerator
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private Address Address {public get; private set; }
        private Device Device { private get; private set; }

        public AbstractDigitalDelayGenerator(int address, int boardNumber = Constants.BoardNumber)
        {
            Address = new Address((byte)address);
            Device = new Device(boardNumber, Address);
        }

        //public AbstractDigitalDelayGenerator(int address)
        //{
        //    Address = new Address((byte) address);
        //    Device = new Device(Constants.BoardNumber, Address);
        //}

        public void LoggedWrite(string command)
        {
            log.Info("GPIB Command: " + command);
            try
            {
                Device.Write(command);
            }
            catch (GpibException e)
            {
                log.Error(e.Message);
            }
            catch (InvalidOperationException e)
            {
                log.Error(e.InnerException.Message);
            }
            
        }

        public string LoggedWriteRead(string command)
        {
            log.Info("GPIB Command: " + command);

            try
            {
                Device.Write(command);
            }
            catch (GpibException e)
            {
                log.Error(e.Message);
                return null;
            }
            catch (InvalidOperationException e)
            {
                log.Error(e.InnerException.Message);
                return null;
            }

            string response = Device.ReadString();
            log.Info("GPIB Response: " + response);
            return response;
        }

    }
}
