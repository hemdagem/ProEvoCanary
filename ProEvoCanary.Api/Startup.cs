using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProEvoCanary.Application.EventHandlers.Events.Commands;
using ProEvoCanary.Application.EventHandlers.Events.Queries;
using ProEvoCanary.Application.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Application.EventHandlers.Results.GetResults;
using ProEvoCanary.Application.EventHandlers.RssFeeds.GetFeed;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess;
using ProEvoCanary.DataAccess.Helpers;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddTransient<ICacheManager, CacheManager>();
			services.AddTransient<IDBConfiguration, DbConfiguration>();
			services.AddTransient<IDbHelper, DbHelper>();

			services.AddTransient<IRssRepository, RssRepository>();
			services.AddTransient<IXmlGenerator, XmlGenerator>();

			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<IResultRepository, ResultsRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IEventWriteRepository, EventWriteRepository>();
			services.AddTransient<IEventReadRepository, EventReadRepository>();
			services.AddTransient<IEventsQueryHandler, EventsQueryHandler>();
			services.AddTransient<IEventCommandHandler, EventCommandHandler>();
			services.AddTransient<IGetRssFeedQueryHandler, GetRssFeedQueryHandler>();
			services.AddTransient<IGetPlayersQueryHandler, GetPlayersQueryHandler>();
			services.AddTransient<IGetResultsQueryHandler, GetResultsQueryHandler>();



		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
