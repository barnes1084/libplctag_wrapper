using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class StringArray
    {
        /// <summary>
        /// Creates a string array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateStringArrayTag(Tags.Tag tag)
        {
            Tag<StringPlcMapper, string[]> stringTag = new Tag<StringPlcMapper, string[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return stringTag;
        }

        /// <summary>
        /// Reads the string value of a STRING contained within a STRING[] PLC tag.
        /// </summary>
        /// <param name="stringArrayTag">LibPlcTag string tag object</param>
        /// <param name="arrayIndex">Index value of the STRING in the STRING[] tag that is desired</param>
        public static async Task<string> ReadStringArrayTag(Tag<StringPlcMapper, string[]> stringArrayTag, int arrayIndex)
        {
            await stringArrayTag.ReadAsync();
            return stringArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the string[] value of a STRING[] PLC tag.
        /// </summary>
        /// <param name="stringArrayTag">LibPlcTag string tag object</param>
        public static async Task<string[]> ReadStringArrayTag(Tag<StringPlcMapper, string[]> stringArrayTag)
        {
            await stringArrayTag.ReadAsync();
            return stringArrayTag.Value;
        }
    }
}
