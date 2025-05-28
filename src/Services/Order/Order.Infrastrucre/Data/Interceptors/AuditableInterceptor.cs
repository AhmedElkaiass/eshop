using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Order.Infrastrucre.Data.Interceptors;

internal class AuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entity in context.ChangeTracker.Entries<IEntity>())
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedAt = DateTime.UtcNow;
                entity.Entity.CreatedBy = "System"; // Replace with actual user context if available
            }
            if (entity.State == EntityState.Added || entity.State == EntityState.Modified || entity.HasChangedOwinedEntity())
            {
                entity.Entity.LastModifiedAt = DateTime.UtcNow;
                entity.Entity.LastModifiedBy = "System"; // Replace with actual user context if available
            }
        }

    }
}
public static class EntityEntryExtensions
{
    public static bool HasChangedOwinedEntity(this EntityEntry entry)
    {
        return entry.References.Any(r => r.TargetEntry != null &&
        r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}