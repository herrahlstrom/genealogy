using System;
using System.Linq;
using System.Text.Json;
using Genealogy.Shared.JsonConverters;

namespace Genealogy;

public static class JsonExtensions
{
    public static JsonSerializerOptions SetGenealogyDefault(this JsonSerializerOptions jsonOptions)
    {
        jsonOptions.PropertyNameCaseInsensitive = true;
        jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        return jsonOptions;
    }
    public static JsonSerializerOptions AddGenealogyConverters(this JsonSerializerOptions jsonOptions)
    {
        jsonOptions.Converters.Add(new GenealogyJsonConverterFactory());
        return jsonOptions;
    }
}
