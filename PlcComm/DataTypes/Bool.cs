using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Bool
    {
        /// <summary>
        /// Creates a bool libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateBoolTag(Tags.Tag tag)
        {
            Tag<BoolPlcMapper, bool> boolTag = new Tag<BoolPlcMapper, bool>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return boolTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL PLC tag.
        /// </summary>
        /// <param name="boolTag">LibPlcTag bool tag object</param>
        public static async Task<bool> ReadBoolTag(Tag<BoolPlcMapper, bool> boolTag)
        {
            await boolTag.ReadAsync();
            return boolTag.Value;
        }
    }
}
