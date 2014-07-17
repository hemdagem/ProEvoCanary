using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using ProEvo45.Models;

namespace ProEvo45.Helpers
{
    public class Loader : ILoader
    {
        public List<RssFeedModel> Load(string url)
        {

            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var rssFeedModel = new List<RssFeedModel>();
            foreach (var syndicationItem in feed.Items)
            {
                rssFeedModel.Add(new RssFeedModel
                {
                    LinkTitle = syndicationItem.Title.Text,
                    LinkDescription = syndicationItem.Summary.Text,
                    LinkUrl = syndicationItem.Id
                });
            }

            return rssFeedModel;
        }
    }
}