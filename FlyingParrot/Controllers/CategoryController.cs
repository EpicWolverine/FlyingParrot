using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlyingParrot.Models;

namespace FlyingParrot.Controllers
{
	[RoutePrefix("api/Category")]
	public class CategoryController : ApiController
	{
		[HttpGet]
		[Route("AllSounds")]
		public IEnumerable<Category> GetAllSounds() {
			return Category.LoadAll();
		}

		[HttpGet]
		public Category GetCategoryWithSounds(int id) {
			var category = new Category() { Id = id };
			category.LoadSounds();
			return category;
		}
	}
}
