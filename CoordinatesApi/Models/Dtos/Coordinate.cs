using System.Text.Json.Serialization;

namespace CoordinatesApi.Models.Dtos;

public class Coordinate
{
    [JsonPropertyName("Latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("Longitude")]
    public double Longitude { get; set; }
}
