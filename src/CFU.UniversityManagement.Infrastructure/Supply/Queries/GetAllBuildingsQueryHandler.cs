using CFU.UniversityManagement.Application.Supply.DTOs;
using CFU.UniversityManagement.Application.Supply.Queries;
using CFU.UniversityManagement.Infrastructure.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CFU.UniversityManagement.Infrastructure.Supply.Queries;

public class GetAllBuildingsQueryHandler : IRequestHandler<GetAllBuildingsQuery, IEnumerable<BuildingDTO>>
{
    private readonly string _connectionString = string.Empty;

    public GetAllBuildingsQueryHandler(IOptions<ConnectionStrings> connectionString)
    {
        _connectionString = connectionString.Value.DefaultConnection;
    }

    public async Task<IEnumerable<BuildingDTO>> Handle(GetAllBuildingsQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        return await GetBuildings(connection);
    }

    private static Task<IEnumerable<BuildingDTO>> GetBuildings(IDbConnection connection) =>
        connection.QueryAsync<BuildingDTO, AddressDTO, BuildingDTO>(
            @"SELECT Id, City, Street, BuildingNumber, Block 
                    FROM buildings
                    WHERE IsDecommissioned = 0",
            (building, address) => {
                building.Address = address;
                return building;
            }, splitOn: "Id, City");
}
