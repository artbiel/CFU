namespace CFU.Domain.StudyContext.SpecialityAggregate;

public record SubjectAddedToSpecialityEvent(Speciality Speciality, Subject Subject) : DomainEvent;
