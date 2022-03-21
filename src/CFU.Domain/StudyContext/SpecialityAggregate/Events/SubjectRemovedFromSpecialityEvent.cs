namespace CFU.Domain.StudyContext.SpecialityAggregate;

public record SubjectRemovedFromSpecialityEvent(Speciality Speciality, Subject Subject) : DomainEvent;
