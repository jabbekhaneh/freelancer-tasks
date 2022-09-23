using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Projects;

namespace Portal.EF.Projects.Configs;

internal class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> _)
    {
        _.HasKey(_ => _.Id);
        _.Property(_ => _.Id).IsRequired();
        _.Property(_=>_.Title).IsRequired()
            .HasMaxLength(255);
        _.Property(_ => _.StartDate).IsRequired();
        _.Property(_ => _.EndDate).IsRequired();
        _.Property(_=>_.PriceTask).IsRequired();
        _.Property(_=>_.UserId).IsRequired();

        _.HasOne(_ => _.User).WithMany(_ => _.Projects)
              .HasForeignKey(_ => _.UserId);
    }
}
