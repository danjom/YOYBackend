using System.Collections.Generic;

namespace YOY.DTO.Services.Email.SendGrid
{
    public class OutboundEmail
    {
        public string Subject { set; get; }
        public string PlainText { set; get; }
        public string HtmlContent { set; get; }
        public Entities.Misc.Structure.POCO.Pair<string, string> From { set; get; }
        public List<Entities.Misc.Structure.POCO.Pair<string, string>> Tos { set; get; }
        public Dictionary<string, string> Substitutions { set; get; }
    }
}
