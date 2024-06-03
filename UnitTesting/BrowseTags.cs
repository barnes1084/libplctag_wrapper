using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class BrowseTags
    {
        private Tags.BrowseTags plc;

        [TestInitialize]
        public void Initialize()
        {
            plc = new Tags.BrowseTags();
        }


        [TestMethod]
        public async Task BrowsePlcTags()
        {
            var list = await plc.BrowseAsync("10.69.46.13", "3");

            Console.WriteLine("ID\tName\tType\tLength");
            foreach (var tag in list)
            {
                Console.WriteLine($"{tag.Id}\t{tag.Name}\t{tag.Type}\t{tag.Length}");
            }

        }
    }
}
