using CFU.UniversityManagement.WebAPI.Endpoints.Structure.Academies;
using CFU.UniversityManagement.WebAPI.Routes;
using FluentAssertions;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CFU.UniversityManagement.IntegrationTests.Structure;

public class StructureEndpointTests
{
    [Fact]
    public async Task GetStructure_ShouldReturnOk()
    {
        // Arrange
        await using var factory = new StructureBaseWebApplicationFactory();
        var host = factory.WithWebHostBuilder(_ => { });
        var client = host.CreateClient();

        // Act
        var response = await client.GetAsync(StructureRoutes.Structure);

        var structureDTO = (await response.Content.ReadFromJsonAsync<GetUniversityStructureResponse>()).UniversityStructure;

        // Assert
        response.EnsureSuccessStatusCode();
        structureDTO.Should().NotBeNull();
        structureDTO!.Academies.Should().ContainSingle();
        structureDTO!.Institutes.Should().BeEmpty();
    }
}
