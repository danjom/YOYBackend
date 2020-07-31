using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Models.v1.Content.POCO;

namespace YOY.UserAPI.Models.v1.Content.SET
{
    public class ContentSet
    {
        public int CellsCount { set; get; }
        public int StructureType { set; get; }
        public int ViewAllAccessType { set; get; }
        public int OnSelectMemberActionType { set; get; }
        public string SetName { set; get; }
        public bool DisplaySetName { set; get; }
        public List<Cell> Slides { set; get; }
    }
}
