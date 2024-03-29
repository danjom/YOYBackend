﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Facebook.POCO
{
    public class FacebookUserViewModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("profile_picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        public DateTime? BirthDay { get; set; }
    }
}
