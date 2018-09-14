using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Api.Controllers
{
  [Route("api/[controller]"), Authorize(Policy = "Common")]
  public class MemberController : Controller
  {
    public MemberController(Context context)
    {
      Context = context;
    }

    private readonly Context Context;
    private Guid UserId;
    private Guid NetworkId;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      UserId = new Guid(User.Claims.First(_ => _.Type == "MemberId").Value);
      NetworkId = new Guid(User.Claims.First(_ => _.Type == "NetworkId").Value);
    }

    [HttpGet("all"), Authorize(Policy = "Common")]
    public IActionResult GetMembers()
    {
      // todo Perform redirect to login page when unauthorized or unauthenticated access detected.
      return Ok(Context.Members
        .Where(_ => _.Network.Id == NetworkId));
    }

    [HttpGet("{id}"), Authorize(Policy = "Common")]
    public IActionResult GetMember(Guid id)
    {
      Member output = Context.Members
        .Include(_ => _.Network)
        .Where(_ => _.Id == UserId)
        .SingleOrDefault(_ => _.Id == id);

      if (output == null)
        return NotFound(id);

      return Ok(output);
    }

    [HttpPost("self/{id}"), Authorize(Policy = "Common")]
    public IActionResult SetMemberSelf(Guid id, [FromBody]Member member)
    {
      if (id != UserId)
        return Unauthorized();

      Member output = Context.Members
        .Where(_ => _.Id == UserId)
        .SingleOrDefault(_ => _.Id == id);

      if (output == null)
        return NotFound(id);

      if (!string.IsNullOrWhiteSpace(member.FirstName))
        output.FirstName = member.FirstName;
      if (!string.IsNullOrWhiteSpace(member.LastName))
        output.LastName = member.LastName;
      if (!string.IsNullOrWhiteSpace(member.Email))
        output.Email = member.Email;
      if (!string.IsNullOrWhiteSpace(member.Phone))
        output.Phone = member.Phone;
      if (!string.IsNullOrWhiteSpace(member.Address))
      {
        output.Address = member.Address;
        output.Lat = member.Lat;
        output.Lng = member.Lng;
      }

      output.AccessedOn = DateTime.UtcNow;
      Context.SaveChanges();

      return Ok();
    }

    [HttpPost("other/{id}"), Authorize(Policy = "Super")]
    public IActionResult SetMemberOther(Guid id, [FromBody]Member member)
    {
      Member output = Context.Members
        .Where(_ => _.Id == member.Id && _.Network.Id == NetworkId)
        .SingleOrDefault(_ => _.Id == id);

      if (output == null)
        return NotFound(id);

      if (!string.IsNullOrWhiteSpace(member.FirstName))
        output.FirstName = member.FirstName;
      if (!string.IsNullOrWhiteSpace(member.LastName))
        output.LastName = member.LastName;
      if (!string.IsNullOrWhiteSpace(member.Phone))
        output.Phone = member.Phone;
      if (!string.IsNullOrWhiteSpace(member.Email))
        output.Email = member.Email;
      if (!string.IsNullOrWhiteSpace(member.Address))
      {
        output.Address = member.Address;
        output.Lat = member.Lat;
        output.Lng = member.Lng;
      }

      Context.SaveChanges();

      return Ok();
    }

    [HttpPost("create"), Authorize(Policy = "Super")]
    public IActionResult SetMember([FromBody] Member member)
    {
      if (Context.Members.Any(_ => _.Id == member.Id))
        return BadRequest("ID already in use.");

      member.Network.Id = NetworkId;

      Context.Members.Add(member);
      Context.SaveChanges();

      return Created("uri", member);
    }

    [HttpDelete("remove"), Authorize(Policy = "Admin")]
    public IActionResult RemoveMember([FromBody] Guid id)
    {
      Member member = Context.Members.FirstOrDefault(_ => _.Id == id);
      if (member == null)
        return BadRequest("ID not recognized.");

      member.Privilege = Member.Privileges.Suspended;
      member.DeletedOn = DateTime.UtcNow;

      Context.Members.Add(member);
      Context.SaveChanges();

      return Created("uri", member);
    }
  }
}