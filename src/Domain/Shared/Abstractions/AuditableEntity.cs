namespace Domain.Shared.Abstractions
{
  public abstract class AuditableEntity<TId> : BaseEntity<TId>
  {
    protected AuditableEntity()
    {
      
    }

    protected AuditableEntity(TId id) : base(id)
    {
      
    }

    public Guid? CreatedBy { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedAt { get; private set; }

    public void SetCreatedAudit(Guid? userId, DateTime createdAt)
    {
      CreatedBy = userId;
      CreatedAt = createdAt;
    }

    public void SetModifiedAudit(Guid? userId, DateTime modifiedAt)
    {
      LastModifiedBy = userId;
      LastModifiedAt = modifiedAt;
    }
  }
}