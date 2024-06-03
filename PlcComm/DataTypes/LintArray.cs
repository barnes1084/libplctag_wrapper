using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class LintArray
    {
        /// <summary>
        /// Creates a lint array libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateLintArrayTag(Tags.Tag tag)
        {
            Tag<LintPlcMapper, long[]> lintArrayTag = new Tag<LintPlcMapper, long[]>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                ArrayDimensions = new int[] { (int)tag.ArrayDimensions },
                Timeout = tag.Timeout
            };
            return lintArrayTag;
        }

        /// <summary>
        /// Reads the long value of a LINT contained within a LINT[] PLC tag.
        /// </summary>
        /// <param name="lintArrayTag">LibPlcTag lint tag object</param>
        /// <param name="arrayIndex">Index value of the LINT in the LINT[] tag that is desired</param>
        public static async Task<long> ReadLintArrayTag(Tag<LintPlcMapper, long[]> lintArrayTag, int arrayIndex)
        {
            await lintArrayTag.ReadAsync();
            return lintArrayTag.Value[arrayIndex];
        }

        /// <summary>
        /// Reads the long[] value of a LINT[] PLC tag.
        /// </summary>
        /// <param name="lintArrayTag">LibPlcTag lint tag object</param>
        public static async Task<long[]> ReadLintArrayTag(Tag<LintPlcMapper, long[]> lintArrayTag)
        {
            await lintArrayTag.ReadAsync();
            return lintArrayTag.Value;
        }
    }
}
