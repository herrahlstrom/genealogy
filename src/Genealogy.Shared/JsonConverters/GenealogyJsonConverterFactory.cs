using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Genealogy.Shared.JsonConverters;

internal class GenealogyJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert == typeof(DateModel))
            return true;
        if (typeToConvert == typeof(PersonName))
            return true;
        return false;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(DateModel))
        {
            return new DateModelConverter();
        }

        if (typeToConvert == typeof(PersonName))
        {
            return new PersonNameConverter();
        }

        return null;
    }
}
