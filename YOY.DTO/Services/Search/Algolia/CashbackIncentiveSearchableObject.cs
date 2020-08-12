using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class CashbackIncentiveSearchableObject : SearchableObjectCore
    {
        public int ApplyType { set; get; }
        public string ApplyTypeName { set; get; }
        public string SearchClueKey { set; get; }
        public string Details { set; get; }
        public double IncentiveAmount { set; get; }
        public double PreviousAmount { set; get; }
    }
}
