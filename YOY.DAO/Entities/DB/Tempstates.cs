using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Tempstates
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryFlag { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public decimal StateLatitude { get; set; }
        public decimal StateLongitude { get; set; }
    }
}
