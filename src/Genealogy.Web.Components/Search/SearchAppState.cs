namespace Genealogy.Web.Components.Search;

public class SearchAppState
{
    public event EventHandler<string>? SearchEvent;

    public void Search(string query)
    {
        SearchEvent?.Invoke(this, query);
    }
}
