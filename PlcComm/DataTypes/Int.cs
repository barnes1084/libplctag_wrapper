using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Int
    {
        /// <summary>
        /// Creates a int libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateIntTag(Tags.Tag tag)
        {
            Tag<IntPlcMapper, short> intTag = new Tag<IntPlcMapper, short>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return intTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a INT PLC tag.
        /// </summary>
        /// <param name="intTag">LibPlcTag int tag object</param>
        /// <param name="valueIndex">Index value of the BOOL in the INT tag that is desired</param>
        public static async Task<bool> ReadIntTag(Tag<IntPlcMapper, short> intTag, int valueIndex)
        {
            await intTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(intTag.Value, 16);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the short value of an INT PLC tag.
        /// </summary>
        /// <param name="intTag">LibPlcTag int tag object</param>
        public static async Task<short> ReadIntTag(Tag<IntPlcMapper, short> intTag)
        {
            await intTag.ReadAsync();
            return intTag.Value;
        }
    }
}
