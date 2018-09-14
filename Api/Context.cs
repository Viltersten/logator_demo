using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Api
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Audit> Audits { get; set; }
    public DbSet<Network> Networks { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
      base.OnModelCreating(model);

      model.HasDefaultSchema("logator");

      OnModelCreating(model.Entity<Network>());
    }

    private static void OnModelCreating(EntityTypeBuilder<Network> entity)
    {
      //entity.HasKey(_ => _.Id);
      //entity.HasIndex(_ => _.Email).IsUnique();
      //entity.Property(_ => _.Email).IsRequired();
      //entity.Property(_ => _.Phone).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.FirstName).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.LastName).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.Street).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.Zip).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.City).HasDefaultValue("").IsRequired();
      //entity.Property(_ => _.TimeFrame).HasDefaultValue(DateTime.Today).IsRequired();
      //entity.Property(_ => _.Birthdate).IsRequired(false);
      //entity.Ignore(_ => _.FullName);
      //entity.Ignore(_ => _.Experiences);
      //entity.Ignore(_ => _.Ambitions);
      //entity.Ignore(_ => _.Age);
    }

    //private static void OnModelCreating(EntityTypeBuilder<Employer> entity)
    //{
    //  entity.HasKey(_ => _.Id);
    //  entity.HasIndex(_ => _.Cin).IsUnique();
    //  entity.Property(_ => _.Cin).IsRequired();
    //  entity.Property(_ => _.Name).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Contact).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Email).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Phone).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Street).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Zip).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.City).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Website).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Headline).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Briefing).HasDefaultValue("").IsRequired();
    //  entity.Property(_ => _.Description).HasDefaultValue("").IsRequired();
    //}

    //private static void OnModelCreating(EntityTypeBuilder<Tag> entity)
    //{
    //  entity.HasKey(_ => _.Id);
    //  entity.HasIndex(_ => _.Name).IsUnique();
    //  entity.Property(_ => _.Name).IsRequired();
    //  entity.Property(_ => _.Type).HasDefaultValue(Tag.Aspect.General).IsRequired();
    //  entity.Property(_ => _.Info).HasDefaultValue("").IsRequired();
    //}

    //private static void OnModelCreating(EntityTypeBuilder<Depiction> entity)
    //{
    //  entity.HasKey(_ => _.Id);
    //  entity.HasOne(_ => _.Tag);
    //  //entity.HasIndex(_ => new { _.Target, _.Tag, _.Talent}).IsUnique();
    //  entity.Property(_ => _.Target).IsRequired();
    //  entity.Property(_ => _.Extent).HasDefaultValue(1).IsRequired();
    //}

    //private static void OnModelCreating(EntityTypeBuilder<Opportunity> entity)
    //{
    //  entity.HasKey(_ => _.Id);
    //  entity.HasOne(_ => _.Employer);
    //  entity.Property(_ => _.Description).HasDefaultValue("").IsRequired();
    //  entity.Ignore(_ => _.Heritages);
    //}
  }
}
