using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadRealTag
    {
        public Tags.RealTag RealTag { get; private set; } = new Tags.RealTag();
        public string Gateway { get; set; }
        public int ProcessorSlotNumber { get; set; }
        public TimeSpan Timeout { get; set; }
        public PlcType PlcType { get; set; }
        public Protocol Protocol { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Initialization initialization = new Initialization();
            Gateway = "10.69.46.13";
            ProcessorSlotNumber = 3;
            Timeout = initialization.Timeout;
            PlcType = initialization.PlcTpye;
            Protocol = initialization.Protocol;
        }

        [TestMethod]
        public async Task ReadAnyRealTag()
        {
            await ReadReal("REAL", "RealTestTag", null, null);
            //await ReadReal("REAL[]", "TestREALArray", 4, 6);
        }

        public async Task ReadReal(string dataType, string tagName, int? arrayIndex, int? arrayDimensions)
        {
            //RealTag = (Tags.RealTag)RealTag.DetermineDataType(dataType);
            RealTag = (Tags.RealTag)Tags.DetermineDataType(dataType);
            RealTag.Name = tagName;
            RealTag.Gateway = Gateway;
            RealTag.ProcessorSlotNumber = ProcessorSlotNumber;
            RealTag.PlcType = PlcType;
            RealTag.Protocol = Protocol;
            RealTag.Timeout = Timeout;
            RealTag.ArrayIndex = arrayIndex;
            RealTag.ArrayDimensions = arrayDimensions;
            RealTag.LibPlcTag = RealTag.CreateTag(RealTag);


            //float value = await RealTag.ReadTagRealValue(RealTag);
            //Console.WriteLine(value);

            await RealTag.WriteTagRealValue(RealTag, 123.4f);

            //Assert.IsTrue(value == 123.4f);
        }
    }
}