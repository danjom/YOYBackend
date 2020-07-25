
namespace YOY.DTO.Entities.Misc.Structure.POCO
{
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.Key = first;
            this.Value = second;
        }

        public T Key { get; set; }
        public U Value { get; set; }
    };
}
