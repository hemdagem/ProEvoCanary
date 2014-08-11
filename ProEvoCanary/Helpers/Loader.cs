using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class Loader : ILoader
    {
        public List<RssFeedModel> Load(string url)
        {

            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var rssFeedModelList = new List<RssFeedModel>();
            if (feed != null)
            {
                foreach (SyndicationItem syndicationItem in feed.Items)
                {

                    var rssFeedModel = new RssFeedModel
                    {
                        LinkTitle = syndicationItem.Title.Text,
                        LinkDescription = syndicationItem.Summary.Text,
                        LinkUrl = syndicationItem.Id,
                        ImageUrl = syndicationItem.ElementExtensions.Select(e => e.GetObject<XElement>().Attribute("url").Value).Last()
                    };

                    rssFeedModelList.Add(rssFeedModel);
                }
            }


            return rssFeedModelList;
        }
    }
}