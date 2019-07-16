namespace GoLive.Saturn.Data.Entities
{
    public class WrappedEntity<T> : Entity
    {
        public T Item { get;set; }
    }
}