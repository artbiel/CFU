using CFU.UniversityManagement.Application.Structure.DTOs;
using CFU.UniversityManagement.Application.StructureContext.Queries;
using CFU.UniversityManagement.Infrastructure.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CFU.UniversityManagement.Infrastructure.Structure.Queries;

public class GetUniversityStructureQueryHandler : IRequestHandler<GetUniversityStructureQuery, UniversityStructureDTO>
{
    private readonly string _connectionString = string.Empty;

    public GetUniversityStructureQueryHandler(IOptions<ConnectionStrings> connectionString)
    {
        _connectionString = connectionString.Value.DefaultConnection;
    }

    public async Task<UniversityStructureDTO> Handle(GetUniversityStructureQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var academiesTask = GetAcademiesTask(connection);
        var institutesTask = GetInstitutesTask(connection);

        await Task.WhenAll(academiesTask, institutesTask);
        return new UniversityStructureDTO
        {
            Academies = academiesTask.Result.ToArray(),
            Institutes = institutesTask.Result.ToArray()
        };
    }

    private static Task<IEnumerable<AcademyDTO>> GetAcademiesTask(IDbConnection connection) =>
        connection.QueryAsync<AcademyDTO, FacultyDTO, DepartmentDTO, AcademyDTO>(
            @"SELECT a.Id, a.Title, f.FId, f.FTitle, f.DId, f.DTitle
                    FROM academies AS a
                    LEFT JOIN 
                    (SELECT fac.Id as FId, fac.Title as FTitle, d.Id as DId, d.Title as DTitle, fac.AcademyId
                    FROM faculties AS fac
                    LEFT JOIN departments AS d ON fac.Id = d.FacultyId) AS f ON a.Id = f.AcademyId
                    WHERE a.IsDisbanded = 0",
            (academy, faculty, department) => {
                if (faculty.Id != default && !academy.Faculties.Contains(faculty)) {
                    academy.Faculties.Add(faculty);
                    if (department.Id != default)
                        faculty.Departments.Add(department);
                }

                return academy;
            }, splitOn: "Id, FId, DId");

    private static Task<IEnumerable<InstituteDTO>> GetInstitutesTask(IDbConnection connection) =>
        connection.QueryAsync<InstituteDTO, DepartmentDTO, InstituteDTO>(
           @"SELECT i.Id, i.Title, d.Id, d.Title
                    FROM institutes AS i
                    LEFT JOIN departments AS d ON i.Id = d.InstituteId
                    WHERE i.IsDisbanded = 0",
           (institute, department) => {
               if (department is not null)
                   institute.Departments.Add(department);

               return institute;
           }, splitOn: "Id, Id");
}
