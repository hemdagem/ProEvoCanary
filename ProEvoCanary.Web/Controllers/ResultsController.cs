using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProEvoCanary.Web.Controllers
{
	public class ResultsController : Controller
	{
		private readonly HttpClient _client;


		public ResultsController(IHttpClientFactory clientFactory)
		{
			_client = clientFactory.CreateClient("API");
		}

		// GET: Results
		public async Task<ActionResult> Update(Guid id)
		{
			var model = JsonConvert.DeserializeObject<Models.ResultsModel>(await _client.GetStringAsync("/api/Event"));
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Update(Models.ResultsModel model)
		{
			var put = await _client.PutAsync("/api/Results", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
			var readAsStringAsync = await put.Content.ReadAsStringAsync();

			var eventId = JsonConvert.DeserializeObject<Guid>(readAsStringAsync);


			return RedirectToAction("Details", "Event", new { id = model.EventId });

		}
	}
}