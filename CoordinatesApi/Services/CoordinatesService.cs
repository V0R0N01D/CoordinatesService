using CoordinatesApi.Interfaces.Repositories;
using CoordinatesApi.Interfaces.Services;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Services;

public class CoordinatesService(ICoordinatesRepository coordinatesRepository) : ICoordinatesService
{
    public IEnumerable<Coordinate> GetCoordinates(int count)
    {
        return coordinatesRepository.GetCoordinates(count);
    }
}
