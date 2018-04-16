using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlyingParrot.Models;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FlyingParrot.Controllers
{
	[RoutePrefix("api/Sound")]
	public class SoundController : ApiController
	{
		//public static List<Sound> sounds;

		[HttpGet]
		[Route("All")]
		public IEnumerable<Sound> GetAllSounds() {
			//sounds = Sound.LoadAll();
			//return sounds;
			return Sound.LoadAll();
		}

		[HttpGet]
		public Sound GetSoundsById(int id) {
			//return sounds.FirstOrDefault(x => x.Id == id);
			var sound = new Sound();
			sound.LoadData(id);
			return sound;
		}

        [HttpPost]
        public bool AddSoundToDb([FromBody] Sound NewSound) {
            return Sound.AddData(NewSound);
		}

        [HttpPost]
		[Route("Upload")]
		public int AddSoundFile([FromUri] string Input) {
            var task = this.Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream requestStream = task.Result;
            try
            {
                Stream fileStream = File.Create(HttpContext.Current.Server.MapPath("~/sounds/" + Input + ".mp3"));
                requestStream.CopyTo(fileStream);
                fileStream.Close();
                requestStream.Close();
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString))
            {
                con.Open();
                SqlCommand FetchId = new SqlCommand("SELECT [Id] FROM SOUNDS WHERE [Filename] = @Input", con);
                FetchId.Parameters.Add("@Input", SqlDbType.NVarChar).SqlValue = Input;
                int Id = (int) FetchId.ExecuteScalar();
                con.Close();
                return Id;
            }
        }

		[HttpGet]
		public IEnumerable<Sound> GetSoundsByCategory(int category) {
			var tempCategory = new Category() { Id = category };
			tempCategory.LoadSounds();
			return tempCategory.Sounds;
		}
	}
}
