using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.Domain.Helpers
{
    public class DbConfiguration : IDBConfiguration
    {
	    private readonly IConfiguration _configuration;

	    public DbConfiguration(IConfiguration configuration)
	    {
		    _configuration = configuration;
	    }

        const string ProEvoLeagueConnectionString = "ProEvoLeagueConnectionString";

        public string GetConfig()
        {
	        return _configuration[ProEvoLeagueConnectionString];
        }
    }
}