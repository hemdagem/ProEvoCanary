using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers
{
    public class RssLoader : IRssLoader
    {
        public List<RssFeedModel> Load(string url)
        {

            //var reader = XmlReader.Create(url);
            //var feed = SyndicationFeed.Load(reader);
            //reader.Close();
            return new List<RssFeedModel>();
            //if (feed != null)
            //{
            //    foreach (SyndicationItem syndicationItem in feed.Items)
            //    {

            //        var rssFeedModel = new RssFeedModel
            //        {
            //            LinkTitle = syndicationItem.Title.Text,
            //            LinkDescription = syndicationItem.Summary.Text,
            //            LinkUrl = syndicationItem.Id,
            //            ImageUrl = syndicationItem.ElementExtensions.Count > 0 ? syndicationItem.ElementExtensions.Select(e => e.GetObject<XElement>().Attribute("url").Value).Last() : ""
            //        };

            //        rssFeedModelList.Add(rssFeedModel);
            //    }
            //}


            //return rssFeedModelList;
        }
    }
}