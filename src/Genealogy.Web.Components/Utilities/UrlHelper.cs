using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace Genealogy.Web.Components.Utilities;

public class UrlHelper
{
    private readonly NameValueCollection _queryCollection;
    private readonly Uri _uri;

    private string _fragment;

    public UrlHelper(Uri uri)
    {
        _uri = uri;
        _queryCollection = HttpUtility.ParseQueryString(_uri.Query);
        _fragment = _uri.Fragment;
    }

    public UrlHelper(NavigationManager navManager) : this(new Uri(navManager.Uri))
    {
    }

    public UrlHelper AppendQueryParameter(string name, string value)
    {
        _queryCollection.Add(name, value);

        return this;
    }

    public UrlHelper SetFragment(string fragment)
    {
        _fragment = fragment;
        return this;
    }

    public UrlHelper SetQueryParameter(string name, string? value)
    {
        if (value == null)
        {
            _queryCollection.Remove(name);
        }
        else
        {
            _queryCollection.Set(name, value);
        }

        return this;
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        // Path
        result.Append(_uri.AbsolutePath);

        // Parameters
        if (_queryCollection.Count > 0)
        {
            result.AppendFormat("?{0}", string.Join("&", _queryCollection));
        }

        // Fragment
        if (string.IsNullOrWhiteSpace(_fragment) == false)
        {
            result.Append(_uri.Fragment);
        }

        return result.ToString();
    }

    public bool TryGetQueryParameter(string name, [MaybeNullWhen(false)] out string value)
    {
        if (_queryCollection[name] is { } queryValue)
        {
            value = queryValue;
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGetQueryParameters(string name, [MaybeNullWhen(false)] out ICollection<string> value)
    {
        if (_queryCollection[name] is { } queryValue)
        {
            value = queryValue.Split(',');
            return true;
        }

        value = default;
        return false;
    }
}
