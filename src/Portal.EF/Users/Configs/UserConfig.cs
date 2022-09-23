using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Users;

namespace Portal.EF.Users.Configs;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> _)
    {
        _.HasKey(_ => _.Id);
        _.Property(_ => _.Id).IsRequired();
        _.Property(_ => _.FirstName).IsRequired().HasMaxLength(255);
        _.Property(_ => _.LastName).IsRequired().HasMaxLength(255);
        _.Property(_=>_.UserName).IsRequired().HasMaxLength(255);

        _.HasMany(_ => _.Projects).WithOne(_ => _.User)
            .HasForeignKey(_ => _.UserId);
        
    }
}
