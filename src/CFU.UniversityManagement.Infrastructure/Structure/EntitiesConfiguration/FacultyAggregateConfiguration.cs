using CFU.Domain.StructureContext.FacultyAggregate;

using AcademyFaculty = CFU.Domain.StructureContext.AcademyAggregate.Faculty;
using InstituteDepartment = CFU.Domain.StructureContext.InstituteAggregate.Department;

namespace CFU.UniversityManagement.Infrastructure.Structure.EntitiesConfiguration;

public class FacultyAggregateConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("faculties");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new FacultyId(v));

        builder.Metadata.FindNavigation(nameof(Faculty.Departments))!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Navigation(a => a.Departments).AutoInclude();

        builder.Ignore(a => a.DomainEvents);

        builder.HasMany(a => a.Departments)
            .WithOne()
            .HasForeignKey(d => d.FacultyId);

        builder.HasOne<AcademyFaculty>()
            .WithOne()
            .HasForeignKey<Faculty>(o => o.Id);

    }
}

public class FacultyDepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.Property(a => a.Title)
            .IsRequired();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new DepartmentId(v));

        builder.Ignore(a => a.DomainEvents);

        builder.HasOne<InstituteDepartment>()
            .WithOne()
            .HasForeignKey<Department>(o => o.Id);
    }
}
