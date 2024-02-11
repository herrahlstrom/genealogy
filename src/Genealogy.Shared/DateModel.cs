using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Genealogy.Shared;

public partial struct DateModel : IEquatable<DateModel>, IComparable<DateModel>
{
    private readonly Lazy<DateOnly?> _lazyDate;
    private readonly Lazy<int?> _lazyYear;

    private static DateModel _emptyInstance = new("");

    public DateModel(string? value)
    {
        Value = value ?? "";
        _lazyYear = new Lazy<int?>(GetYear);
        _lazyDate = new Lazy<DateOnly?>(GetDate);
    }

    public static DateModel Empty
    {
        get => _emptyInstance;
    }

    public readonly DateOnly? Date => _lazyDate.Value;

    public readonly string DisplayDate => GetDisplayDate();
    public readonly bool HasValue => Value is { Length: > 0 };
    public string Value { get; }
    public readonly int? Year => _lazyYear.Value;

    public static implicit operator DateModel(string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return new DateModel("");
        }
        return new DateModel(str);
    }

    public readonly int CompareTo(DateModel other)
    {
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
    public readonly bool Equals(DateModel other)
    {
        return other.Value == Value;
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is DateModel)
        {
            return Equals((DateModel)obj);
        }
        return false;
    }
    public readonly string GetDisplayDate()
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

    public override readonly int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override readonly string ToString()
    {
        return Value;
    }

    [GeneratedRegex(@"^(?<year>\d{4})\-(?<month>\d{2})\-(?<day>\d{2})$")]
    private static partial Regex ExactDateRegex();

    [GeneratedRegex(@"^(?<year>\d{4})(\D|$)")]
    private static partial Regex YearRegex();

    private readonly DateOnly? GetDate()
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

    private readonly int? GetYear()
    {
        if (YearRegex().Match(Value) is { Success: true } m)
        {
            return int.Parse(m.Groups["year"].Value);
        }
        return null;
    }
}
