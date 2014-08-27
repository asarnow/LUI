using System;
using NationalInstruments.NI4882;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace LUI
{
    class DigitalDelayGenerator
    {
        public Address Address;
        private Device Device;

        public DigitalDelayGenerator(int boardNumber, int address)
        {
            Address = new Address((byte) address);
            Device = new Device(0, Address);
        }

        public void SetADelay(double delay)
        {
             
        }

        public void SetBDelay(double delay)
        {
            
        }

        public void SetCDelay(double delay)
        {
            
        }

        public void SetDDelay(double delay)
        {
            
        }

        public double GetADelay()
        {
            return 0D;
        }

        public double GetBDelay()
        {
            return 0D;
        }

        public double GetCDelay()
        {
            return 0D;
        }

        public double GetDDelay()
        {
            return 0D;
        }

    }
}
