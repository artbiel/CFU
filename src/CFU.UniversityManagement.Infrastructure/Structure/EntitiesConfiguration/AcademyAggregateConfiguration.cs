using CFU.Domain.StructureContext.AcademyAggregate;

namespace CFU.UniversityManagement.Infrastructure.Structure.EntitiesConfiguration;

public class AcademyAggregateConfiguration : IEntityTypeConfiguration<Academy>
{
    public void Configure(EntityTypeBuilder<Academy> builder)
    {
        builder.ToTable("academies");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new AcademyId(v));

        builder.HasQueryFilter(a => a.IsDisbanded == false);

        builder.Metadata.FindNavigation(nameof(Academy.Faculties))?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(a => a.Faculties).AutoInclude();

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Ignore(a => a.DomainEvents);

        builder.HasMany(a => a.Faculties)
            .WithOne()
            .HasForeignKey(f => f.AcademyId);
    }
}

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("faculties");

        builder.Property(a => a.Title)
            .IsRequired();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new FacultyId(v));

        builder.Ignore(a => a.DomainEvents);
    }
}
