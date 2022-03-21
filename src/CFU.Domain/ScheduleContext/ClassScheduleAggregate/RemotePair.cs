namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class RemotePair : Pair
{
    public RemotePair(PairId pairId, ClassSchedule classSchedule, Subject subject, Url url)
        : base(pairId, classSchedule, subject)
    {
        Url = url;
    }

    public Url Url { get; private set; }

    public override bool Equals(object obj)
        => obj is RemotePair p && Id == p.Id && Subject == p.Subject && Url == p.Url;

    public override int GetHashCode() => Id.GetHashCode();

    //public override Task<IntersectionResult> Intersects(IQueryable<Pair> lessons)
    //{
    //    throw new NotImplementedException();
    //}

    ///<exception cref="ArgumentNullException"></exception>
    //public override async Task<IntersectionResult> Intersects(IQueryable<Pair> lessons)
    //{
    //    if (lessons is null)
    //        throw new ArgumentNullException(nameof(lessons));

    //    Func<IQueryable<Pair>, IQueryable<Pair>> pairEquals = (less) => less.Where(l => l.PairId == PairId);
    //    Func<IQueryable<Pair>, IQueryable<Pair>> subjectEquals = (less) => less.Where(l => l.SubjectId == SubjectId);
    //    var teachersIntersects = Teachers.Intersect(other.Teachers).Any();
    //    var groupsIntersects = Groups.Intersect(other.Groups).Any();
    //    // если пары совпадают по времени, но не по предмету, и пересекаются по преподам, то конфликт преподов
    //    var intersectedByTeachers = await lessons.
    //        Where(l =>
    //            l.PairId == PairId &&
    //            l.SubjectId != SubjectId &&
    //            l.Teachers.Intersect(Teachers)
    //        .Any())
    //        .ToArrayAsync();

    //    if (intersectedByTeachers.Any())
    //        return IntersectionResult(IntersectionStatus.TeachersIntersect, intersectedByTeachers);

    //    if (other is RemotePair lecture)
    //    {
    //        var uriEquals = Uri.Equals(lecture.Uri, StringComparison.OrdinalIgnoreCase);
    //        // если обе пары заочные, они совпадают по времени и ссылке, но не совпадают по дисциплине, то конфликт ссылок
    //        if (pairEquals && uriEquals && !subjectEquals)
    //            return IntersectionResult.UriIntersect;
    //    }
    //    else
    //    {
    //        // если одна пара очная, а другая - дистанционная, и они совпадают по времени и пересекаются по группам либо
    //        // преподам, то конфликт типов пар
    //        if (pairEquals && (groupsIntersects || teachersIntersects))
    //            return IntersectionResult.LessonTypeIntersect;
    //    }

    //    // иначе всё хорошо
    //    return IntersectionResult.NotIntersect;
    //}
}

public record Url
{
    public Url(string value)
    {
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new InvalidUrlFormatException(value);
        Value = value;
    }

    public string Value { get; }
}
