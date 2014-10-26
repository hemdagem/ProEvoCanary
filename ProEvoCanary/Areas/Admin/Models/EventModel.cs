﻿using System;
using System.ComponentModel.DataAnnotations;
using ProEvoCanary.Models;

namespace ProEvoCanary.Areas.Admin.Models
{
    public class EventModel
    {
        public EventModel() { }

        public EventModel(EventTypes eventType, string tournamentName, DateTime date, SelectListModel userSelectListModel)
        {
            EventType = eventType;
            TournamentName = tournamentName;
            Date = date;
            UserSelectListModel = userSelectListModel;
        }


        [Required]
        [Range(1, 3)]
        public EventTypes? EventType { get; set; }

        [Required]
        public string TournamentName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public SelectListModel UserSelectListModel { get; set; }

    }
}