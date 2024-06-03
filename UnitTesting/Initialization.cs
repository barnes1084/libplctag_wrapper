using System;
using libplctag;

namespace UnitTesting
{
    public class Initialization
    {
        public string Gateway { get; set; } = "10.69.46.64";
        public int ProcessorSlotNumber { get; set; } = 1;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
        public PlcType PlcTpye { get; set; } = PlcType.ControlLogix;
        public Protocol Protocol { get; set; } = Protocol.ab_eip;
    }
}
