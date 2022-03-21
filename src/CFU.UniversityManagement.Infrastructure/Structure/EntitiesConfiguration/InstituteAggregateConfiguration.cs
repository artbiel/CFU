using CFU.Domain.StructureContext.InstituteAggregate;

namespace CFU.UniversityManagement.Infrastructure.Structure.EntitiesConfiguration;

public class InstituteAggregateConfiguration : IEntityTypeConfiguration<Institute>
{
    public void Configure(EntityTypeBuilder<Institute> builder)
    {
        builder.ToTable("institutes");

        builder.HasQueryFilter(a => a.IsDisbanded == false);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new InstituteId(v));

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Metadata.FindNavigation(nameof(Institute.Departments))?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(a => a.Departments).AutoInclude();

        builder.Ignore(a => a.DomainEvents);

        builder.HasMany(a => a.Departments)
            .WithOne()
            .HasForeignKey(d => d.InstituteId);
    }
}

public class InstituteDepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.Property(a => a.Title)
            .IsRequired();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new DepartmentId(v));

        builder.Ignore(a => a.DomainEvents);
    }
}
