using System.IO;
using System.Text;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IUtilityService, UtilityService>();
      services.AddSingleton<IEmailService, EmailService>();
      services.AddCors(_ => _.AddPolicy("Open policy", __ => __
        .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(_ =>
        {
          _.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateActor = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Security:key").Value))
          };
        });
      services.AddAuthorization(_ => _.AddPolicy("Common", __ => __.RequireRole("Creator", "Admin", "Super", "Common")));
      services.AddAuthorization(_ => _.AddPolicy("Super", __ => __.RequireRole("Creator", "Admin", "Super")));
      services.AddAuthorization(_ => _.AddPolicy("Admin", __ => __.RequireRole("Creator", "Admin")));
      services.AddAuthorization(_ => _.AddPolicy("Creator", __ => __.RequireRole("Creator")));
      
      services.AddMvc()
        .AddJsonOptions(_ => _.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
      services.AddSwaggerGen(_ => _.SwaggerDoc("v0.1", new Info { Title = "LoGator" }));
      services.AddDbContext<Context>(_ => _.UseSqlServer(Configuration.GetConnectionString("azure")));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseCors("Open policy");
      app.UseAuthentication();
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(_ => _.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Version 0.1"));
      app.Run(async _ =>
      {
        _.Response.ContentType = "text/html";
        await _.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
      });
    }
  }
}
