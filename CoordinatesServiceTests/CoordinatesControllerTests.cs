using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CoordinatesApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CoordinatesServiceTests;

public class CoordinatesControllerTests
    : IClassFixture<WebApplicationFactory<CoordinatesApi.Program>>
{
    private readonly WebApplicationFactory<CoordinatesApi.Program> _factory;
    private readonly HttpClient _client;

    public CoordinatesControllerTests(WebApplicationFactory<CoordinatesApi.Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public async Task GetCoordinates_WithCountGreaterThanZero_ReturnsNonEmptyArray(int count)
    {
        var response = await _client.GetAsync($"/coordinates?count={count}");

        response.EnsureSuccessStatusCode();
        var coordinates = await response.Content.ReadFromJsonAsync<List<Coordinate>>();

        Assert.NotNull(coordinates);
        Assert.Equal(count, coordinates.Count);
        Assert.All(coordinates, coordinate =>
        {
            Assert.InRange(coordinate.Latitude, -90.0, 90.0);
            Assert.InRange(coordinate.Longitude, -180.0, 180.0);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task GetCoordinates_WithCountLessThanOne_ReturnsBadRequest(int count)
    {
        var response = await _client.GetAsync($"/coordinates?count={count}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CalculateDistance_WithMultipleCoordinates_ReturnsNonZeroDistance()
    {
        var coordinates = new List<Coordinate>
        {
            new() { Latitude = 60.021158, Longitude = 30.321135 },
            new() { Latitude = 60.024157, Longitude = 30.323133 },
            new() { Latitude = 60.051155, Longitude = 30.341132 }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(coordinates),
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/coordinates", content);

        response.EnsureSuccessStatusCode();
        var distanceResult = await response.Content.ReadFromJsonAsync<DistanceResult>();

        Assert.NotNull(distanceResult);
        Assert.True(distanceResult.Metres > 0);
        Assert.True(distanceResult.Miles > 0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public async Task CalculateDistance_WithOneOrZeroCoordinates_ReturnsZeroDistance(
        int coordinateCount)
    {
        List<Coordinate> coordinates = new();

        if (coordinateCount == 1)
            coordinates.Add(new Coordinate { Latitude = 60.021158, Longitude = 30.321135 });

        var content = new StringContent(
            JsonSerializer.Serialize(coordinates),
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/coordinates", content);

        response.EnsureSuccessStatusCode();
        var distanceResult = await response.Content.ReadFromJsonAsync<DistanceResult>();

        Assert.NotNull(distanceResult);
        Assert.Equal(0, distanceResult.Metres);
        Assert.Equal(0, distanceResult.Miles);
    }

    [Fact]
    public async Task CalculateDistance_WithNullCoordinates_ReturnsZeroDistance()
    {
        var response = await _client.PostAsync("/coordinates", null);

        response.EnsureSuccessStatusCode();
        var distanceResult = await response.Content.ReadFromJsonAsync<DistanceResult>();

        Assert.NotNull(distanceResult);
        Assert.Equal(0, distanceResult.Metres);
        Assert.Equal(0, distanceResult.Miles);
    }
}