using System.Text.Json.Serialization;

namespace CoordinatesApi.Models.Dtos;

public class DistanceResult
{
    [JsonPropertyName("Metres")]
    public double Metres { get; set; }

    [JsonPropertyName("Miles")]
    public double Miles { get; set; }
}
