
namespace lasercom.control
{
    public enum PumpState { Open, Closed }
    /// <summary>
    /// Defines the public operations supported by a pump.
    /// </summary>
    public interface IPump
    {
        PumpState CurrentState
        {
            get;
        }

        PumpState Toggle();

        void SetOpen();

        void SetClosed();

        PumpState GetState();

    }
}
