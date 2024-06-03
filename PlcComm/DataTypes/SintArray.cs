using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class SintArray
    {
        /// <summary>
        /// Creates a sint array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateSintArrayTag(Tags.Tag tag)
        {
            Tag<SintPlcMapper, sbyte[]> sintArrayTag = new Tag<SintPlcMapper, sbyte[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return sintArrayTag;
        }

        /// <summary>
        /// Reads the boolean value of a BOOL contained within a SINT[] PLC tag.
        /// </summary>
        /// <param name="sintArrayTag">LibPlcTag sint array tag object</param>
        /// <param name="arrayIndex">Index value of the SINT in the SINT[] tag that contains the desired BOOL</param>
        /// <param name="valueIndex">Index value of the BOOL in the SINT tag that is desired</param>
        public static async Task<bool> ReadSintArrayTag(Tag<SintPlcMapper, sbyte[]> sintArrayTag, int arrayIndex, int valueIndex)
        {
            await sintArrayTag.ReadAsync();

            List<bool> intAsListOfBools = Common.TypeConversions.IntToBinaryListOfBoolsConverter(sintArrayTag.Value[arrayIndex], 8);

            return intAsListOfBools[valueIndex];
        }

        /// <summary>
        /// Reads the sbyte value of a SINT contained within a SINT[] PLC tag.
        /// </summary>
        /// <param name="sintArrayTag">LibPlcTag sint array tag object</param>
        /// <param name="arrayIndex">Index value of the SINT in the SINT[] tag that is desired</param>
        public static async Task<sbyte> ReadSintArrayTag(Tag<SintPlcMapper, sbyte[]> sintArrayTag, int arrayIndex)
        {
            await sintArrayTag.ReadAsync();
            return sintArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the sbyte[] value of a SINT[] PLC tag.
        /// </summary>
        /// <param name="sintArrayTag">LibPlcTag sint array tag object</param>
        public static async Task<sbyte[]> ReadSintArrayTag(Tag<SintPlcMapper, sbyte[]> sintArrayTag)
        {
            await sintArrayTag.ReadAsync();
            return sintArrayTag.Value;
        }
    }
}
