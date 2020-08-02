using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Models.v1.Content.POCO;

namespace YOY.UserAPI.Models.v1.Content.SET
{
    public class ContentStructure
    {
        public bool HasOwner { set; get; }
        public Guid CellOwnerId { set; get; }
        public int CellsCount { set; get; }
        public int StructureType { set; get; }
        public int ViewAllAccessType { set; get; }
        public int MaxDisplayedCellsOnInitialStructure { set; get; }
        public int OnSelectMemberActionType { set; get; }
        public string StructureTitle { set; get; }
        public bool DisplayStructureTitle { set; get; }
        public List<Cell> Cells { set; get; }
    }
}
