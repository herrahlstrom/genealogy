using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genealogy.Domain.Data.Entities;

public class FamilyEntity
{
    public required Guid Id { get; set; }
    public string Notes { get; set; } = "";
}
