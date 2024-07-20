using JobListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobListAPI.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        //var userName = this._identityService.GetUserClaimByType(Constants.IdentityClaimTypes.Name) ?? string.Empty;
        var now = DateTime.UtcNow;
        var auditedEntities = ChangeTracker.Entries<IAuditable>()
            .Where(e => e.State == EntityState.Added 
                     || e.State == EntityState.Modified);

        foreach (var entity in auditedEntities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedDate = now;
                //entity.Entity.UserCreated = userName;
            }

            entity.Entity.UpdatedDate = now;
            //entity.Entity.UserModified = userName;
        }
    }


    public DbSet<Job> Jobs { get; set; }
}
