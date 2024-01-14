using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Genealogy.Shared;

public partial class DateModel : IEquatable<DateModel>, IComparable<DateModel>
{
    private readonly Lazy<DateOnly?> _lazyDate;
    private readonly Lazy<int?> _lazyYear;

    public DateModel(string? value)
    {
        Value = value ?? "";
        _lazyYear = new Lazy<int?>(GetYear);
        _lazyDate = new Lazy<DateOnly?>(GetDate);
    }

    public DateOnly? Date => _lazyDate.Value;

    public string GetDisplayDate()
    {
        if (Date is { } d)
        {
            return d.ToString("d MMM yyyy");
        }
        if (Year is { } y)
        {
            return y.ToString();
        }
        return Value;
    }

    public bool HasValue => Value is { Length: > 0 };
    public string Value { get; }
    public int? Year => _lazyYear.Value;

    public static implicit operator DateModel(string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return new DateModel("");
        }
        return new DateModel(str);
    }

    public override string ToString()
    {
        return Value;
    }
    public bool Equals(DateModel? other)
    {
        return other != null && other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DateModel);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    [GeneratedRegex(@"^(?<year>\d{4})\-(?<month>\d{2})\-(?<day>\d{2})$")]
    private static partial Regex ExactDateRegex();

    [GeneratedRegex(@"^(?<year>\d{4})(\D|$)")]
    private static partial Regex YearRegex();

    private DateOnly? GetDate()
    {
        if (ExactDateRegex().Match(Value) is { Success: true } m)
        {
            return new DateOnly(
               int.Parse(m.Groups["year"].Value),
               int.Parse(m.Groups["month"].Value),
               int.Parse(m.Groups["day"].Value));
        }
        return null;
    }

    private int? GetYear()
    {
        if (YearRegex().Match(Value) is { Success: true } m)
        {
            return int.Parse(m.Groups["year"].Value);
        }
        return null;
    }

    public int CompareTo(DateModel? other)
    {
        if (other is null)
            return -1;

        if (Date.HasValue && other.Date.HasValue)
        {
            return Date.Value.CompareTo(other.Date.Value);
        }

        if (Year.HasValue && other.Year.HasValue)
        {
            return Year.Value.CompareTo(other.Year.Value);
        }

        return Value.CompareTo(other.Value);
    }
}
