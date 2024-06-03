using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Real
    {
        /// <summary>
        /// Creates a real libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateRealTag(Tags.Tag tag)
        {
            Tag<RealPlcMapper, float> floatTag = new Tag<RealPlcMapper, float>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return floatTag;
        }

        /// <summary>
        /// Reads the float value of a REAL PLC tag.
        /// </summary>
        /// <param name="realTag">LibPlcTag string tag object</param>
        public static async Task<float> ReadRealTag(Tag<RealPlcMapper, float> realTag)
        {
            await realTag.ReadAsync();
            return realTag.Value;
        }

        /// <summary>
        /// Writes the float value of a REAL PLC tag.
        /// </summary>
        /// <param name="realTag">REAL plc tag</param>
        /// <param name="value">value to be written</param>
        /// <returns>value written</returns>
        public static async Task<float> WriteRealTag(Tag<RealPlcMapper, float> realTag, float value)
        {
            realTag.Value = value;
            await realTag.WriteAsync(value);
            return realTag.Value;
        }
    }
}
