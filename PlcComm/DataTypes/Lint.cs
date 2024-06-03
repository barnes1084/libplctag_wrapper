using libplctag;
using libplctag.DataTypes;

namespace PlcComm.DataTypes
{
    public class Lint
    {
        /// <summary>
        /// Creates a lint libplctag tag.
        /// </summary>
        /// <param name="tag">Tag object created by user</param>
        public static object CreateLintTag(Tags.Tag tag)
        {
            Tag<LintPlcMapper, long> lintTag = new Tag<LintPlcMapper, long>
            {
                Name = tag.Name,
                Gateway = tag.Gateway,
                Path = $"1,{tag.ProcessorSlotNumber}",
                PlcType = tag.PlcType,
                Protocol = tag.Protocol,
                Timeout = tag.Timeout
            };
            return lintTag;
        }

        /// <summary>
        /// Reads the long value of a LINT PLC tag.
        /// </summary>
        /// <param name="lintTag">LibPlcTag lint tag object</param>
        public static async Task<long> ReadLintTag(Tag<LintPlcMapper, long> lintTag)
        {
            await lintTag.ReadAsync();
            return lintTag.Value;
        }
    }
}
