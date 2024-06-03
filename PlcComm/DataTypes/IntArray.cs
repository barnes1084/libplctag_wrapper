using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class IntArray
    {
        /// <summary>
        /// Creates a int array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateIntArrayTag(Tags.Tag tag)
        {
            Tag<IntPlcMapper, short[]> intArrayTag = new Tag<IntPlcMapper, short[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return intArrayTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a INT[] PLC tag.
        /// </summary>
        /// <param name="intArrayTag">LibPlcTag int array tag object</param>
        /// <param name="arrayIndex">Index value of the INT in the INT[] tag that contains the desired BOOL</param>
        /// <param name="valueIndex">Index value of the BOOL in the INT tag that is desired</param>
        public static async Task<bool> ReadIntArrayTag(Tag<IntPlcMapper, short[]> intArrayTag, int arrayIndex, int valueIndex)
        {
            await intArrayTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(intArrayTag.Value[arrayIndex], 16);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the short value of an INT contained within a INT[] PLC tag.
        /// </summary>
        /// <param name="intArrayTag">LibPlcTag int array tag object</param>
        /// <param name="arrayIndex">Index value of the INT in the INT[] tag that is desired</param>
        public static async Task<short> ReadIntArrayTag(Tag<IntPlcMapper, short[]> intArrayTag, int arrayIndex)
        {
            await intArrayTag.ReadAsync();
            return intArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the short[] value of an INT[] PLC tag.
        /// </summary>
        /// <param name="intArrayTag">LibPlcTag int array tag object</param>
        public static async Task<short[]> ReadIntArrayTag(Tag<IntPlcMapper, short[]> intArrayTag)
        {
            await intArrayTag.ReadAsync();
            return intArrayTag.Value;
        }
    }
}
