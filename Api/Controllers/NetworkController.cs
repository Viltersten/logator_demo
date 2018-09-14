using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Api.Controllers
{
  [Authorize(Roles = "Super")]
  //[Authorize]
  [Route("api/[controller]")]
  public class NetworkController : Controller
  {
    public NetworkController(Context context)
    {
      Context = context;
    }

    private readonly Context Context;

    [HttpGet("Networks")]
    public IActionResult GetNetworks()
    {
      // todo Investigate why the included subfields are not working.
      return Ok(Context.Networks.Include(_ => _.Members));
      //return Ok(Context.Networks);
    }

    [HttpGet("Network/{id}")]
    public IActionResult GetNetwork(Guid id)
    {
      Network output = Context.Networks.SingleOrDefault(_ => _.Id == id);

      if (output == null)
        return NotFound(id);

      return Ok(output);
    }

    [HttpPost("Network")]
    public IActionResult SetNetwork([FromBody] Network network)
    {
      if (Context.Networks.Any(_ => _.Id == network.Id))
        return BadRequest("Id already in use");

      Context.Networks.Add(network);
      Context.SaveChanges();

      return Created("uri", network);
    }
  }
}