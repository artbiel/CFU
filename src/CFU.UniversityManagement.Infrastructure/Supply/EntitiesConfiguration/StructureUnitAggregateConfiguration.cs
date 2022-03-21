using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.UniversityManagement.Infrastructure.Supply.EntitiesConfiguration;

public class StructureUnitAggregateConfiguration : IEntityTypeConfiguration<StructureUnit>
{
    public void Configure(EntityTypeBuilder<StructureUnit> builder)
    {
        builder.ToTable("structure-units");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new StructureUnitId(v));
        builder.Property(x => x.BuildingId).HasConversion(v => v.Id, v => new BuildingId(v));
        builder.Property(x => x.AuditoriumNumber).HasConversion(v => v.Value, v => new AuditoriumNumber(v));

        builder.Ignore(b => b.DomainEvents);

        builder.HasOne<AdministrativeAuditorium>()
            .WithOne()
            .HasForeignKey<AdministrativeAuditorium>(nameof(AdministrativeAuditorium.StructureUnitId));
    }
}
