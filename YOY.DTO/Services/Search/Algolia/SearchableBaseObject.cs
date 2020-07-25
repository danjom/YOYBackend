

namespace YOY.DTO.Services.Search.Algolia
{
    public class SearchableBaseObject : SearchableObjectData
    {
        public string commerceId { set; get; }
        public int type { set; get; }
        public string name { set; get; }
        public string category { set; get; }
        public string keywords { set; get; }
        public string details { set; get; }
        public int releaseDate { set; get; }
        public int expirationDate { set; get; }
    }
}
