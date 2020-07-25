using System;
using System.Collections.Generic;
using YOY.DTO.Entities.Misc.Structure.POCO;

namespace YOY.ThirdpartyServices.Builders.Email
{
    public class EmailBuilder
    {
        public string Build(string html, List<Pair<string, string>> substitutions)
        {

            foreach (Pair<string, string> item in substitutions)
            {
                html = html.Replace(item.Key, item.Value);
            }

            return html;
        }
    }
}
