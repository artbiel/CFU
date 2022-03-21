using CFU.Domain.StructureContext.DepartmentAggregate;

using FacultyDepartment = CFU.Domain.StructureContext.FacultyAggregate.Department;
using InstituteDepartment = CFU.Domain.StructureContext.InstituteAggregate.Department;

namespace CFU.UniversityManagement.Infrastructure.Structure.EntitiesConfiguration;

public class DepartmentAggregateConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasQueryFilter(d => !d.IsDisbanded);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new DepartmentId(v));

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Ignore(a => a.DomainEvents);

        builder.HasOne<FacultyDepartment>()
            .WithOne()
            .HasForeignKey<Department>(o => o.Id);

        builder.HasOne<InstituteDepartment>()
            .WithOne()
            .HasForeignKey<Department>(o => o.Id);
    }
}
