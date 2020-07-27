

namespace YOY.DTO.Services.Search.Algolia
{
    public class UpdateGeneralContentSearchableObject : UpdateSearchableObjectCore
    {
        public string SearchKey { set; get; }
        public string Details { set; get; }
        public string MainCategory { set; get; }
        public string Categories { set; get; }
        public string Classifications { set; get; }
        public decimal Value { set; get; }
        public double CashbackPercentage { set; get; }
    }
}
