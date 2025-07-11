namespace Domain.Models.BaseEntities
{
    public abstract class BaseEntity<TKey> : BaseEntityPrimaryKey<TKey>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        protected BaseEntity() => CreatedAt = DateTime.UtcNow;
    }
}
