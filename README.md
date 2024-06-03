## Steps:
1. Specify the tag type (ex: public PlcComm.Tags.StringTag StringTag { get; set; })
2. Populate all necessary tag information (ex: tag name, plc type, etc)
3. Create the libplctag using the CreateLibPlcTag method built into all PlcComm.Tags.Tag objects (ex: StringTag.LibPlcTag = StringTag.CreateTag(StringTag);)
4. Call the Read method associated with the tag type (ex: string value = StringTag.ReadStringValue();)

See unit Tests for examples.