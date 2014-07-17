﻿using System.Collections.Generic;

namespace ProEvo45.Models
{
    public class HomeModel
    {
        public List<PlayerModel> Players { get; set; }
        public List<RssFeedModel> News { get; set; }
        public List<EventModel> Events { get; set; }
        public List<ResultsModel> Results { get; set; }
    }
}