using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Web.Models.EventModel;

namespace ProEvoCanary.Web.Controllers
{
	public class EventController : Controller
	{
		private readonly HttpClient _client;

		public EventController(IHttpClientFactory clientFactory)
		{
			_client = clientFactory.CreateClient("API");
		}

		public ActionResult Create()
		{
			return View("Create", new AddEventModel());
		}

		[HttpPost]
		public async Task<ActionResult> Create(AddEventModel model)
		{
			var put = await _client.PutAsync("/api/Event", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
			var readAsStringAsync = await put.Content.ReadAsStringAsync();

			var eventId = JsonConvert.DeserializeObject<Guid>(readAsStringAsync);

			return RedirectToAction("GenerateFixtures", "Event", new { eventId });
		}

		public async Task<ActionResult> Details(Guid id)
		{
			var model = JsonConvert.DeserializeObject<EventModel>(await _client.GetStringAsync("/api/Event"));
			return View("Details", model);
		}

		public async Task<ActionResult> GenerateFixtures(Guid id)
		{
			var model = JsonConvert.DeserializeObject<FixturesModel>(await _client.GetStringAsync($"/api/Fixtures/{id}"));
			return View("GenerateFixtures", model);
		}

		[HttpPost]
		public async Task<ActionResult> GenerateFixtures(Guid id, List<int> userIds)
		{
			var eventCommand = new GenerateFixturesModel(id, userIds);
			var put = await _client.PutAsync("/api/Fixtures", new StringContent(JsonConvert.SerializeObject(eventCommand)));
			var eventId = JsonConvert.DeserializeObject<Guid>(await put.Content.ReadAsStringAsync());
			return RedirectToAction("Details", "Event", new { Id = eventId });
		}
	}

	public class GenerateFixturesModel
	{
		public Guid Id { get; }
		public List<int> UserIds { get; }

		public GenerateFixturesModel(Guid id, List<int> userIds)
		{
			Id = id;
			UserIds = userIds;
		}
	}
}