using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
	public class DefaultController : Controller
	{
		private readonly IHttpClientFactory _clientFactory;

		public DefaultController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<ActionResult> Index()
		{
			var client =_clientFactory.CreateClient("API");
			var homeModel = JsonConvert.DeserializeObject<HomeModel>(await client.GetStringAsync("api/Home"));
			
			return View("Index", homeModel);
		}
	}
}
