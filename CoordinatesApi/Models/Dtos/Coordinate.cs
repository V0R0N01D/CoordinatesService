using System.Text.Json.Serialization;

namespace CoordinatesApi.Models.Dtos;

public class Coordinate
{
    // широта (90)
    [JsonPropertyName("Latitude")]
    public double Latitude { get; set; }
    // долгота (180)

    [JsonPropertyName("Longitude")]
    public double Longitude { get; set; }
}
