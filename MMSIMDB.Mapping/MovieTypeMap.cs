using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMSIMDB.Domain.Entities;

namespace MMSIMDB.Mapping
{
    public class MovieTypeMap : IEntityTypeConfiguration<MovieType>
    {
        public void Configure(EntityTypeBuilder<MovieType> builder)
        {
            builder.Property<int>(p => p.ID)
                .ValueGeneratedNever()
                .HasColumnType("int");

            builder
                .ToTable(nameof(MovieType))
                .HasKey(p => p.ID);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }

    public class MovieUserRatingMap : IEntityTypeConfiguration<MovieUserRating>
    {
        public void Configure(EntityTypeBuilder<MovieUserRating> builder)
        {
            builder.Property<int>(p => p.ID)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder
                .ToTable(nameof(MovieUserRating))
                .HasKey(p => p.ID);

            builder.Property(p => p.UserID)
                .IsRequired()
                .HasMaxLength(450);

        }
    }
}