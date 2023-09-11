using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.Data;
public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Household> Households { get; set; } = null!;
    public DbSet<HouseholdUser> HouseholdUsers { get; set; } = null!;
    public DbSet<HouseholdInvitation> HouseholdInvitations { get; set; } = null!;
    public DbSet<Budget> Budgets { get; set; } = null!;
    public DbSet<BudgetEvent> BudgetEvents { get; set; } = null!;
    public DbSet<BudgetCategory> BudgetCategories { get; set; } = null!;
    public DbSet<BudgetSubCategory> BudgetSubCategories { get; set; } = null!;
    public DbSet<RecurringEvent> RecurringEvents { get; set; } = null!;
    public DbSet<Entity> Entities { get; set; } = null!;

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<AccountUser> AccountUsers { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureIdentityTables(builder);
        ConfigureHouseholdTables(builder);
        ConfigureBudgetTables(builder);


        builder.Entity<Entity>()
            .OwnsOne(e => e.ContactInfo)
            .OwnsOne(c => c.Address);
    }

    private static void ConfigureIdentityTables(ModelBuilder builder)
    {
        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable(name: "Users");
        });
        builder.Entity<AppRole>(entity =>
        {
            entity.ToTable(name: "Roles");
        });
        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
    public void ConfigureHouseholdTables(ModelBuilder builder)
    {
        builder.Entity<Household>()
           .HasMany(h => h.Accounts)
           .WithOne(a => a.Household);

        builder.Entity<HouseholdUser>()
            .HasKey(hu => new { hu.HouseholdId, hu.AppUserId });
        builder.Entity<HouseholdUser>()
            .HasOne(hu => hu.Household)
            .WithMany(h => h.HouseholdUsers)
            .HasForeignKey(hu => hu.HouseholdId);
        builder.Entity<HouseholdUser>()
            .HasOne(hu => hu.AppUser)
            .WithMany(h => h.HouseholdUsers)
            .HasForeignKey(hu => hu.AppUserId);

    }
    public static void ConfigureBudgetTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Budget>()
            .HasMany(b => b.RecurringEvents)
            .WithOne(bi => bi.Budget)
            .HasForeignKey(bi => bi.BudgetId);
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.Household)
            .WithMany(h => h.Budgets)
            .HasForeignKey(b => b.HouseholdId);

        modelBuilder.Entity<RecurringEvent>()
                    .HasKey(bi => bi.Id);
        modelBuilder.Entity<RecurringEvent>()
            .HasOne(bi => bi.Budget)
            .WithMany(b => b.RecurringEvents)
            .HasForeignKey(bi => bi.BudgetId);
        modelBuilder.Entity<RecurringEvent>()
            .HasOne(bi => bi.BudgetCategory)
            .WithMany()
            .HasForeignKey(bi => bi.BudgetCategoryId);

        modelBuilder.Entity<BudgetEvent>()
            .HasKey(be => be.Id);

     

        modelBuilder.Entity<EventLog>()
            .HasKey(el => el.Id);
        modelBuilder.Entity<EventLog>()
            .HasOne(el => el.EventStatus)
            .WithMany();

        modelBuilder.Entity<BudgetCategory>()
            .HasKey(bc => bc.Id);
        modelBuilder.Entity<RecurringEvent>()
            .HasOne(bc => bc.BudgetCategory)
            .WithMany()
            .HasForeignKey(bc => bc.BudgetCategoryId);
        modelBuilder.Entity<RecurringEvent>()
            .HasMany(re => re.BudgetEvents)
            .WithOne(e => e.RecurringEvent);
        modelBuilder.Entity<Account>()
           .HasMany(a => a.Transactions)
           .WithOne(t => t.Account)
           .HasForeignKey(t => t.AccountId);
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Users)
            .WithMany(u => u.Accounts)
            .UsingEntity<AccountUser>(
               j => j.HasOne(au => au.AppUser)
                    .WithMany(u => u.UserAccounts)
                    .HasForeignKey(au => au.AppUserId),
               j => j.HasOne(au => au.Account)
                     .WithMany(a => a.AccountUsers)
                     .HasForeignKey(au => au.AccountId),
               j =>
               {
                   j.HasKey(t => new { t.AccountId, t.AppUserId });
                   j.ToTable("AccountUsers");
               });
    }



}

