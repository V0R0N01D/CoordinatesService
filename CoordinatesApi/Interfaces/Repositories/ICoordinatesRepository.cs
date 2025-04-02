using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Interfaces.Repositories;

public interface ICoordinatesRepository
{
    IEnumerable<Coordinate> GetCoordinates(int count);
}
