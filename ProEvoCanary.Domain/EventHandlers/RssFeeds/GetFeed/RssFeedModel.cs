﻿namespace ProEvoCanary.Application.EventHandlers.RssFeeds.GetFeed
{
    public class RssFeedModelDto
    {
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string LinkDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageHeight { get; set; }
        public string ImageWidth { get; set; }
        public string PublishDate { get; set; }
    }
}