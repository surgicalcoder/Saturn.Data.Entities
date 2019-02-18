namespace GoLive.Saturn.Data.Entities
{
    public abstract class ScopedEntity<T> : Entity where T : Entity
    {
        public Ref<T> Scope { get; set; }
    }
}