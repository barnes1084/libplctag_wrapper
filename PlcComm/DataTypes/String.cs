using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class String
    {
        /// <summary>
        /// Creates a string libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateStringTag(Tags.Tag tag)
        {
            Tag<StringPlcMapper, string> stringTag = new Tag<StringPlcMapper, string>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return stringTag;
        }

        /// <summary>
        /// Reads the string value of a STRING PLC tag.
        /// </summary>
        /// <param name="stringTag">LibPlcTag string tag object</param>
        public static async Task<string> ReadStringTag(Tag<StringPlcMapper, string> stringTag)
        {
            await stringTag.ReadAsync();
            return stringTag.Value;
        }

        /// <summary>
        /// Writes string to a STRING PLC tag.
        /// </summary>
        /// <param name="stringTag">String plc tag</param>
        /// <param name="value">value</param>
        /// <returns>task</returns>
        public static async Task WriteStringTag(Tag<StringPlcMapper, string> stringTag, string value)
        {
            await stringTag.WriteAsync(value);
        }
    }
}
