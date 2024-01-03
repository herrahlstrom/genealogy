using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Genealogy.Shared.JsonConverters;

internal class PersonNameConverter : JsonConverter<PersonName>
{
    public override PersonName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is { } value)
        {
            return new PersonName(value);
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, PersonName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
