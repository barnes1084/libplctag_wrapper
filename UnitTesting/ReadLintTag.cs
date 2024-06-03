using libplctag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ReadLintTag
    {
        public Tags.LintTag LintTag { get; private set; } = new Tags.LintTag();
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
        public async Task ReadAnyLintTag()
        {
            await ReadLint("LINT", "TestLINT", null, null);
            await ReadLint("LINT[]", "TestLINTArray", 1, 5);
        }

        public async Task ReadLint(string dataType, string tagName, int? arrayIndex, int? arrayDimensions)
        {
            //LintTag = (Tags.LintTag)LintTag.DetermineDataType(dataType);
            LintTag = (Tags.LintTag)Tags.DetermineDataType(dataType);
            LintTag.Name = tagName;
            LintTag.Gateway = Gateway;
            LintTag.ProcessorSlotNumber = ProcessorSlotNumber;
            LintTag.PlcType = PlcType;
            LintTag.Protocol = Protocol;
            LintTag.Timeout = Timeout;
            LintTag.ArrayIndex = arrayIndex;
            LintTag.ArrayDimensions = arrayDimensions;
            LintTag.LibPlcTag = LintTag.CreateTag(LintTag);


            long value = await LintTag.ReadTagLintValue(LintTag);
            Console.WriteLine(value);

            Assert.IsTrue(value == 10000);
        }
    }
}