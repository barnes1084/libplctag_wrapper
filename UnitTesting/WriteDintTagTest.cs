using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Threading.Tasks;
using static PlcComm.Tags;

namespace UnitTesting
{
    [TestClass]
    public class WriteDintTagTest
    {
        public DintTag DintTag { get; private set; }
        public string Gateway { get; set; }
        public int ProcessorSlotNumber { get; set; }
        public TimeSpan Timeout { get; set; }
        public PlcType PlcType { get; set; }
        public Protocol Protocol { get; set; }

        [TestInitialize]
        public void Init()
        {
            Initialization initialization = new Initialization();
            Gateway = "10.69.46.13";
            ProcessorSlotNumber = 3;
            Timeout = initialization.Timeout;
            PlcType = initialization.PlcTpye;
            Protocol = initialization.Protocol;
        }

        [TestMethod]
        public async Task ReadAnyIntTag()
        {
            //await ReadIntTag("SINT", "TestSINT", null, null, null);
            //await ReadIntTag("SINT[]", "TestSINTArray", null, 1, 3);
            //await ReadIntTag("INT", "TestINT", null, null, null);
            //await ReadIntTag("INT[]", "TestINTArray", null, 3, 5);
            //await ReadIntTag("DINT", "TestDINT", null, null, null);
            //await ReadIntTag("DINT[]", "TestDINTArray", null, 1, 2);

            await WriteDintTag("DINT", "NicksTestTag", null, null, null);
        }

        public async Task WriteDintTag(string dataType, string tagName, int? valueIndex, int? arrayIndex, int? arrayDimensions)
        {
            //IntTag = (Tags.BoolBasedIntTag)IntTag.DetermineDataType(dataType);
            PlcComm.Tags.DataTypeMap.TryGetValue(dataType, out Type dataTypeOut);
            DintTag = (Tags.DintTag)Activator.CreateInstance(Type.GetType(dataTypeOut.AssemblyQualifiedName));
            DintTag.Name = tagName;
            DintTag.Gateway = Gateway;
            DintTag.ProcessorSlotNumber = ProcessorSlotNumber;
            DintTag.PlcType = PlcType;
            DintTag.Protocol = Protocol;
            DintTag.Timeout = Timeout;
            DintTag.ValueIndex = valueIndex;
            DintTag.ArrayIndex = arrayIndex;
            DintTag.ArrayDimensions = arrayDimensions;
            DintTag.LibPlcTag = DintTag.CreateTag(DintTag);


            int value = 35;
            await DintTag.WriteDintTag(DintTag, value);
            Console.WriteLine(value);

            //Assert.IsTrue(value);
        }
    }
}
