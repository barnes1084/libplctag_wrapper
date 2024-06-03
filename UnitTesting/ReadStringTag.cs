using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using PlcComm.DataTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadStringTag
    {
        public Tags.StringTag StringTag { get; private set; }
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
        public async Task ReadAnyStringTag()
        {
            await WriteString();
            //await ReadString("STRING", "TestSTRING", null, null);
            //await ReadString("STRING[]", "TestSTRINGArray", 2, 4);
        }

        public async Task ReadString(string dataType, string tagName, int? arrayIndex, int? arrayDimensions)
        {
            //StringTag = (Tags.StringTag)StringTag.DetermineDataType(dataType);
            StringTag = (Tags.StringTag)Tags.DetermineDataType(dataType);
            StringTag.Name = tagName;
            StringTag.Gateway = Gateway;
            StringTag.ProcessorSlotNumber = ProcessorSlotNumber;
            StringTag.PlcType = PlcType;
            StringTag.Protocol = Protocol;
            StringTag.Timeout = Timeout;
            StringTag.ArrayIndex = arrayIndex;
            StringTag.ArrayDimensions = arrayDimensions;
            StringTag.LibPlcTag = StringTag.CreateTag(StringTag);


            string value = await StringTag.ReadTagStringValue(StringTag);
            Console.WriteLine(value);

            Assert.IsTrue(value == "Test");
        }

        public async Task WriteString()
        {
            StringTag = (Tags.StringTag)Tags.DetermineDataType("STRING");
            StringTag.Name = "GyTireInfo.FTC";
            StringTag.Gateway = Gateway;
            StringTag.ProcessorSlotNumber = ProcessorSlotNumber;
            StringTag.PlcType = PlcType;
            StringTag.Protocol = Protocol;
            StringTag.Timeout = Timeout;
            StringTag.ArrayIndex = null;
            StringTag.ArrayDimensions = null;
            StringTag.LibPlcTag = StringTag.CreateTag(StringTag);

            await StringTag.WriteStringValue(StringTag, "123 456 789");
        }
    }
}