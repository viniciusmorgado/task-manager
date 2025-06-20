using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.UserName)
            .HasMaxLength(256);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.HasData(
                    new User
                    {
                        Id = "944cc574-7936-4e3f-8b59-56c4af209d6f",
                        UserName = "admin@taskmanager.com",
                        NormalizedUserName = "ADMIN@TASKMANAGER.COM",
                        Email = "admin@taskmanager.com",
                        NormalizedEmail = "ADMIN@TASKMANAGER.COM",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAIAAYagAAAAEDNp4hs5LoG14uSkMSi++QTc4IauP3GpOv/Xl4Uhnt6MerlT3evllIZvHsNnEFue3w==",
                        SecurityStamp = "KFH2DVXTLVQIXVPVAOWGGQU62PSGKRB4",
                        ConcurrencyStamp = "1f2226b9-feb7-49bb-8779-ddec32a6be9e",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    });
    }
}
