using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class ExceptionLogging
    {
        public long Id { get; set; }
        public int Layer { get; set; }
        public string ThrownClass { get; set; }
        public string ExceptionMsg { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionUrl { get; set; }
        public DateTime Logdate { get; set; }
    }
}
