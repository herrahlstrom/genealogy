using System;
using System.Linq;

namespace Genealogy.Web.Models.Person;

public record PersonReference(PersonName Name, Uri Url) : ILink
{
    string ILink.Text => Name.DisplayName;
};

public record SourceReference(string Name, Uri Url) : ILink
{
    string ILink.Text => Name;
};

public record EventReference(string Name, Uri Url) : ILink
{
    string ILink.Text => Name;
};