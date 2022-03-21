using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.Contracts.Identifiers;

public record AuditoriumId(AuditoriumNumber Number, BuildingId BuildingId);
