using BlogNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogNew.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

            builder.Property(x => x.Bio);
            builder.Property(x => x.Image);
            builder.Property(x => x.Email);
            builder.Property(x => x.PasswordHash);


            builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
            //indices
            builder
            .HasIndex(x => x.Slug, "IX_User_Slug")
            .IsUnique();

            //Relacionamentos 

            builder.HasMany(x => x.Roles)
                      .WithMany(x => x.Users)
                      // gera uma tabela virtual baseada no Dicionario que recebe uma 
                      //String(nome da tabela e um objeto ( 2 objetos post e tag))
                      .UsingEntity<Dictionary<string, object>>
                          ("UserRole",
                              role => role.HasOne<Role>()
                              .WithMany()
                              .HasForeignKey("RoleId")
                              .HasConstraintName("FK_UserRole_Role_id")
                              .OnDelete(DeleteBehavior.Cascade),
                               user => user.HasOne<User>()
                              .WithMany()
                              .HasForeignKey("UserId")
                              .HasConstraintName("FK_UserRole_User_id")
                              .OnDelete(DeleteBehavior.Cascade));


        }
    }
}