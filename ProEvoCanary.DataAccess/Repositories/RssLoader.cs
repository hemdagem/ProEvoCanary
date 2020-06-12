using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories
{
	public class RssLoader : IRssLoader
	{
		public List<RssFeedModel> Load(string url)
		{
			using var reader = XmlReader.Create(url);
			var rssFeedModelList = new List<RssFeedModel>();

			var feed = SyndicationFeed.Load(reader);
			if (feed == null) return rssFeedModelList;
			rssFeedModelList.AddRange(feed.Items.Select(syndicationItem => new RssFeedModel { LinkTitle = syndicationItem.Title.Text, LinkDescription = syndicationItem.Summary.Text, LinkUrl = syndicationItem.Id, ImageUrl = syndicationItem.ElementExtensions.Count > 0 ? syndicationItem.ElementExtensions.Select(e => e.GetObject<XElement>().Attribute("url").Value).Last() : "" }));
			return rssFeedModelList;
		}
	}
}