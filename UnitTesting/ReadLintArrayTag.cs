using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadLintArrayTag
    {
        public Tags.LintArrayTag LintArrayTag { get; private set; } = new Tags.LintArrayTag();
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

        public string dataType = "LINT[]";
        public string tagName = "TestLINTArray";
        public int arrayDimensions = 5;

        [TestMethod]
        public async Task ReadLintArray()
        {
            //LintArrayTag = (Tags.LintArrayTag)LintArrayTag.DetermineDataType(dataType);
            LintArrayTag.Name = tagName;
            LintArrayTag.Gateway = Gateway;
            LintArrayTag.ProcessorSlotNumber = ProcessorSlotNumber;
            LintArrayTag.PlcType = PlcType;
            LintArrayTag.Protocol = Protocol;
            LintArrayTag.Timeout = Timeout;
            LintArrayTag.ArrayDimensions = arrayDimensions;
            LintArrayTag.LibPlcTag = LintArrayTag.CreateTag(LintArrayTag);

            long[] value = await LintArrayTag.ReadTagLintArrayValue(LintArrayTag);
            value.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            //Assert.IsTrue(value == "Test");
        }
    }
}