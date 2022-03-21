using System.Linq.Expressions;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public record ConflictingPairsSpecification : Specification<Pair>
{
    private readonly IEnumerable<Pair> _pairs;

    public ConflictingPairsSpecification(IEnumerable<Pair> pairs)
    {
        _pairs = pairs;
    }

    public override Expression<Func<Pair, bool>> ToExpression() =>
        pair => _pairs.Any(added => added.Id == pair.Id &&
            //если пары совпадают по времени и преподавателю, но не по дисциплине
            (added.Subject.Id != pair.Subject.Id && added.Subject.TeacherId == pair.Subject.TeacherId ||
            (added is AttendedPair ?
                pair is AttendedPair ?
                    //если обе пары очные, они совпадают по времени и аудитории, но не совпадают по дисциплине
                    added.Subject.Id != pair.Subject.Id && (added as AttendedPair).ClassRoom == (pair as AttendedPair).ClassRoom
                // если одна пара очная, а другая - дистанционная, и они совпадают по времени + по группам либо преподавателям, но не по дисциплине
                : added.Subject.Id != pair.Subject.Id && (added.Subject.TeacherId == pair.Subject.TeacherId || added.ClassScheduleId == pair.ClassScheduleId)
            : pair is RemotePair ?
                    // если обе пары заочные, они совпадают по времени и url, но не совпадают по дисциплине
                    added.Subject.Id != pair.Subject.Id && (added as RemotePair).Url == (pair as RemotePair).Url
                // если одна пара очная, а другая - дистанционная, и они совпадают по времени + по группам либо преподавателям, но не по дисциплине
                : added.Subject.Id != pair.Subject.Id && (added.Subject.TeacherId == pair.Subject.TeacherId || added.ClassScheduleId == pair.ClassScheduleId)
            ))
        );
}
