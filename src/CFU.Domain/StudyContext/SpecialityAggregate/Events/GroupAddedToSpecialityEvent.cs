namespace CFU.Domain.StudyContext.SpecialityAggregate;

public record GroupAddedToSpecialityEvent(Speciality Speciality, Group Group) : DomainEvent;
