using libplctag;
using libplctag.DataTypes;
using PlcComm.DataTypes;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using static PlcComm.Tags;

namespace PlcComm
{
    public class Tags
    {
        public static Dictionary<string, Type> DataTypeMap = new Dictionary<string, Type>()
        {
            { "BOOL", typeof(BoolTag) },
            { "SINT", typeof(SintTag) },
            { "SINT[]", typeof(SintArrayTag) },
            { "INT", typeof(IntTag) },
            { "INT[]", typeof(IntArrayTag) },
            { "DINT", typeof(DintTag) },
            { "DINT[]", typeof(DintArrayTag) },
            { "LINT", typeof(LintTag) },
            { "LINT[]", typeof(LintArrayTag) },
            { "STRING", typeof(StringTag) },
            { "STRING[]", typeof(StringArrayTag) },
            { "REAL", typeof(RealTag) },
            { "REAL[]", typeof(RealArrayTag) }
        };

        public static object DetermineDataType(string dataTypeString)
        {
            try
            {
                DataTypeMap.TryGetValue(dataTypeString, out Type dataType);

                return Activator.CreateInstance(Type.GetType(dataType.AssemblyQualifiedName));
            }
            catch
            {
                Console.WriteLine("DataType input is invalid. ");
                return null;
            }
        }

        public abstract class Tag
        {
            public string Name { get; set; }
            public string Gateway { get; set; }
            public int ProcessorSlotNumber { get; set; }
            public PlcType PlcType { get; set; }
            public Protocol Protocol { get; set; }
            public int? ArrayDimensions { get; set; }
            public TimeSpan Timeout { get; set; }
            public int? ArrayIndex { get; set; }
            public int? ValueIndex { get; set; }
            public object LibPlcTag { get; set; }
            public abstract object CreateTag(Tag tag);
        }

        public class BoolTag : Tag
        {
            public override object CreateTag(Tag tag)
            {
                return Bool.CreateBoolTag(tag);
            }
            public virtual async Task<bool> ReadTagBoolValue(Tag boolTag)
            {
                return await Bool.ReadBoolTag((Tag<BoolPlcMapper, bool>)boolTag.LibPlcTag);
            }
        }

        public abstract class BoolBasedIntTag : BoolTag
        {
            public abstract Task<int> ReadTagIntValue(Tag tag);
        }

        public abstract class BoolBasedIntArrayTag : BoolBasedIntTag
        {
            public abstract Task<int[]> ReadTagIntArrayValue(Tag tag);
        }

        public class SintTag : BoolBasedIntTag
        {
            public override object CreateTag(Tag tag)
            {
                return Sint.CreateSintTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag sintTag)
            {
                return await Sint.ReadSintTag((Tag<SintPlcMapper, sbyte>)sintTag.LibPlcTag, (int)sintTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag sintTag)
            {
                return await Sint.ReadSintTag((Tag<SintPlcMapper, sbyte>)sintTag.LibPlcTag);
            }
        }

        public class SintArrayTag : BoolBasedIntArrayTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.SintArray.CreateSintArrayTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag sintArrayTag)
            {
                return await SintArray.ReadSintArrayTag((Tag<SintPlcMapper, sbyte[]>)sintArrayTag.LibPlcTag,
                    (int)sintArrayTag.ArrayIndex, (int)sintArrayTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag sintArrayTag)
            {
                return await SintArray.ReadSintArrayTag((Tag<SintPlcMapper, sbyte[]>)sintArrayTag.LibPlcTag,
                    (int)sintArrayTag.ArrayIndex);
            }
            public override async Task<int[]> ReadTagIntArrayValue(Tag sintTag)
            {
                return (await SintArray.ReadSintArrayTag((Tag<SintPlcMapper, sbyte[]>)sintTag.LibPlcTag)).Select(x => (int)x).ToArray();
            }
        }

        public class IntTag : BoolBasedIntTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.Int.CreateIntTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag intTag)
            {
                return await Int.ReadIntTag((Tag<IntPlcMapper, short>)intTag.LibPlcTag, (int)intTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag intTag)
            {
                return await Int.ReadIntTag((Tag<IntPlcMapper, short>)intTag.LibPlcTag);
            }
        }

        public class IntArrayTag : BoolBasedIntArrayTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.IntArray.CreateIntArrayTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag intArrayTag)
            {
                return await IntArray.ReadIntArrayTag((Tag<IntPlcMapper, short[]>)intArrayTag.LibPlcTag,
                    (int)intArrayTag.ArrayIndex, (int)intArrayTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag intArrayTag)
            {
                return await IntArray.ReadIntArrayTag((Tag<IntPlcMapper, short[]>)intArrayTag.LibPlcTag,
                    (int)intArrayTag.ArrayIndex);
            }
            public override async Task<int[]> ReadTagIntArrayValue(Tag intTag)
            {
                return (await IntArray.ReadIntArrayTag((Tag<IntPlcMapper, short[]>)intTag.LibPlcTag)).Select(x => (int)x).ToArray();
            }
        }

        public class DintTag : BoolBasedIntTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.Dint.CreateDintTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag dintTag)
            {
                return await Dint.ReadDintTag((Tag<DintPlcMapper, int>)dintTag.LibPlcTag, (int)dintTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag dintTag)
            {
                return await Dint.ReadDintTag((Tag<DintPlcMapper, int>)dintTag.LibPlcTag);
            }
            public async Task WriteDintTag(Tag dintTag, int value)
            {
                await Dint.WriteDintTag((Tag<DintPlcMapper, int>)dintTag.LibPlcTag, value);
            }
        }

        public class DintArrayTag : BoolBasedIntArrayTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.DintArray.CreateDintArrayTag(tag);
            }
            public override async Task<bool> ReadTagBoolValue(Tag dintArrayTag)
            {
                return await DintArray.ReadDintArrayTag((Tag<DintPlcMapper, int[]>)dintArrayTag.LibPlcTag,
                    (int)dintArrayTag.ArrayIndex, (int)dintArrayTag.ValueIndex);
            }
            public override async Task<int> ReadTagIntValue(Tag dintArrayTag)
            {
                return await DintArray.ReadDintArrayTag((Tag<DintPlcMapper, int[]>)dintArrayTag.LibPlcTag,
                    (int)dintArrayTag.ArrayIndex);
            }
            public override async Task<int[]> ReadTagIntArrayValue(Tag dintTag)
            {
                return await DintArray.ReadDintArrayTag((Tag<DintPlcMapper, int[]>)dintTag.LibPlcTag);
            }
        }

        public class LintTag : Tag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.Lint.CreateLintTag(tag);
            }
            public virtual async Task<long> ReadTagLintValue(Tag lintTag)
            {
                return await Lint.ReadLintTag((Tag<LintPlcMapper, long>)lintTag.LibPlcTag);
            }
        }

        public class LintArrayTag : LintTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.LintArray.CreateLintArrayTag(tag);
            }
            public override async Task<long> ReadTagLintValue(Tag lintArrayTag)
            {
                return await LintArray.ReadLintArrayTag((Tag<LintPlcMapper, long[]>)lintArrayTag.LibPlcTag, 
                    (int)lintArrayTag.ArrayIndex);
            }
            public async Task<long[]> ReadTagLintArrayValue(Tag lintArrayTag)
            {
                return await LintArray.ReadLintArrayTag((Tag<LintPlcMapper, long[]>)lintArrayTag.LibPlcTag);
            }
        }

        public class StringTag : Tag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.String.CreateStringTag(tag);
            }
            public virtual async Task<string> ReadTagStringValue(Tag stringTag)
            {
                return await DataTypes.String.ReadStringTag((Tag<StringPlcMapper, string>)stringTag.LibPlcTag);
            }
            public virtual async Task WriteStringValue(Tag stringTag, string value)
            {
                await DataTypes.String.WriteStringTag((Tag<StringPlcMapper, string>)stringTag.LibPlcTag, value);
            }
        }

        public class StringArrayTag : StringTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.StringArray.CreateStringArrayTag(tag);
            }
            public override async Task<string> ReadTagStringValue(Tag stringArrayTag)
            {
                return await StringArray.ReadStringArrayTag((Tag<StringPlcMapper, string[]>)stringArrayTag.LibPlcTag, 
                    (int)stringArrayTag.ArrayIndex);
            }
            public async Task<string[]> ReadTagStringArrayValue(Tag stringArrayTag)
            {
                return await StringArray.ReadStringArrayTag((Tag<StringPlcMapper, string[]>)stringArrayTag.LibPlcTag);
            }
        }

        public class RealTag : Tag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.Real.CreateRealTag(tag);
            }
            public virtual async Task<float> ReadTagRealValue(Tag realTag)
            {
                return await Real.ReadRealTag((Tag<RealPlcMapper, float>)realTag.LibPlcTag);
            }
            public virtual async Task<float> WriteTagRealValue(Tag realTag, float value)
            {
                return await Real.WriteRealTag((Tag<RealPlcMapper, float>)realTag.LibPlcTag, value);
            }
        }

        public class RealArrayTag : RealTag
        {
            public override object CreateTag(Tag tag)
            {
                return DataTypes.RealArray.CreateRealArrayTag(tag);
            }
            public override async Task<float> ReadTagRealValue(Tag realArrayTag)
            {
                return await RealArray.ReadRealArrayTag((Tag<RealPlcMapper, float[]>)realArrayTag.LibPlcTag,
                    (int)realArrayTag.ArrayIndex);
            }
            public async Task<float[]> ReadTagRealArrayValue(Tag realArrayTag)
            {
                return await RealArray.ReadRealArrayTag((Tag<RealPlcMapper, float[]>)realArrayTag.LibPlcTag);
            }
        }

        public class BrowseTags
        {
            private const int MAX_DEPTH = 7;
            private string _gateway;
            private string _slot;

            // Helper method to asynchronously process UDT fields
            private async Task ProcessUdtFieldsAsync(ushort type, string basePath, List<TagDetail> tagDetails, int depth)
            {
                if (depth > MAX_DEPTH) return;

                var udtTag = new Tag<UdtInfoPlcMapper, UdtInfo>()
                {
                    Gateway = _gateway,
                    Path = $"1,{_slot}",
                    PlcType = PlcType.ControlLogix,
                    Protocol = Protocol.ab_eip,
                    Name = $"@udt/{GetUdtId(type)}",
                };

                // Assuming an asynchronous Read method is available
                await udtTag.ReadAsync();

                foreach (var f in udtTag.Value.Fields)
                {
                    if (f.Name.StartsWith("ZZZZZZZZ")) continue;
                    var fullPath = $"{basePath}.{f.Name}";
                    if (TagIsUdt(f.Type))
                    {
                        await ProcessUdtFieldsAsync(f.Type, fullPath, tagDetails, depth + 1);
                    }
                    else
                    {
                        tagDetails.Add(new TagDetail
                        {
                            Tagname = fullPath,
                            Datatype = FindType(f.Type),
                        });
                    }
                }
            }

            public async Task<List<TagDetail>> BrowseControllerTagsAsync(string gateway, string slot)
            {
                _gateway = gateway;
                _slot = slot;
                var tags = new Tag<TagInfoPlcMapper, TagInfo[]>()
                {
                    Gateway = gateway,
                    Path = $"1,{slot}",
                    PlcType = PlcType.ControlLogix,
                    Protocol = Protocol.ab_eip,
                    Name = "@tags",
                    Timeout = TimeSpan.FromSeconds(10)
                };

                // Assuming an asynchronous Read method is available
                await tags.ReadAsync();

                var tagDetails = new List<TagDetail>();
                foreach (var tag in tags.Value)
                {
                    if (tag.Name.StartsWith("__DEFVAL_") || tag.Name.StartsWith("UDI:"))
                        continue;

                    if (TagIsUdt(tag.Type))
                    {
                        await ProcessUdtFieldsAsync(tag.Type, tag.Name, tagDetails, 1);
                    }
                    else
                    {
                        if (!FindType(tag.Type).Equals("unknown"))
                        {
                            tagDetails.Add(new TagDetail
                            {
                                Tagname = tag.Name,
                                Datatype = FindType(tag.Type),
                            });
                        }
                    }
                }

                return tagDetails;
            }

            private string FindType(ushort type)
            {
                switch (type)
                {
                    case 193: return "BOOL";        // 0x00c1
                    case 194: return "SINT";        // 0x00c2
                    case 195: return "INT";         // 0x00c3
                    case 196: return "DINT";        // 0x00c4
                    case 202: return "REAL";        // 0x00ca
                    case 8403: return "BOOL[]";
                    case 8386: return "SINT[]";
                    case 8387: return "INT[]";
                    case 8388: return "DINT[]";
                    case 8394: return "REAL[]";
                    case 16580: return "DINT[,]";
                    case 36814: return "STRING";    // 0x8fce
                    default: return "unknown";
                }
            }

            static bool TagIsUdt(ushort type)
            {
                if (type != 36814)  // 36814 is a string, i think
                {
                    const ushort TYPE_IS_STRUCT = 0x8000;
                    const ushort TYPE_IS_SYSTEM = 0x1000; 
                    var is_udt = ((type & TYPE_IS_STRUCT) != 0) && !((type & TYPE_IS_SYSTEM) != 0);
                    return is_udt;
                }
                else { return false; }
            }

            static int GetUdtId(ushort type)
            {
                const ushort TYPE_UDT_ID_MASK = 0x0FFF;
                return type & TYPE_UDT_ID_MASK;
            }

            static bool TagIsProgram(TagInfo tag, out string prefix)
            {
                if (tag.Name.StartsWith("Program:"))
                {
                    prefix = tag.Name;
                    return true;
                }
                else
                {
                    prefix = string.Empty;
                    return false;
                }
            }
        }

    }
}
