using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.AcademyAggregate;

public class Academy : StructureUnit<AcademyId>, IAggregateRoot<AcademyId>
{
    internal Academy(AcademyId id, string title) : base(id, title) { }

    public bool IsDisbanded { get; private set; }

    private readonly List<Faculty> _faculties = new List<Faculty>();
    public IReadOnlyCollection<Faculty> Faculties => _faculties.AsReadOnly();

    public Faculty CreateFaculty(FacultyId id, string title)
    {
        if (IsDisbanded) throw new AcademyAlreadyDisbandedException(this);
        var faculty = new Faculty(id, this, title);
        if (_faculties.Any(f => f.Equals(faculty)))
            throw new FacultyAlreadyExistException(title, this);
        _faculties.Add(faculty);
        AddDomainEvent(new FacultyCreatedEvent(faculty));
        return faculty;
    }

    public void Disband()
    {
        if (IsDisbanded) throw new AcademyAlreadyDisbandedException(this);
        IsDisbanded = true;
        AddDomainEvent(new AcademyDisbandedEvent(this));
    }

    internal void DisbandFaculty(FacultyId id)
    {
        if (IsDisbanded) throw new AcademyAlreadyDisbandedException(this);
        var faculty = _faculties.SingleOrDefault(b => b.Id == id);
        _faculties.Remove(faculty);
    }

}
