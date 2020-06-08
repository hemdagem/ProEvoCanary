namespace ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed
{
    public class RssFeedQuery
    {
	    public RssFeedQuery(string url)
	    {
		    Url = url;
	    }
        public string Url { get; }
    }
}