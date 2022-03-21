namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class AttendedPair : Pair
{
    public AttendedPair(PairId pairId, ClassSchedule classSchedule, Subject subject, AuditoriumId classRoom)
        : base(pairId, classSchedule, subject)
    {
        ClassRoom = classRoom;
    }

    public AuditoriumId ClassRoom { get; private set; }

    public override bool Equals(object obj)
        => obj is AttendedPair p && Id == p.Id && Subject == p.Subject && ClassRoom == p.ClassRoom;

    public override int GetHashCode() => Id.GetHashCode();

    //public override Task<IntersectionResult> Intersects(IQueryable<Pair> lessons)
    //{
    //    throw new NotImplementedException();
    //}

    /////<exception cref = "ArgumentNullException" ></ exception >
    //public override async Task<IntersectionResult> Intersects(IQueryable<Pair> lessons)
    //{
    //    if (other is null)
    //        throw new ArgumentNullException(nameof(other));

    //    bool pairEquals = Pair.Equals(other.Pair);
    //    bool subjectEquals = Subject.Equals(other.Subject);
    //    bool teachersIntersects = Teachers.Intersect(other.Teachers).Any();
    //    bool groupsIntersects = Groups.Intersect(other.Groups).Any();

    //    if (other is AttendedPair attended)
    //    {
    //        bool roomEquals = Room.Equals(attended.Room);
    //        //если обе пары очные, они совпадают по времени и аудитории, но не совпадают по дисциплине, то конфликт аудиторий
    //        if (pairEquals && roomEquals && !subjectEquals)
    //            return IntersectionResult.RoomIntersect;
    //    }
    //    else
    //    {
    //        // если одна пара очная, а другая - дистанционная, и они совпадают по времени и пересекаются по группам либо
    //        // преподам, то конфликт типов пар
    //        if (pairEquals && (groupsIntersects || teachersIntersects))
    //            return IntersectionResult.LessonTypeIntersect;
    //    }
    //    //если пары совпадают по времени, но не по предмету, и пересекаются по преподам, то конфликт преподов
    //    if (pairEquals && !subjectEquals && teachersIntersects)
    //        return IntersectionResult.TeachersIntersect;
    //    //иначе всё хорошо
    //    return IntersectionResult.NotIntersect;
    //}
}

