using Microsoft.AspNetCore.Mvc;
using CoordinatesApi.Interfaces.Services;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Controllers;

[Route("/[controller]")]
[ApiController]
public class CoordinatesController(ICoordinatesService coordinatesService) : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Coordinate>> GetCoordinates([FromQuery] int count)
    {
        if (count < 1) return BadRequest("Count can't be less than 1.");

        IEnumerable<Coordinate> coordinates = coordinatesService.GetCoordinates(count);

        return Ok(coordinates);
    }

    [HttpPost]
    public ActionResult<IEnumerable<DistanceResult>> CalculateDistance(
        [FromBody] List<Coordinate>? coordinates)
    {
        if (coordinates?
            .Any(c => Math.Abs(c.Latitude) > 90 || Math.Abs(c.Longitude) > 180) == true)
            return BadRequest(
                "The coordinates should be in the range: latitude ±90°, longitude ±180°");

        return Ok(coordinatesService.CalculateDistance(coordinates));
    }
}