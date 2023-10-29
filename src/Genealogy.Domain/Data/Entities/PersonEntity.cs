using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public class PersonEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Notes { get; set; }
    public string? Profession { get; set; }
    public required string Sex { get; set; }
}
