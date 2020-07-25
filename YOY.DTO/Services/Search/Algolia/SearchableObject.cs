namespace YOY.DTO.Services.Search.Algolia
{
    public class SearchableObject
    {
        public string objectID { set; get; }
        public string commerceId { set; get; }
        public int type { set; get; }
        public string name { set; get; }
        public string icon { set; get; }
        public string classification { set; get; }
        public string category { set; get; }
        public string keywords { set; get; }
        public string details { set; get; }
        public int contentType { set; get; }
        public string isActive { set; get; }
        public int releaseDate { set; get; }
        public int expirationDate { set; get; }
        public string countryId { set; get; }
    }
}
