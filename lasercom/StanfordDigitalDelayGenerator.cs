using System;
using NationalInstruments.NI4882;
using log4net;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace LUI
{
    public abstract class StanfordDigitalDelayGenerator:IDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public Address Address {get; set; }
        public Device Device { get; set; }

        

        public StanfordDigitalDelayGenerator(int address, int boardNumber = Constants.BoardNumber)
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

        public string LoggedWriteRead(string command)
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



        public void SetADelay(double delay)
        {
            throw new NotImplementedException();
        }

        public void SetBDelay(double delay)
        {
            throw new NotImplementedException();
        }

        public void SetCDelay(double delay)
        {
            throw new NotImplementedException();
        }

        public void SetDDelay(double delay)
        {
            throw new NotImplementedException();
        }

        public double GetADelay()
        {
            throw new NotImplementedException();
        }

        public double GetBDelay()
        {
            throw new NotImplementedException();
        }

        public double GetCDelay()
        {
            throw new NotImplementedException();
        }

        public double GetDDelay()
        {
            throw new NotImplementedException();
        }
    }
}
