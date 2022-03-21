using CFU.Domain.ScheduleContext.PairsTimetableAggregate;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public record struct PairId(PairOrder Order, DayOfWeek DayOfWeek);

public abstract class Pair : Entity<PairId>
{
    protected Pair(PairId pairId, ClassSchedule classSchedule, Subject subject) : base(pairId)
    {
        Subject = Guard.Against.Default(subject, nameof(subject));
        ClassScheduleId = Guard.Against.Default(classSchedule, nameof(classSchedule)).Id;
    }

    public ClassScheduleId ClassScheduleId { get; private set; }
    public Subject Subject { get; private init; }
}

//internal class PairsEqualityComparer : IEqualityComparer<Pair>
//{
//    public bool Equals(Pair x, Pair y) => x.Id == y.Id && x.Subject == y.Subject
//        && x is AttendedPair att1 && y is AttendedPair att2 ? att1.ClassRoom == att2.ClassRoom :
//        x is RemotePair rem1 && y is RemotePair rem2 ? rem1.Url == rem2.Url : false;
//        /* && x.Subject.TeacherId == y.Subject.TeacherId*/

//    public int GetHashCode([DisallowNull] Pair obj) => obj.Id.GetHashCode() ^ obj.Subject.GetHashCode()/* ^ obj.Subject.TeacherId.GetHashCode()*/;
//}
