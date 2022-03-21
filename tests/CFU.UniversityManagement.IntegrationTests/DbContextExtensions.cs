using CFU.Domain.Contracts.Identifiers;
using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.UniversityManagement.Infrastructure;
using System;

namespace CFU.UniversityManagement.IntegrationTests;

public static class DbContextExtensions
{
    public static void Inititalize(this UniversityManagementDBContext context)
    {
        var academy = new Academy(new AcademyId(Guid.NewGuid()), "academy");
        context.Academies.Add(academy);
        context.SaveChanges();
    }
}
