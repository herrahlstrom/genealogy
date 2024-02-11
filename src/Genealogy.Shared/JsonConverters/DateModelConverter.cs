using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Genealogy.Shared.JsonConverters;

internal class DateModelConverter : JsonConverter<DateModel>
{
    public override DateModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is { } value)
        {
            return new DateModel(value);
        }
        return DateModel.Empty;
    }

    public override void Write(Utf8JsonWriter writer, DateModel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
