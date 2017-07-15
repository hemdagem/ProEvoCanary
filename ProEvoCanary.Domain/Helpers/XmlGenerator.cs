using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers
{
    public class XmlGenerator : IXmlGenerator
    {
        public string GenerateFixtures(List<TeamIds> teamIds, int eventId)
        {
            if (teamIds.Count == 0)
            {
                throw new Exception("No teamIds");
            }

            var template = "<dataset>" +
                              "<metadata>" +
                                  "<item name=\"TournamentId\" type=\"xs:int\" />" +
                                  "<item name=\"HomeScore\" type=\"xs:int\"  />" +
                                  "<item name=\"AwayScore\" type=\"xs:int\"  />" +
                                  "<item name=\"Round\" type=\"xs:int\"  />" +
                                  "<item name=\"HomeUserId\" type=\"xs:int\" />" +
                                  "<item name=\"AwayUserId\" type=\"xs:int\" />" +
                              "</metadata>" +
                              "<data>{0}</data>" +
                          "</dataset> ";

            var teamValues = "";

            foreach (var teamId in teamIds)
            {
                teamValues += string.Format("<row>" +
                                  "<value>{0}</value>" +
                                  "<value>-1</value>" +
                                  "<value>-1</value>" +
                                  "<value>0</value>" +
                                  "<value>{1}</value>" +
                                  "<value>{2}</value>" +
                                  "<value>0</value>" +
                              "</row>", eventId, teamId.TeamOne, teamId.TeamTwo);

            }

            return string.Format(template, teamValues);
        }

        public string GenerateTournamentUsers(List<int> userIds, int eventId)
        {
            if (userIds.Count == 0)
            {
                throw new Exception("No users");
            }

            var template = "<dataset>" +
                              "<metadata>" +
                                  "<item name=\"UserId\" type=\"xs:int\" />" +
                                  "<item name=\"TournamentId\" type=\"xs:int\"  />" +
                                  "<item name=\"PreviousPosition\" type=\"xs:int\"  />" +
                              "</metadata>" +
                              "<data>{0}</data>" +
                          "</dataset> ";

            var teamValues = "";

            foreach (var id in userIds)
            {
                teamValues += string.Format("<row>" +
                                  "<value>{0}</value>" +
                                  "<value>{1}</value>" +
                                  "<value>0</value>" +
                              "</row>", id, eventId);
            }

            return string.Format(template, teamValues);
        }
    }
}