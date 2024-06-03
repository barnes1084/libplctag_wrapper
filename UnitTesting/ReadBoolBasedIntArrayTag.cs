using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadBoolbasedIntArrayTag
    {
        public Tags.BoolBasedIntArrayTag IntArrayTag { get; private set; }
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
            await ReadIntArrayTag("SINT[]", "TestSINTArray", null, null, 3);
            await ReadIntArrayTag("INT[]", "TestINTArray", null, null, 5);
            await ReadIntArrayTag("DINT[]", "TestDINTArray", null, null, 2);
        }

        public async Task ReadIntArrayTag(string dataType, string tagName, int? valueIndex, int? arrayIndex, int? arrayDimensions)
        {
            //IntTag = (Tags.BoolBasedIntTag)IntTag.DetermineDataType(dataType);
            //PlcComm.Tags.DataTypeMap.TryGetValue(dataType, out Type dataTypeOut);
            //IntArrayTag = (Tags.BoolBasedIntArrayTag)Activator.CreateInstance(Type.GetType(dataTypeOut.AssemblyQualifiedName));
            IntArrayTag = (Tags.BoolBasedIntArrayTag)Tags.DetermineDataType(dataType);
            //IntArrayTag = (Tags.BoolBasedIntArrayTag)IntArrayTag.DetermineDataType(dataType);
            IntArrayTag.Name = tagName;
            IntArrayTag.Gateway = Gateway;
            IntArrayTag.ProcessorSlotNumber = ProcessorSlotNumber;
            IntArrayTag.PlcType = PlcType;
            IntArrayTag.Protocol = Protocol;
            IntArrayTag.Timeout = Timeout;
            IntArrayTag.ValueIndex = valueIndex;
            IntArrayTag.ArrayIndex = arrayIndex;
            IntArrayTag.ArrayDimensions = arrayDimensions;
            IntArrayTag.LibPlcTag = IntArrayTag.CreateTag(IntArrayTag);


            int[] value = await IntArrayTag.ReadTagIntArrayValue(IntArrayTag);
            value.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            //Assert.IsTrue(value);
        }
    }
}