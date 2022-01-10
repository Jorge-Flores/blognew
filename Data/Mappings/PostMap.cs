using BlogNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogNew.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //Tabela
            builder.ToTable("Post");

            //Chave primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades 

            builder.Property(x => x.LastUpdateDate)
            .IsRequired()
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME")
            //.HasDefaultValueSql("GETDATE()") // para que o banco defina o valor
            .HasDefaultValue(DateTime.Now.ToUniversalTime()); // indicado usar UTC

            //índices 
            builder
            .HasIndex(x => x.Slug, "IX_Post_Slug")
            .IsUnique();

            //Relacionamentos 

            builder.HasOne(x => x.Author)
            .WithMany(x => x.Posts)
            .HasConstraintName("FK_Post_Author")
            .OnDelete(DeleteBehavior.Cascade);

            // One to Many - Um para muitos 

            builder.HasOne(x => x.Category)
           .WithMany(x => x.Posts)
           .HasConstraintName("FK_Post_Category")
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tags)
            .WithMany(x => x.Posts)
            // gera uma tabela virtual baseada no Dicionario que recebe uma 
            //String(nome da tabela e um objeto ( 2 objetos post e tag))
            .UsingEntity<Dictionary<string, object>>
                ("PostTag",
                    post => post.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .HasConstraintName("FK_PostTag_Post_id")
                    .OnDelete(DeleteBehavior.Cascade),
                     tag => tag.HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_PostTag_Tag_id")
                    .OnDelete(DeleteBehavior.Cascade));





        }
    }
}