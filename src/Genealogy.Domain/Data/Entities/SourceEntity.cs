using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public class SourceEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Notes { get; set; }
    public string? Page { get; set; }
    public string? ReferenceId { get; set; }
    public string? Repository { get; set; }
    public SourceType Type { get; set; }
    public string? Url { get; set; }
    public string? Volume { get; set; }
}
