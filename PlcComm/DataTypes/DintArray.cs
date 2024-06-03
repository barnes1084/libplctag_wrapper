using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class DintArray
    {
        /// <summary>
        /// Creates a dint array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateDintArrayTag(Tags.Tag tag)
        {
            Tag<DintPlcMapper, int[]> dintArrayTag = new Tag<DintPlcMapper, int[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return dintArrayTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a DINT[] PLC tag.
        /// </summary>
        /// <param name="dintArrayTag">LibPlcTag dint array tag object</param>
        /// <param name="arrayIndex">Index value of the DINT in the DINT[] tag that contains the desired BOOL</param>
        /// <param name="valueIndex">Index value of the BOOL in the DINT tag that is desired</param>
        public static async Task<bool> ReadDintArrayTag(Tag<DintPlcMapper, int[]> dintArrayTag, int arrayIndex, int valueIndex)
        {
            await dintArrayTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(dintArrayTag.Value[arrayIndex], 32);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the int value of a DINT contained within a DINT[] PLC tag.
        /// </summary>
        /// <param name="dintArrayTag">LibPlcTag dint array tag object</param>
        /// <param name="arrayIndex">Index value of the DINT in the DINT[] tag that is desired</param>
        public static async Task<int> ReadDintArrayTag(Tag<DintPlcMapper, int[]> dintArrayTag, int arrayIndex)
        {
            await dintArrayTag.ReadAsync();
            return dintArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the int[] value of a DINT[] PLC tag.
        /// </summary>
        /// <param name="dintArrayTag">LibPlcTag dint array tag object</param>
        public static async Task<int[]> ReadDintArrayTag(Tag<DintPlcMapper, int[]> dintArrayTag)
        {
            await dintArrayTag.ReadAsync();
            return dintArrayTag.Value;
        }
    }
}
