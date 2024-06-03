using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadBoolbasedIntTag
    {
        public Tags.BoolBasedIntTag IntTag { get; private set; }
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
        public async Task ReadAnyIntTag()
        {
            await ReadIntTag("SINT", "TestSINT", null, null, null);
            await ReadIntTag("SINT[]", "TestSINTArray", null, 1, 3);
            await ReadIntTag("INT", "TestINT", null, null, null);
            await ReadIntTag("INT[]", "TestINTArray", null, 3, 5);
            await ReadIntTag("DINT", "TestDINT", null, null, null);
            await ReadIntTag("DINT[]", "TestDINTArray", null, 1, 2);
        }

        public async Task ReadIntTag(string dataType, string tagName, int? valueIndex, int? arrayIndex, int? arrayDimensions)
        {
            //IntTag = (Tags.BoolBasedIntTag)IntTag.DetermineDataType(dataType);
            PlcComm.Tags.DataTypeMap.TryGetValue(dataType, out Type dataTypeOut);
            IntTag = (Tags.BoolBasedIntTag)Activator.CreateInstance(Type.GetType(dataTypeOut.AssemblyQualifiedName));
            IntTag.Name = tagName;
            IntTag.Gateway = Gateway;
            IntTag.ProcessorSlotNumber = ProcessorSlotNumber;
            IntTag.PlcType = PlcType;
            IntTag.Protocol = Protocol;
            IntTag.Timeout = Timeout;
            IntTag.ValueIndex = valueIndex;
            IntTag.ArrayIndex = arrayIndex;
            IntTag.ArrayDimensions = arrayDimensions;
            IntTag.LibPlcTag = IntTag.CreateTag(IntTag);


            int value = await IntTag.ReadTagIntValue(IntTag);
            Console.WriteLine(value);

            //Assert.IsTrue(value);
        }
    }
}