using System;
using System.Linq;

namespace Genealogy.Web.Models.Person;

public interface ILink
{
    public string Text { get; }

    public Uri Url { get; }
}
