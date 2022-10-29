using System.ComponentModel.DataAnnotations;

namespace Horeb.Infrastructure.Data;

/// <summary>
///     Provides a base class for your objects which will be persisted to the database.
/// </summary>
public abstract class BaseEntity : Identity<int>, IAuditTrails
{
    [Required]
    public DateTime UtcCreatedOn { get; set; } = DateTime.Now.ToUniversalTime();

    [Required]
    public DateTime UtcLastestUpdateOn { get; set; } = DateTime.Now.ToUniversalTime();

    public bool IsActive () => !this.IsDeleted;

    public bool IsDeleted { get; set; }
}
