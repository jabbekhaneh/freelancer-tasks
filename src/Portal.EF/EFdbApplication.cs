using Microsoft.EntityFrameworkCore;
using Portal.Domain.Projects;
using Portal.Domain.Users;

namespace Portal.EF;

public class EFdbApplication : DbContext
{
    public static string _ConnectionString { get; set; } = "data source =.; initial catalog =dbFreeLancerContext; integrated security = True; MultipleActiveResultSets=True";
    public bool UseSqlServer { get; set; }
    public EFdbApplication()
    {

    }
    public EFdbApplication(DbContextOptions options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (UseSqlServer)
            optionsBuilder.UseSqlServer(_ConnectionString); ;
        base.OnConfiguring(optionsBuilder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    #region Users
    public DbSet<User> Users { get; set; }
    #endregion
    #region Prjects
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    #endregion
}
