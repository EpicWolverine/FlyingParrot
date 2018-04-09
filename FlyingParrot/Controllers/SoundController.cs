﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlyingParrot.Models;

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


        [HttpGet]
        public IEnumerable<Sound> GetSoundsByCategory(int category) {
            var tempCategory = new Category();
            tempCategory.LoadSounds(category);
            return tempCategory.Sounds;
        }

        [HttpPost]
        public bool AddSoundToDb(Sound NewSound) {
            return Sound.AddData(NewSound);
        }
	}
}
