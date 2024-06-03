using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadRealArrayTag
    {
        public Tags.RealArrayTag RealArrayTag { get; private set; } = new Tags.RealArrayTag();
        public string Gateway { get; set; }
        public int ProcessorSlotNumber { get; set; }
        public TimeSpan Timeout { get; set; }
        public PlcType PlcType { get; set; }
        public Protocol Protocol { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Initialization initialization = new Initialization();
            Gateway = initialization.Gateway;
            ProcessorSlotNumber = initialization.ProcessorSlotNumber;
            Timeout = initialization.Timeout;
            PlcType = initialization.PlcTpye;
            Protocol = initialization.Protocol;
        }

        public string dataType = "REAL[]";
        public string tagName = "TestREALArray";
        public int arrayDimensions = 6;

        [TestMethod]
        public async Task ReadRealArray()
        {
            //RealArrayTag = (Tags.RealArrayTag)RealArrayTag.DetermineDataType(dataType);
            RealArrayTag.Name = tagName;
            RealArrayTag.Gateway = Gateway;
            RealArrayTag.ProcessorSlotNumber = ProcessorSlotNumber;
            RealArrayTag.PlcType = PlcType;
            RealArrayTag.Protocol = Protocol;
            RealArrayTag.Timeout = Timeout;
            RealArrayTag.ArrayDimensions = arrayDimensions;
            RealArrayTag.LibPlcTag = RealArrayTag.CreateTag(RealArrayTag);

            float[] value = await RealArrayTag.ReadTagRealArrayValue(RealArrayTag);
            value.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            //Assert.IsTrue(value == "Test");
        }
    }
}