using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  public class AdminController : Controller
  {
    public AdminController(Context context, IEmailService email, IUtilityService utility)
    {
      Context = context;
      Email = email;
      Utility = utility;
    }

    private readonly Context Context;
    private readonly IEmailService Email;
    private readonly IUtilityService Utility;

    [HttpPost("RequestNetwork")]
    public IActionResult RequestNetwork([FromHeader] string name, [FromHeader] string email)
    {
      Network network = new Network { Name = name };
      Context.Networks.Add(network);
      Context.SaveChanges();

      Email.EmailNetworkClaim(network, email);

      return Ok();
    }

    [HttpPost("ClaimNetwork")]
    public IActionResult ClaimNetwork([FromHeader]string networkId, [FromHeader]string email)
    {
      if (Context.Members.Any(_ => _.Email == email || _.FirstName == email))
        return BadRequest("Email for the creator already exists. Please use an unique email for a new organization.");
      Network network = Context.Networks
        .Include(_ => _.Members)
        .SingleOrDefault(_ => _.Id == new Guid(networkId));
      if (network == null)
        return BadRequest("Organization not recognized. Please use a valid link to claim it.");

      Member owner = new Member
      {
        Email = email,
        FirstName = email,
        Privilege = Member.Privileges.Creator
      };
      network.Members.Add(owner);
      network.AccessedOn = DateTime.UtcNow;

      Context.SaveChanges();

      Email.EmailPasswordReset(email);

      return Ok();
    }

    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword([FromHeader] string email, [FromHeader] string password)
    {
      Member member = Context.Members.Single(_ => _.Email == email);
      member.Password = Utility.Hash(email + password);
      member.AccessedOn = DateTime.UtcNow;

      Context.SaveChanges();

      return Ok();
    }

    [HttpPost("LogIn")]
    public IActionResult LogIn([FromHeader] string userName, [FromHeader] string password)
    {
      Member member = Context.Members
        .Include(_ => _.Network)
        .SingleOrDefault(_ => _.Email == userName);

      if (member == null)
        return NotFound("User " + userName + " not recognized.");

      string hash = Utility.Hash(userName + password);
      if (member.Password != hash)
        return Unauthorized();

      Claim[] claims =
      {
        new Claim(ClaimTypes.Expiration, Utility.TokenExpiration.ToString(), ClaimValueTypes.DateTime),
        new Claim(ClaimTypes.Name, userName, ClaimValueTypes.String),
        new Claim(ClaimTypes.Role, member.Privilege + "", ClaimValueTypes.Integer),
        new Claim(ClaimTypes.Version, "0.1", ClaimValueTypes.String),
        new Claim(ClaimTypes.Webpage, "logator.azurewebsites.net", ClaimValueTypes.String),
        new Claim("NetworkId", member.Network.Id.ToString(), ClaimValueTypes.String),
        new Claim("MemberId", member.Id.ToString(), ClaimValueTypes.String),
        new Claim("Author", "Kvix", ClaimValueTypes.String)
      };
      SymmetricSecurityKey key = new SymmetricSecurityKey(Utility.SecurityKey);
      SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      JwtSecurityToken token = new JwtSecurityToken(
        "logator.azurewebsites.net",
        "logator.azurewebsites.net",
        claims,
        DateTime.UtcNow,
        Utility.TokenExpiration,
        credentials);
      string output = new JwtSecurityTokenHandler().WriteToken(token);

      //member.Token = output;
      member.AccessedOn = DateTime.UtcNow;
      Context.SaveChanges();

      return Ok(new { token = output });
    }

    //[HttpPost("LogCheck")]
    //public IActionResult LogCheck([FromHeader] string token)
    //{
    //  JwtSecurityToken tokenSent = new JwtSecurityToken(token);
    //  string userName = tokenSent.Claims
    //    .FirstOrDefault(_ => _.Type == ClaimTypes.Name)
    //    ?.Value;
    //  Member member = Context.Members.SingleOrDefault(_ => _.Email == userName);

    //  if (member == null)
    //    return NotFound();

    //  string role = member.Privilege.ToString();
    //  TimeSpan remainingTime = tokenSent.ValidTo - DateTime.UtcNow;
    //  string verificator = member.Token;

    //  if (remainingTime.Ticks < 0)
    //    return Unauthorized();

    //  if (verificator != token)
    //    return Unauthorized();

    //  if (member.Privilege == Member.Privileges.Suspended)
    //    return Forbid();

    //  return Ok(new
    //  {
    //    authorized = true,
    //    role,
    //    remainingTime,
    //    token
    //  });
    //}

    [HttpPost("LogOut/{id}")]
    public IActionResult LogOut([FromHeader] Guid id)
    {
      Member member = Context.Members.SingleOrDefault(_ => _.Id == id);

      if (member != null)
        //member.Token = null;
        member.AccessedOn = DateTime.Now;

      Context.SaveChanges();

      return Ok();
    }












    [HttpGet("Audits")]
    public IActionResult GetAudits()
    {
      return Ok(Context.Audits);
    }

    [HttpPost("StoreAudit")]
    public IActionResult StoreAudit(Audit audit)
    {
      Context.Audits.Add(audit);
      Context.SaveChanges();

      return Ok();
    }









    //[HttpGet("User/{id}")]
    //public object User(Guid userId)
    //{
    //  return new object();
    //}

    [HttpGet("Users")]
    public List<object> Users(Guid organizationId)
    {
      return new List<object>();
    }

    [HttpPost("AdmitUser")]
    public string AdmitUser(string userName, string password, Guid organizationId)
    {
      return $"CreateUser-{userName}-{password}-{organizationId}";
    }

    [HttpPost("GetToken")]
    public string GetToken(string userName, string password)
    {
      return $"GetToken-{userName}-{password}";
    }
  }
}