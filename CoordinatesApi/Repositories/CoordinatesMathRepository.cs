using CoordinatesApi.Interfaces.Repositories;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Repositories;

public class CoordinatesMathRepository : ICoordinatesMathRepository
{
    private const double EarthRadiusMeters = 6371000;
    private const double MetersToMilesConversion = 0.000621371;

    public DistanceResult CalculateDistance(List<Coordinate> coordinates)
    {
        double totalDistanceMeters = 0;

        for (int i = 0; i < coordinates.Count - 1; i++)
        {
            var currentCoord = coordinates[i];
            var nextCoord = coordinates[i + 1];


            double latCurRad = DegreesToRadians(currentCoord.Latitude);
            double lonCurRad = DegreesToRadians(currentCoord.Longitude);
            double latNextRad = DegreesToRadians(nextCoord.Latitude);
            double lonNextRad = DegreesToRadians(nextCoord.Longitude);

            double deltaLat = latNextRad - latCurRad;
            double deltaLon = lonNextRad - lonCurRad;

            double haversineSquared = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(latCurRad) * Math.Cos(latNextRad) *
                Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double angularDistance = 2 * Math.Atan2(
                Math.Sqrt(haversineSquared),
                Math.Sqrt(1 - haversineSquared));

            double segmentDistance = EarthRadiusMeters * angularDistance;
            totalDistanceMeters += segmentDistance;
        }

        double totalDistanceMiles = totalDistanceMeters * MetersToMilesConversion;

        return new DistanceResult()
        {
            Metres = Math.Round(totalDistanceMeters, 3),
            Miles = Math.Round(totalDistanceMiles, 3)
        };
    }

    private double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
}
