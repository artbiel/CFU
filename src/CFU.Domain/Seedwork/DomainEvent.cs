using MediatR;

namespace CFU.Domain.Seedwork;

public abstract record DomainEvent() : INotification;

