using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Genealogy;

public static class AttributeExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        MemberInfo enumMember = enumValue.GetType()
                                         .GetMember(enumValue.ToString())
                                         .First();
        return enumMember.GetCustomAttribute<DisplayAttribute>()?.Name ?? enumValue.ToString();
    }
}
