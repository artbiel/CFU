namespace CFU.Domain.StudyContext.SpecialityAggregate;

public record GroupRemovedFromSpecialityEvent(Speciality Speciality, Group Group) : DomainEvent;
