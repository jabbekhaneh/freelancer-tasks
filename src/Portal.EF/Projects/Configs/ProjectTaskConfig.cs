using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Projects;

namespace Portal.EF.Projects.Configs;

public class ProjectTaskConfig : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> _)
    {
        _.HasIndex(_ => _.Id).IsUnique();
        _.Property(_ => _.Id).IsRequired();
        _.Property(_=>_.Title).IsRequired().HasMaxLength(255);
        _.Property(_=>_.StartDate).IsRequired();
        _.Property(_ => _.EndDate).IsRequired();
        _.Property(_=>_.ProjectId).IsRequired();

        _.HasOne(_ => _.Project).WithMany(_ => _.ProjectTasks)
            .HasForeignKey(_ => _.ProjectId);
    }
}
