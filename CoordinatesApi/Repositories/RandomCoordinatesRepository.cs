using CoordinatesApi.Interfaces.Repositories;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Repositories;

public class RandomCoordinatesRepository : ICoordinatesRepository
{
    public IEnumerable<Coordinate> GetCoordinates(int count)
    {
        Coordinate[] coordinates = new Coordinate[count];

        Random random = new();

        for (var i = 0; i < count; i++)
        {
            Coordinate current = new()
            {
                Latitude = GetRandomDouble(90),
                Longitude = GetRandomDouble(180)
            };

            coordinates[i] = current;
        }

        return coordinates;


        double GetRandomDouble(int maxValue)
        {
            return Math.Round(random.NextDouble() * maxValue * 2 - maxValue, 6);
        }
    }
}
