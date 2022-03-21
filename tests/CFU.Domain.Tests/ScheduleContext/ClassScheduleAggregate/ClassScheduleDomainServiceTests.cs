using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using CFU.Domain.ScheduleContext.PairsTimetableAggregate;
using CFU.Domain.SupplyContext.BuildingAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DayOfWeek = CFU.Domain.ScheduleContext.ClassScheduleAggregate.DayOfWeek;

// TODO тест для одинаковых пар с разными предметами или преподавателями

namespace CFU.Domain.UnitTests.ScheduleContext.ClassScheduleAggregate;

public class ClassScheduleDomainServiceTests
{
    [Fact]
    public void Constructor_ShouldCreateClassScheduleDomainService_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IClassScheduleRepository>();

        // Act
        var service = new ClassScheduleDomainService(repository);

        // Assert
        service.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new ClassScheduleDomainService(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [MemberData(nameof(GetNotIntersectedClassSchedule))]
    public async Task UpdateClassSchedule_ShouldUpdateClassSchedule_WhenPairsNotIntersect(
        ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] expectedPairsToAdd,
        Pair[] expectedPairsToRemove,
        Pair[] allPairs)
    {
        // Arrange
        var repository = Substitute.For<IClassScheduleRepository>();

        attendedPairs ??= Array.Empty<(PairId Id, Subject Subject, AuditoriumId AuditoriumId)>();
        remotePairs ??= Array.Empty<(PairId Id, Subject Subject, Url Url)>();

        var newPairs = new List<Pair>(attendedPairs.Length + remotePairs.Length);
        foreach (var pair in attendedPairs)
            newPairs.Add(new AttendedPair(pair.Id, classSchedule, pair.Subject, pair.AuditoriumId));
        foreach (var pair in remotePairs)
            newPairs.Add(new RemotePair(pair.Id, classSchedule, pair.Subject, pair.Url));

        repository.GetAllAsync(Arg.Any<ConflictingPairsSpecification>())
            .Returns(allPairs
                    .Where(p => new ConflictingPairsSpecification(newPairs)
                    .ToExpression()
                    .Compile()
                    .Invoke(p))
                    .Select(p => new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()))));

        var service = new ClassScheduleDomainService(repository);

        // Act
        await service.UpdateClassScheduleAsync(classSchedule,
            attendedPairs.Select(at => (at.Id, at.Subject.Id, at.AuditoriumId)).ToArray(),
            remotePairs.Select(rem => (rem.Id, rem.Subject.Id, rem.Url)).ToArray());

        // Assert
        if (expectedPairsToAdd.Any())
            classSchedule.Pairs.Should().Contain(expectedPairsToAdd);
        if (expectedPairsToRemove.Any())
            classSchedule.Pairs.Should().NotContain(expectedPairsToRemove);
        classSchedule.DomainEvents.Should().ContainEquivalentOf(new ClassScheduleUpdatedEvent(classSchedule));
    }

    [Theory]
    [MemberData(nameof(GetIntersectedByPairTimeAndTeacherButNotBySubject))]
    public Task UpdateClassSchedule_ShouldThrowClassSchedulesConflictException_WhenPairsIntersectsByPairTimeAndTeacherButNotBySubject(
         ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] allPairs)
            => CheckPairsIntersection(classSchedule, attendedPairs, remotePairs, allPairs);

    [Theory]
    [MemberData(nameof(GetPairsBothAreAttendedAndIntersectsByPairTimeAndClassRoomButNotBySubject))]
    public Task UpdateClassSchedule_ShouldThrowClassSchedulesConflictException_WhenPairsBothAreAttendedAndIntersectsByPairTimeAndClassRoomButNotBySubject(
        ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] allPairs)
            => CheckPairsIntersection(classSchedule, attendedPairs, remotePairs, allPairs);

    [Theory]
    [MemberData(nameof(GetPairsBothAreRemoteAndIntersectsByPairTimeAndUrlButNotBySubject))]
    public Task UpdateClassSchedule_ShouldThrowClassSchedulesConflictException_WhenPairsBothAreRemoteAndIntersectsByPairTimeAndUrlButNotBySubject(
        ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] allPairs)
            => CheckPairsIntersection(classSchedule, attendedPairs, remotePairs, allPairs);

    [Theory]
    [MemberData(nameof(GetOnePairIsAttendedAndAnotherIsRemoteAndIntersectsByPairTimeAndGroupsOrTeachersButNotBySubjects))]
    public Task UpdateClassSchedule_ShouldThrowClassSchedulesConflictException_WhenOnePairIsAttendedAndAnotherIsRemoteAndIntersectsByPairTimeAndGroupsOrTeachersButNotBySubjects(
         ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] allPairs)
            => CheckPairsIntersection(classSchedule, attendedPairs, remotePairs, allPairs);

    private static async Task CheckPairsIntersection(
         ClassSchedule classSchedule,
        (PairId Id, Subject Subject, AuditoriumId AuditoriumId)[] attendedPairs,
        (PairId Id, Subject Subject, Url Url)[] remotePairs,
        Pair[] allPairs)
    {
        // Arrange
        var repository = Substitute.For<IClassScheduleRepository>();

        attendedPairs ??= Array.Empty<(PairId Id, Subject Subject, AuditoriumId AuditoriumId)>();
        remotePairs ??= Array.Empty<(PairId Id, Subject Subject, Url Url)>();

        var newPairs = new List<Pair>(attendedPairs.Length + remotePairs.Length);
        foreach (var pair in attendedPairs)
            newPairs.Add(new AttendedPair(pair.Id, classSchedule, pair.Subject, pair.AuditoriumId));
        foreach (var pair in remotePairs)
            newPairs.Add(new RemotePair(pair.Id, classSchedule, pair.Subject, pair.Url));

        repository.GetAllAsync(Arg.Any<ConflictingPairsSpecification>())
            .Returns(allPairs
                    .Where(p => new ConflictingPairsSpecification(newPairs)
                    .ToExpression()
                    .Compile()
                    .Invoke(p))
                    .Select(p => new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()))));

        var service = new ClassScheduleDomainService(repository);

        // Act
        var act = () => service.UpdateClassScheduleAsync(classSchedule,
            attendedPairs.Select(at => (at.Id, at.Subject.Id, at.AuditoriumId)).ToArray(),
            remotePairs.Select(rem => (rem.Id, rem.Subject.Id, rem.Url)).ToArray());

        // Assert
        await act.Should().ThrowAsync<ClassSchedulesConflictException>();
    }

    public static TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url)[],
        Pair[],
        Pair[],
        Pair[]> GetNotIntersectedClassSchedule()
    {
        var classSchedule = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var buildingId = new BuildingId(Guid.NewGuid());
        var subject = new Subject(new SubjectId(Guid.NewGuid()), new TeacherId(Guid.NewGuid()));
        classSchedule.AddSubject(subject);
        var pairId = new PairId(new PairOrder(1), DayOfWeek.Monday);
        var classRoom1 = new AuditoriumId(new AuditoriumNumber(1), buildingId);
        var classRoom2 = new AuditoriumId(new AuditoriumNumber(2), buildingId);

        var newAttendedPair = new AttendedPair(pairId, classSchedule, subject, classRoom1);
        var oldAttendedPair = new AttendedPair(pairId, classSchedule, subject, classRoom2);
        classSchedule.AddPair(oldAttendedPair);

        return new TheoryData<ClassSchedule,
            (PairId, Subject, AuditoriumId)[],
            (PairId, Subject, Url)[],
            Pair[],
            Pair[],
            Pair[]>
            {
                {
                    classSchedule,
                    new (PairId, Subject, AuditoriumId)[] { (pairId, subject, classRoom1)},
                    default!,
                    new Pair [] { newAttendedPair },
                    new Pair[] { oldAttendedPair },
                    new Pair[] { oldAttendedPair}
                }
            };
    }

    public static TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url)[],
        Pair[]> GetIntersectedByPairTimeAndTeacherButNotBySubject()
    {
        var classSchedule = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var buildingId = new BuildingId(Guid.NewGuid());
        var teacherId = new TeacherId(Guid.NewGuid());
        var pairId = new PairId(new PairOrder(1), DayOfWeek.Monday);
        var classRoom1 = new AuditoriumId(new AuditoriumNumber(1), buildingId);
        var classRoom2 = new AuditoriumId(new AuditoriumNumber(2), buildingId);
        var url1 = new Url("https://example1.com");
        var url2 = new Url("https://example2.com");
        var subject1 = new Subject(new SubjectId(Guid.NewGuid()), teacherId);
        var subject2 = new Subject(new SubjectId(Guid.NewGuid()), teacherId);
        classSchedule.AddSubject(subject1);
        classSchedule.AddSubject(subject2);
        var existingRemotePair1 = new RemotePair(pairId, classSchedule, subject2, url1);
        var existingRemotePair2 = new RemotePair(pairId, classSchedule, subject2, url2);
        var existingAttendedPair = new AttendedPair(pairId, classSchedule, subject2, classRoom2);
        classSchedule.AddPair(existingRemotePair1);
        classSchedule.AddPair(existingRemotePair2);
        classSchedule.AddPair(existingAttendedPair);
        return new TheoryData<ClassSchedule,
            (PairId, Subject, AuditoriumId)[],
            (PairId, Subject, Url)[],
            Pair[]>
            {
                {
                    classSchedule,
                    new (PairId, Subject, AuditoriumId)[] { (pairId, subject1, classRoom1) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { existingRemotePair1 }
                },
                {
                    classSchedule,
                    new (PairId, Subject, AuditoriumId)[] { (pairId, subject1, classRoom1) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { existingAttendedPair }
                },
                {
                    classSchedule,
                    Array.Empty<(PairId, Subject, AuditoriumId)>(),
                    new (PairId, Subject, Url)[] { (pairId, subject1, url1) },
                    new Pair[] { existingRemotePair2 }
                }
            };
    }

    public static TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url)[],
        Pair[]> GetPairsBothAreAttendedAndIntersectsByPairTimeAndClassRoomButNotBySubject()
    {
        var classSchedule = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var buildingId = new BuildingId(Guid.NewGuid());
        var teacherId1 = new TeacherId(Guid.NewGuid());
        var teacherId2 = new TeacherId(Guid.NewGuid());
        var pairId = new PairId(new PairOrder(1), DayOfWeek.Monday);
        var classRoom = new AuditoriumId(new AuditoriumNumber(1), buildingId);
        var subject1 = new Subject(new SubjectId(Guid.NewGuid()), teacherId1);
        var subject2 = new Subject(new SubjectId(Guid.NewGuid()), teacherId1);
        var subject3 = new Subject(new SubjectId(Guid.NewGuid()), teacherId2);
        classSchedule.AddSubject(subject1);
        classSchedule.AddSubject(subject2);
        classSchedule.AddSubject(subject3);
        var existingAttendedPair1 = new AttendedPair(pairId, classSchedule, subject2, classRoom);
        var existingAttendedPair2 = new AttendedPair(pairId, classSchedule, subject3, classRoom);
        classSchedule.AddPair(existingAttendedPair1);
        classSchedule.AddPair(existingAttendedPair2);

        return new TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url Url)[],
        Pair[]>
            {
                 {
                    classSchedule,
                    new (PairId Id, Subject SubjectId, AuditoriumId AuditoriumId)[] { (pairId, subject1, classRoom) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { existingAttendedPair1 }
                 },
                 {
                    classSchedule,
                    new (PairId Id, Subject SubjectId, AuditoriumId AuditoriumId)[] { (pairId, subject1, classRoom) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { existingAttendedPair2 }
                 }
            };
    }

    public static TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url)[],
        Pair[]> GetPairsBothAreRemoteAndIntersectsByPairTimeAndUrlButNotBySubject()
    {
        var classSchedule = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var teacherId1 = new TeacherId(Guid.NewGuid());
        var teacherId2 = new TeacherId(Guid.NewGuid());
        var pairId = new PairId(new PairOrder(1), DayOfWeek.Monday);
        var subject1 = new Subject(new SubjectId(Guid.NewGuid()), teacherId1);
        var subject2 = new Subject(new SubjectId(Guid.NewGuid()), teacherId1);
        var subject3 = new Subject(new SubjectId(Guid.NewGuid()), teacherId2);
        classSchedule.AddSubject(subject1);
        classSchedule.AddSubject(subject2);
        classSchedule.AddSubject(subject3);
        var uri = new Url("https://example.com");
        var existingRemotePair1 = new RemotePair(pairId, classSchedule, subject2, uri);
        var existingRemotePair2 = new RemotePair(pairId, classSchedule, subject3, uri);
        classSchedule.AddPair(existingRemotePair1);
        classSchedule.AddPair(existingRemotePair2);

        return new TheoryData<ClassSchedule,
            (PairId, Subject, AuditoriumId)[],
            (PairId, Subject, Url)[],
            Pair[]>
            {
                {
                    classSchedule,
                    Array.Empty<(PairId, Subject, AuditoriumId)>(),
                    new (PairId Id, Subject SubjectId, Url Url)[] { (pairId, subject1, uri) },
                    new Pair[] { existingRemotePair1 }
                },
                {
                    classSchedule,
                    Array.Empty<(PairId, Subject, AuditoriumId)>(),
                    new (PairId Id, Subject SubjectId, Url Url)[] {  (pairId, subject1, uri) },
                    new Pair[] { existingRemotePair2 }
                },
            };
    }

    public static TheoryData<ClassSchedule,
        (PairId, Subject, AuditoriumId)[],
        (PairId, Subject, Url)[],
        Pair[]> GetOnePairIsAttendedAndAnotherIsRemoteAndIntersectsByPairTimeAndGroupsOrTeachersButNotBySubjects()
    {
        var classSchedule1 = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var classSchedule2 = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), new GroupId(Guid.NewGuid()));
        var teacherId1 = new TeacherId(Guid.NewGuid());
        var teacherId2 = new TeacherId(Guid.NewGuid());
        var pairId = new PairId(new PairOrder(1), DayOfWeek.Monday);
        var subjectId1 = new SubjectId(Guid.NewGuid());
        var subjectId2 = new SubjectId(Guid.NewGuid());
        var subject1 = new Subject(subjectId1, teacherId1);
        var subject2 = new Subject(subjectId2, teacherId2);
        var subject3 = new Subject(subjectId2, teacherId1);
        classSchedule1.AddSubject(subject1);
        classSchedule1.AddSubject(subject2);
        classSchedule2.AddSubject(subject3);
        var classRoom = new AuditoriumId(new AuditoriumNumber(1), new BuildingId(Guid.NewGuid()));
        var url = new Url("https://example.com");
        //var existingAttendedPair1 = new AttendedPair(pairId, classSchedule1, subject1, classRoom);
        //classSchedule1.AddPair(existingAttendedPair1);
        // Intersection by groups
        var remotePair1 = new RemotePair(pairId, classSchedule1, subject2, url);
        classSchedule1.AddPair(remotePair1);
        // Intersection by teachers
        var remotePair2 = new RemotePair(pairId, classSchedule2, subject3, url);
        classSchedule2.AddPair(remotePair2);

        return new TheoryData<ClassSchedule,
            (PairId, Subject, AuditoriumId)[],
            (PairId, Subject, Url)[],
            Pair[]>
            {
                {
                    classSchedule1,
                    new (PairId, Subject, AuditoriumId)[] { (pairId, subject1, classRoom) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { remotePair1 }
                },
                {
                    classSchedule1,
                    new (PairId, Subject, AuditoriumId)[] { (pairId, subject1, classRoom) },
                    Array.Empty<(PairId, Subject, Url)>(),
                    new Pair[] { remotePair2 }
                }
            };
    }
}
