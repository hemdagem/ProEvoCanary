using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Domain.Repositories.Interfaces;

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
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			services.AddTransient<ICacheManager, CachingManager>();
			services.AddTransient<ICacheRssLoader, RssCacheLoader>();
			services.AddTransient<IDBConfiguration, DbConfiguration>();
			services.AddTransient<IDbHelper, DbHelper>();

			services.AddTransient<IRssLoader, RssLoader>();
			//services.AddTransient<IAuthenticationHandler, AuthenticationHandler>();
			services.AddTransient<IPasswordHash, PasswordHash>();
			services.AddTransient<IAppUser, CurrentAppUser>();
			services.AddTransient<IXmlGenerator, XmlGenerator>();

			services.AddTransient<IEventRepository, EventRepository>();
			services.AddTransient<ICacheEventRepository, CacheEventRepository>();
			services.AddTransient<ICachePlayerRepository, CachePlayerRepository>();
			services.AddTransient<ICacheResultsRepository, ResultsCacheRepository>();
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<IResultRepository, ResultsRepository>();
			services.AddTransient<IRssFeedRepository, RssFeedRepositoryDecorator>();
			services.AddTransient<IUserRepository, UserRepository>();

			//Auto mapper
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Domain.Models.RecordsModel, Models.RecordsModel>();
				cfg.CreateMap<Domain.Models.PlayerModel, Models.PlayerModel>();
				cfg.CreateMap<Domain.Models.EventModel, Models.EventModel>();
				cfg.CreateMap<Domain.Models.ResultsModel, Models.ResultsModel>();
				cfg.CreateMap<Domain.Models.RssFeedModel, Models.RssFeedModel>();
				cfg.CreateMap<Domain.Models.TournamentType, Models.TournamentType>();
				cfg.CreateMap<Domain.Models.Standings, Models.Standings>();
			});

			services.AddTransient<IMapper>(x => mapperConfiguration.CreateMapper());


		}
		
	}
}