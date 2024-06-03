using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class RealArray
    {
        /// <summary>
        /// Creates a real array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateRealArrayTag(Tags.Tag tag)
        {
            Tag<RealPlcMapper, float[]> realTag = new Tag<RealPlcMapper, float[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return realTag;
        }

        /// <summary>
        /// Reads the float value of a REAL contained within a REAL[] PLC tag.
        /// </summary>
        /// <param name="realArrayTag">LibPlcTag string tag object</param>
        /// <param name="arrayIndex">Index value of the REAL in the REAL[] tag that is desired</param>
        public static async Task<float> ReadRealArrayTag(Tag<RealPlcMapper, float[]> realArrayTag, int arrayIndex)
        {
            await realArrayTag.ReadAsync();
            return realArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the float[] value of a REAL[] PLC tag.
        /// </summary>
        /// <param name="realArrayTag">LibPlcTag string tag object</param>
        public static async Task<float[]> ReadRealArrayTag(Tag<RealPlcMapper, float[]> realArrayTag)
        {
            await realArrayTag.ReadAsync();
            return realArrayTag.Value;
        }
    }
}
