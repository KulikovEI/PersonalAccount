using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data.Entities;

namespace PersonalAccount.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
    public DbSet<StudentProfileEntity> StudentProfiles => Set<StudentProfileEntity>();
    public DbSet<ConfirmationTokenEntity> ConfirmationTokens => Set<ConfirmationTokenEntity>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AccountEntity>(entity =>
        {
            entity.ToTable("accounts");
            entity.HasKey(account => account.Id);
            entity.HasIndex(account => account.Email)
                .IsUnique();
            
            entity.Property(account => account.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            entity.Property(account => account.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();
   
            entity.Property(account => account.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            entity.Property(account => account.Role)
                .HasColumnName("role")
                .IsRequired();
        });

        builder.Entity<StudentProfileEntity>(entity =>
        {
            entity.ToTable("student_profiles");

            entity.HasKey(student => student.Id);
            entity.Property(student => student.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            entity.Property(student => student.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            entity.Property(student => student.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(student => student.GroupName)
                .HasColumnName("group_name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(student => student.PhotoUrl)
                .HasColumnName("photo_url");
            
            entity.HasOne(student => student.Account)
                .WithOne(student => student.StudentProfile)
                .HasForeignKey<StudentProfileEntity>(student => student.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ConfirmationTokenEntity>(entity =>
        {
            entity.ToTable("confirmation_tokens");
            entity.HasKey(token => token.Id);

            entity.Property(token => token.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(token => token.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            entity.Property(token => token.TokenHash)
                .HasColumnName("token_hash")
                .IsRequired();

            entity.Property(token => token.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();

            entity.Property(token => token.ConfirmedAt)
                .HasColumnName("confirmed_at");

            entity.HasOne(token => token.Account)
                .WithMany(student => student.ConfirmationTokens)
                .HasForeignKey(token => token.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}