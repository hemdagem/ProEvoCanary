﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Models;

namespace ProEvoCanary.IntegrationTests
{
    public class CacheResultsRepositoryTests
    {
        private MemoryCache _cache;
        private CacheItemPolicy _cacheItemPolicy;

        private void Setup()
        {
            _cache = MemoryCache.Default;
            _cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
            };
        }

        private void End()
        {
            _cache.Remove("TopPlayerCacheList");
            _cache.Remove("PlayerCacheList");
        }


        [Test]
        public void ShouldGetCachedResults()
        {
            //given
            Setup();
            var resultsModel = new List<ResultsModel>
            {
                new ResultsModel
                {
                    AwayScore = 1
                }
            };
            const string recentResultsKey = "recent_results";

            _cache.Set(recentResultsKey, resultsModel, _cacheItemPolicy);

            var repository = new ResultsCacheRepository(new CachingManager(_cache));

            //when
            var resultsModels = repository.GetResults();

            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels[0].AwayScore, Is.EqualTo(resultsModel[0].AwayScore));

            End();
        }


        [Test]
        public void ShouldGetCachedHeadToHeadRecord()
        {
            //given
            Setup();
            var resultsModel = new RecordsModel
            {
                PlayerOneWins = 1,
                Results = new List<ResultsModel>
                {
                    new ResultsModel
                    {
                        AwayScore = 1
                    }
                }
            };

            string headToHeadResultsKey = string.Format("{0}_{1}", 1, 2);

            _cache.Set(headToHeadResultsKey, resultsModel, _cacheItemPolicy);

            var repository = new ResultsCacheRepository(new CachingManager(_cache));

            //when
            var resultsModels = repository.GetHeadToHeadRecord(1, 2);

            Assert.IsNotNull(resultsModels);
            Assert.That(resultsModels.PlayerOneWins, Is.EqualTo(resultsModel.PlayerOneWins));
            Assert.That(resultsModels.Results.Count,Is.EqualTo(1));
            Assert.That(resultsModels.Results.First().AwayScore,Is.EqualTo(1));

            End();
        }

    }


}
