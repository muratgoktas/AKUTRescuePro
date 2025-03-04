namespace AKUTRescue.Core.Repositories;

public abstract class Entity<TId>:IEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
     public bool Status { get; set; } = true;
    public string CreatedByWho { get; set; }= "CreatedNoName";
    public string UpdatedByWho { get; set; }= "UpdatedNoName";
    public string DeletedByWho { get; set; } = "DeletedNoName";

    protected Entity() => CreateDate = DateTime.UtcNow;
}