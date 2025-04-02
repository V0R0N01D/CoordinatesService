using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Interfaces.Repositories;

public interface ICoordinatesMathRepository
{
    public DistanceResult CalculateDistance(List<Coordinate> coordinates);
}
