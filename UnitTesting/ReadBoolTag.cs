using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadBoolTag
    {
        public Tags.BoolTag BoolTag { get; private set; } = new Tags.BoolTag();
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

        [TestMethod]
        public async Task ReadAnyBoolTag()
        {
            await ReadBool("BOOL", "TestBOOL", null, null, null);
            await ReadBool("SINT", "TestSINT", 2, null, null);
            await ReadBool("SINT[]", "TestSINTArray", 6, 1, 3);
            await ReadBool("INT", "TestINT", 12, null, null);
            await ReadBool("INT[]", "TestINTArray", 3, 3, 5);
            await ReadBool("DINT", "TestDINT", 15, null, null);
            await ReadBool("DINT[]", "TestDINTArray", 4, 1, 2);
        }

        public async Task ReadBool(string dataType, string tagName, int? valueIndex, int? arrayIndex, int? arrayDimensions)
        {
            //BoolTag = (Tags.BoolTag)BoolTag.DetermineDataType(dataType);
            BoolTag = (Tags.BoolTag)Tags.DetermineDataType(dataType);
            BoolTag.Name = tagName;
            BoolTag.Gateway = Gateway;
            BoolTag.ProcessorSlotNumber = ProcessorSlotNumber;
            BoolTag.PlcType = PlcType;
            BoolTag.Protocol = Protocol;
            BoolTag.Timeout = Timeout;
            BoolTag.ValueIndex = valueIndex;
            BoolTag.ArrayIndex = arrayIndex;
            BoolTag.ArrayDimensions = arrayDimensions;
            BoolTag.LibPlcTag = BoolTag.CreateTag(BoolTag);


            bool value = await BoolTag.ReadTagBoolValue(BoolTag);
            Console.WriteLine(value);

            Assert.IsTrue(value);
        }
    }
}