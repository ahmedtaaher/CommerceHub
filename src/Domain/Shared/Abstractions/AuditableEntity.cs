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

    public bool IsDeleted { get; private set; }

    public Guid? DeletedBy { get; private set; }

    public DateTime? DeletedAt { get; private set; }

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

    public void SoftDelete()
    {
      if (IsDeleted)
        return;

      IsDeleted = true;
    }

    public void Restore()
    {
      if (!IsDeleted)
        return;

      IsDeleted = false;
    }

    public void SetDeletedAudit(Guid? userId, DateTime deletedAt)
    {
      DeletedBy = userId;
      DeletedAt = deletedAt;
    }
  }
}