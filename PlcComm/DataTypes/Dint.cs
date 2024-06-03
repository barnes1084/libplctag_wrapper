using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Dint
    {
        /// <summary>
        /// Creates a dint libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateDintTag(Tags.Tag tag)
        {
            Tag<DintPlcMapper, int> dintTag = new Tag<DintPlcMapper, int>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return dintTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a DINT PLC tag.
        /// </summary>
        /// <param name="dintTag">LibPlcTag dint tag object</param>
        /// <param name="valueIndex">Index value of the BOOL in the DINT tag that is desired</param>
        public static async Task<bool> ReadDintTag(Tag<DintPlcMapper, int> dintTag, int valueIndex)
        {
            await dintTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(dintTag.Value, 32);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the int value of a DINT PLC tag.
        /// </summary>
        /// <param name="dintTag">LibPlcTag dint tag object</param>
        public static async Task<int> ReadDintTag(Tag<DintPlcMapper, int> dintTag)
        {
            await dintTag.ReadAsync();
            return dintTag.Value;
        }

        /// <summary>
        /// Writes int value to a DINT PLC tag.
        /// </summary>
        /// <param name="dintTag">dint tag</param>
        /// <param name="value">value to be written</param>
        public static async Task WriteDintTag(Tag<DintPlcMapper, int> dintTag, int value)
        {
            await dintTag.WriteAsync(value);
        }
    }
}
