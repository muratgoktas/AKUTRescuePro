namespace AKUTRescue.Core.Repositories;

public abstract class Entity<TId>:IEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string CreateBy { get; set; }
    public string UpdatedBy { get; set; }
    public string DeletedBy { get; set; }
    public bool Status { get; set; } = true;

    protected Entity() => CreateDate = DateTime.UtcNow;
}