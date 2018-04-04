using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlyingParrot.Models;

namespace FlyingParrot.Controllers
{
	//[RoutePrefix("api/Sound")]
	public class SoundController : ApiController
	{
		public static List<Sound> sounds;

		[HttpGet]
		[Route("All")]
		public IEnumerable<Sound> GetAllSounds() {
			sounds = Sound.LoadAll();
			return sounds;
		}

		[HttpGet]
		//[Route("Bathrooms")]
		public Sound GetSoundsById(int id) {
			return sounds.FirstOrDefault(x => x.Id == id);
		}
	}
}
