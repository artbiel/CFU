namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class InvalidUrlFormatException : ArgumentException
{
    public InvalidUrlFormatException(string url) : base($"Url '{url}' has incorrct format") { }
}
