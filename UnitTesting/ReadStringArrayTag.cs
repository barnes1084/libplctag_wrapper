using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadStringArrayTag
    {
        public Tags.StringArrayTag StringArrayTag { get; private set; } = new Tags.StringArrayTag();
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

        public string dataType = "STRING[]";
        public string tagName = "TestSTRINGArray";
        public int arrayDimensions = 4;

        [TestMethod]
        public async Task ReadStringArray()
        {
            //StringArrayTag = (Tags.StringArrayTag)StringArrayTag.DetermineDataType(dataType);
            StringArrayTag.Name = tagName;
            StringArrayTag.Gateway = Gateway;
            StringArrayTag.ProcessorSlotNumber = ProcessorSlotNumber;
            StringArrayTag.PlcType = PlcType;
            StringArrayTag.Protocol = Protocol;
            StringArrayTag.Timeout = Timeout;
            StringArrayTag.ArrayDimensions = arrayDimensions;
            StringArrayTag.LibPlcTag = StringArrayTag.CreateTag(StringArrayTag);

            string[] value = await StringArrayTag.ReadTagStringArrayValue(StringArrayTag);
            value.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            //Assert.IsTrue(value == "Test");
        }
    }
}