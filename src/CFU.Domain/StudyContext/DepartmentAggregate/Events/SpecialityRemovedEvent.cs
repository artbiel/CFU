namespace CFU.Domain.StudyContext.DepartmentAggregate;

public record SpecialityRemovedEvent(Speciality Speciality) : DomainEvent;
