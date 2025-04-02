using Microsoft.AspNetCore.Mvc;
using CoordinatesApi.Interfaces.Services;
using CoordinatesApi.Models.Dtos;

namespace CoordinatesApi.Controllers;

[Route("api/[controller]")]
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
    public ActionResult Post([FromBody] IEnumerable<Coordinate> coordinates)
    {
        return StatusCode(501);
    }
}