using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProEvoCanary.DataAccess;
using ProEvoCanary.DataAccess.Helpers;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Events.Commands;
using ProEvoCanary.Domain.EventHandlers.Events.Queries;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Domain.EventHandlers.Results.GetResults;
using ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed;
using ProEvoCanary.Domain.Infrastructure;
using ProEvoCanary.Web.Models;
using PlayerModel = ProEvoCanary.Domain.EventHandlers.Events.Queries.PlayerModel;
using ResultsModel = ProEvoCanary.Domain.EventHandlers.Events.Queries.ResultsModel;
using Standings = ProEvoCanary.Domain.EventHandlers.Events.Queries.Standings;
using TournamentType = ProEvoCanary.Domain.EventHandlers.Events.Queries.TournamentType;

namespace ProEvoCanary.Web
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Default}/{action=Index}/{id?}");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			

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

			//Auto mapper
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<PlayerModel, PlayerModelDto>();
				cfg.CreateMap<PlayerModel, Models.PlayerModel>();
				cfg.CreateMap<ResultsModel, Models.ResultsModel>();
				cfg.CreateMap<ResultsModel, GetResultsModelDto>();
				cfg.CreateMap<TournamentType, Models.TournamentType>();
				cfg.CreateMap<Standings, Models.Standings>();
				cfg.CreateMap<RssFeedModelDto, DataAccess.Models.RssFeedModel>();
				cfg.CreateMap<DataAccess.Models.RssFeedModel, RssFeedModelDto>();
				cfg.CreateMap<RssFeedModelDto, RssFeedModel>();
				cfg.CreateMap<EventModel, EventModelDto>();
				cfg.CreateMap<EventModelDto, EventModel>();
				cfg.CreateMap<EventModelDto, DataAccess.Models.EventModel>();
				cfg.CreateMap<DataAccess.Models.EventModel, EventModelDto>();
			});

			services.AddTransient<IMapper>(x => mapperConfiguration.CreateMapper());


		}
		
	}
}