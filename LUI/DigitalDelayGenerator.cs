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
        private readonly Board _board;

        public DigitalDelayGenerator(int address, Board board)
        {
            Address = new Address((byte) address);
            _board = board;
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
