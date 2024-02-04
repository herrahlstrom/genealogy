using System;
using System.Linq;

namespace Genealogy.Web.Models.Person;

public record PersonReference(PersonName Name, Uri Url) : ILink
{
    string ILink.Text => Name.DisplayName;
};