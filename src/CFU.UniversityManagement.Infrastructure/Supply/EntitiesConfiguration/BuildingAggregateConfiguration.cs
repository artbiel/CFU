using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.UniversityManagement.Infrastructure.Supply.EntitiesConfiguration;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("buildings");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(v => v.Id, v => new BuildingId(v));

        builder.HasQueryFilter(b => b.IsDecommissioned == false);

        builder.OwnsOne(b => b.Address, x => {
            x.WithOwner();
            x.Property(a => a.City)
                .IsRequired()
                .HasColumnName(nameof(Address.City));
            x.Property(a => a.Street)
                .IsRequired()
                .HasColumnName(nameof(Address.Street));
            x.Property(a => a.BuildingNumber)
                .IsRequired()
                .HasConversion(v => v.Value, v => new BuildingNumber(v))
                .HasColumnName(nameof(Address.BuildingNumber));
            x.Property(a => a.Block)
                .IsRequired()
                .HasConversion(v => v.Value, v => new BuildingBlock(v))
                .HasColumnName(nameof(Address.Block));
        });

        builder.Navigation(b => b.Address)
            .IsRequired();

        builder.Metadata.FindNavigation(nameof(Building.Auditoriums))?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(b => b.Auditoriums).AutoInclude();

        builder.Ignore(b => b.DomainEvents);

        builder.HasMany(b => b.Auditoriums)
            .WithOne()
            .HasForeignKey("BuildingId");
    }
}

public class AuditoriumConfiguration : IEntityTypeConfiguration<Auditorium>
{
    public void Configure(EntityTypeBuilder<Auditorium> builder)
    {
        builder.ToTable("auditoriums");

        builder.HasKey(x => new { x.BuildingId, x.Number });
        //builder.Property(x => x.Id)
        //    .HasConversion(v => new { BuildingId = v.BuildingId.Id, Number = v.Number.Value }, x => new AuditoriumId(new(x.Number), new(x.BuildingId)));

        builder.Property(x => x.BuildingId).HasConversion(v => v.Id, v => new BuildingId(v));
        builder.Property(x => x.Number).HasConversion(v => v.Value, v => new AuditoriumNumber(v));


        //builder.OwnsOne(x => x.Id, x =>
        //{
        //    x.HasKey(x => new { x.BuildingId, x.Number });
        //    x.Property(x => x.Number)
        //        .HasConversion(v => v.Value, v => new AuditoriumNumber(v))
        //        .HasColumnName("Number");
        //    x.Property(x => x.BuildingId)
        //        .HasConversion(v => v.Id, v => new BuildingId(v))
        //        .HasColumnName("BuildingId");
        //});

        builder.Ignore(a => a.DomainEvents);
    }
}

public class AdministrativeAuditoriumConfiguration : IEntityTypeConfiguration<AdministrativeAuditorium>
{
    public void Configure(EntityTypeBuilder<AdministrativeAuditorium> builder)
    {
        builder.ToTable("auditoriums");

        builder.HasBaseType<Auditorium>();

        builder.Property(a => a.StructureUnitId)
            .HasConversion(v => v.Id, v => new StructureUnitId(v));
    }
}

public class ClassRoomConfiguration : IEntityTypeConfiguration<ClassRoom>
{
    public void Configure(EntityTypeBuilder<ClassRoom> builder)
    {
        builder.ToTable("auditoriums");

        builder.HasBaseType<Auditorium>();

        builder.Property(c => c.Capacity)
            .HasConversion(v => v.Value, v => new ClassRoomCapacity(v));
    }
}

public class UnassignedAuditoriumConfiguration : IEntityTypeConfiguration<UnassignedAuditorium>
{
    public void Configure(EntityTypeBuilder<UnassignedAuditorium> builder)
    {
        builder.ToTable("auditoriums");

        builder.HasBaseType<Auditorium>();
    }
}
