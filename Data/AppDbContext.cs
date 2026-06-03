using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data.Entities;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
    public DbSet<ConfirmationTokenEntity> ConfirmationTokens => Set<ConfirmationTokenEntity>();


    public DbSet<GroupEntity> Groups => Set<GroupEntity>();
    public DbSet<DisciplineEntity> Disciplines => Set<DisciplineEntity>();
    public DbSet<TeacherGroupDisciplineEntity> TeacherGroupDisciplines => Set<TeacherGroupDisciplineEntity>();

    public DbSet<StudentProfileEntity> StudentProfiles => Set<StudentProfileEntity>();
    public DbSet<TeacherProfileEntity> TeacherProfiles => Set<TeacherProfileEntity>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DisciplineEntity>(entity =>
        {
            entity.BuildEntity("disciplines");

            entity.Property(discipline => discipline.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.Entity<TeacherGroupDisciplineEntity>(entity =>
        {
            entity.BuildEntity("teacher_group_disciplines");

            entity.Property(link => link.GroupId)
                .HasColumnName("group_id")
                .IsRequired();

            entity.Property(link => link.TeacherAccountId)
                .HasColumnName("teacher_account_id")
                .IsRequired();

            entity.Property(link => link.DisciplineId)
                .HasColumnName("discipline_id")
                .IsRequired();

            entity.HasOne(link => link.Group)
                .WithMany(group => group.TeacherGroupDisciplines)
                .HasForeignKey(link => link.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(link => link.TeacherAccount)
                .WithMany(account => account.TeacherGroupDisciplines)
                .HasForeignKey(link => link.TeacherAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(link => link.Discipline)
                .WithMany(discipline => discipline.TeacherGroupDisciplines)
                .HasForeignKey(link => link.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<AccountEntity>(entity =>
        {
            entity.BuildEntity("accounts");
            entity.HasIndex(account => account.Email)
                .IsUnique();

            entity.Property(account => account.Role)
                .HasColumnName("role")
                .HasDefaultValue(AccountRoles.Student)
                .IsRequired();

            entity.Property(account => account.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(account => account.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();
        });

        builder.Entity<GroupEntity>(entity =>
        {
            entity.BuildEntity("groups");

            entity.Property(group => group.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(group => group.Description)
                .HasColumnName("description")
                .HasMaxLength(2047)
                .HasDefaultValue(string.Empty)
                .IsRequired();

            entity.Property(group => group.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(2047);
        });

        builder.Entity<StudentProfileEntity>(entity =>
        {
            entity.BuildProfileEntity("student_profiles", account => account.StudentProfile);

            entity.Property(student => student.GroupId)
                .HasColumnName("group_id");

            entity.HasOne(student => student.Group)
                .WithMany(group => group.StudentProfiles)
                .HasForeignKey(student => student.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TeacherProfileEntity>(entity =>
        {
            entity.BuildProfileEntity("teacher_profiles", account => account.TeacherProfile);
        });

        builder.Entity<ConfirmationTokenEntity>(entity =>
        {
            entity.BuildEntity("confirmation_tokens");

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