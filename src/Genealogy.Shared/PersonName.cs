using System;
using System.Linq;

namespace Genealogy.Shared;

public class PersonName
{
    public PersonName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            FirstName = "";
            GivenName = "";
            NickName = "";
            LastName = "";
        }
        else if (TryParseLastName(value, out string fn, out string ln))
        {
            FirstName = fn;
            GivenName = TryParseGivenName(fn, out string gn) ? gn : "";
            NickName = TryParseNickName(fn, out string nn) ? nn : "";
            LastName = ln;
        }
        else
        {
            throw new ArgumentException($"Invalid name: \"{value}\"");
        }
    }

    public PersonName(string firstName, string lastName)
    {
        FirstName = firstName;

        if (string.IsNullOrWhiteSpace(firstName))
        {
            GivenName = "";
            NickName = "";
        }
        else
        {
            GivenName = TryParseGivenName(firstName, out string gn) ? gn : "";
            NickName = TryParseNickName(firstName, out string nn) ? nn : "";
        }

        LastName = lastName;
    }

    public string DisplayName
    {
        get
        {
            if (NickName.Length > 0)
            {
                return $"{NickName} {LastName}".Trim();
            }
            if (GivenName.Length > 0)
            {
                return $"{GivenName} {LastName}".Trim();
            }
            return $"{FirstName} {LastName}".Trim();
        }
    }

    public string FirstName { get; }

    public string FullName => string.IsNullOrEmpty(FirstName)
        ? $"/{LastName}/"
        : $"{FirstName} /{LastName}/";

    public string GivenName { get; }

    public string LastName { get; }
    public string NickName { get; }

    public override string ToString()
    {
        return FullName;
    }
    private bool TryParseGivenName(string firstName, out string givenName)
    {
        var arr = firstName.Split(' ');

        foreach (var str in arr)
        {
            if (str.EndsWith('*'))
            {
                givenName = str[..^1];
                return true;
            }
        }

        if (arr.Length > 0)
        {
            givenName = arr[0];
            return true;
        }

        givenName = default!;
        return false;
    }

    private bool TryParseLastName(string input, out string firstName, out string lastName)
    {
        int numberOfSlashes = input.Count(x => x.Equals('/'));

        if (numberOfSlashes == 0)
        {
            var arr = input.Split();
            if (arr.Length == 1)
            {
                firstName = arr[0];
                lastName = "";
            }
            else
            {
                firstName = string.Join(" ", arr.Take(arr.Length - 1));
                lastName = arr.Last();
            }
            return true;
        }

        ReadOnlySpan<char> span = input.AsSpan();

        if (numberOfSlashes == 2 && span.EndsWith("/"))
        {
            var p = span.IndexOf('/');

            if (p > 0)
            {
                var fnSpan = span[..(p - 1)].Trim();
                firstName = fnSpan.ToString();
            }
            else
            {
                firstName = "";
            }

            var lnSpan = span[(p + 1)..];
            lnSpan = lnSpan[..^1];
            lastName = lnSpan.Trim().ToString();

            return true;
        }

        firstName = default!;
        lastName = default!;
        return false;
    }

    private bool TryParseNickName(string firstName, out string nickName)
    {
        nickName = default!;
        return false;
    }
}