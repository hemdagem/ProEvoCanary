using System;
using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers
{
    public class FixtureGenerator :IFixtureGenerator
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

            foreach (int teamOne in teamIds)
            {
                foreach (int teamTwo in teamIds)
                {
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

    public interface IFixtureGenerator
    {
        List<TeamIds> Generate(List<int> teamIds);
    }
}