using System;
using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class FixtureGenerator
    {
        public List<TeamIds> Generate(List<int> teamIds)
        {
            if (teamIds == null || teamIds.Count == 0)
            {
                throw new ArgumentNullException();
            }

            if (teamIds.Distinct().Count() != teamIds.Count)
            {
                throw new NotUniqueException();
            }

            var generatedTeamIds = new List<TeamIds>();

            for (int i = 0; i < teamIds.Count; i++)
            {
                for (int j = 0; j < teamIds.Count; j++)
                {
                    int teamOne = teamIds[i];
                    int teamTwo = teamIds[j];
                    if (teamOne != teamTwo)
                    {
                        if (!FixturesAreGenerated(teamOne, teamTwo, generatedTeamIds))
                        {
                            generatedTeamIds.Add(new TeamIds
                            {
                                TeamOne = teamOne,
                                TeamTwo = teamTwo
                            });
                        }
                    }
                }
            }

            return generatedTeamIds;
        }

        private bool FixturesAreGenerated(int teamOne, int teamTwo, IEnumerable<TeamIds> generatedIds)
        {
            var fixturesAreGenerated = false;

            foreach (var generatedId in generatedIds)
            {
                if ((generatedId.TeamOne == teamOne && generatedId.TeamTwo == teamTwo) ||
                    (generatedId.TeamTwo == teamOne && generatedId.TeamOne == teamTwo))
                {
                    fixturesAreGenerated = true;
                    break;
                }

            }

            return fixturesAreGenerated;
        }
    }
}