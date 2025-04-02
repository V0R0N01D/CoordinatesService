using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Interfaces.Services;

public interface ICoordinatesService
{
    IEnumerable<Coordinate> GetCoordinates(int count);
}
