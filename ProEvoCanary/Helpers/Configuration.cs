using ProEvoCanary.Helpers.Interfaces;

namespace ProEvoCanary.Helpers
{
    public class Configuration : IConfiguration
    {
        public string GetConfig()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ProEvoLeagueConnectionString"].ToString();
        }
    }
}