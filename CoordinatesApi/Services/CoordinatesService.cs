using CoordinatesApi.Interfaces.Repositories;
using CoordinatesApi.Interfaces.Services;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Services;

public class CoordinatesService(
    ICoordinatesRepository coordinatesRepository,
    ICoordinatesMathRepository coordinatesMathRepository) : ICoordinatesService
{
    public IEnumerable<Coordinate> GetCoordinates(int count)
    {
        return coordinatesRepository.GetCoordinates(count);
    }

    public DistanceResult CalculateDistance(List<Coordinate>? coordinates)
    {
        if (coordinates == null || coordinates.Count < 2)
            return new DistanceResult();

        return coordinatesMathRepository.CalculateDistance(coordinates);
    }
}
