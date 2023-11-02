using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace Genealogy.Infrastructure.Data.Migration.Sqlite;
internal partial class SqliteScriptProvider : IScriptProvider
{
    public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
    {
        var assembly = Assembly.GetExecutingAssembly();

        string? prefix = GetType().Namespace;
        var resourceNames = from name in assembly.GetManifestResourceNames()
                            where name.EndsWith(".sql")
                            where name.Contains("Migration.Sqlite")
                            select name;

        foreach (var resourceName in resourceNames)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                continue;
            }

            using var reader = new StreamReader(stream);
            yield return new SqlScript(
                name: GetScriptName(resourceName),
                contents: reader.ReadToEnd());
        }
    }

    [GeneratedRegex("Migration\\.Sqlite\\.(?<name>\\d+.+)\\.sql$")]
    private static partial Regex ScriptNameRegex();

    private static string GetScriptName(string resourceName)
    {
        var m = ScriptNameRegex().Match(resourceName);
        if (m.Success)
        {
            return m.Groups["name"].Value;
        }
        return resourceName;
    }
}
