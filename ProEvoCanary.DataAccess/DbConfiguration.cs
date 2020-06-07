using Microsoft.Extensions.Configuration;

namespace ProEvoCanary.DataAccess
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