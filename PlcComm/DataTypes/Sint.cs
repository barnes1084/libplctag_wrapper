using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Sint
    {
        /// <summary>
        /// Creates a sint libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateSintTag(Tags.Tag tag)
        {
            Tag<SintPlcMapper, sbyte> sintTag = new Tag<SintPlcMapper, sbyte>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return sintTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a SINT PLC tag.
        /// </summary>
        /// <param name="sintTag">LibPlcTag sint tag object</param>
        /// <param name="valueIndex">Index value of the BOOL in the SINT tag that is desired</param>
        public static async Task<bool> ReadSintTag(Tag<SintPlcMapper, sbyte> sintTag, int valueIndex)
        {
            await sintTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(sintTag.Value, 8);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the sbyte value of a SINT PLC tag.
        /// </summary>
        /// <param name="sintTag">LibPlcTag sint tag object</param>
        public static async Task<sbyte> ReadSintTag(Tag<SintPlcMapper, sbyte> sintTag)
        {
            await sintTag.ReadAsync();
            return sintTag.Value;
        }
    }
}
