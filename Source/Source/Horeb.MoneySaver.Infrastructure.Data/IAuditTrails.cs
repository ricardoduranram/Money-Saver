namespace Horeb.Infrastructure.Data;

public interface IAuditTrails
{
    DateTime UtcCreatedOn { get; set; }

    DateTime UtcLastestUpdateOn { get; set; }

    bool IsActive ();
}
