using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SSAuth.Models;

namespace SSAuth.Data.DBContext
{
    public partial class SSAuthContext : DbContext
    {
        public SSAuthContext()
        {
        }

        public SSAuthContext(DbContextOptions<SSAuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthGroup> AuthGroup { get; set; }
        public virtual DbSet<AuthMethod> AuthMethod { get; set; }
        public virtual DbSet<AuthUser> AuthUser { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestion { get; set; }
        public virtual DbSet<SeqControl> SeqControl { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserSecurityQuestion> UserSecurityQuestion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User=samuel;Password=abc123$$;database=auth;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthGroup>(entity =>
            {
                entity.ToTable("auth_group");

                entity.HasIndex(e => e.AuthGroupName)
                    .HasName("auth_group_name")
                    .IsUnique();

                entity.HasIndex(e => e.AuthMethodId)
                    .HasName("auth_method_id");

                entity.Property(e => e.AuthGroupId)
                    .HasColumnName("auth_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthGroupName)
                    .IsRequired()
                    .HasColumnName("auth_group_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.AuthMethodId)
                    .HasColumnName("auth_method_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.AuthMethod)
                    .WithMany(p => p.AuthGroup)
                    .HasForeignKey(d => d.AuthMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_ibfk_1");
            });

            modelBuilder.Entity<AuthMethod>(entity =>
            {
                entity.ToTable("auth_method");

                entity.HasIndex(e => e.AuthMethodName)
                    .HasName("auth_method_name")
                    .IsUnique();

                entity.Property(e => e.AuthMethodId)
                    .HasColumnName("auth_method_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthMethodName)
                    .IsRequired()
                    .HasColumnName("auth_method_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("auth_user");

                entity.HasIndex(e => new { e.AuthGroupId, e.Email })
                    .HasName("auth_group_id")
                    .IsUnique();

                entity.Property(e => e.AuthUserId)
                    .HasColumnName("auth_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthGroupId)
                    .HasColumnName("auth_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.AuthGroup)
                    .WithMany(p => p.AuthUser)
                    .HasForeignKey(d => d.AuthGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_ibfk_1");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("login_history");

                entity.HasIndex(e => e.AuthUserId)
                    .HasName("auth_user_id");

                entity.Property(e => e.LoginHistoryId)
                    .HasColumnName("login_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthUserId)
                    .HasColumnName("auth_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoginDate)
                    .HasColumnName("login_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.LoginHistory)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("login_history_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SecurityQuestion>(entity =>
            {
                entity.ToTable("security_question");

                entity.Property(e => e.SecurityQuestionId)
                    .HasColumnName("security_question_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SeqControl>(entity =>
            {
                entity.HasKey(e => e.ObjName)
                    .HasName("PRIMARY");

                entity.ToTable("seq_control");

                entity.Property(e => e.ObjName)
                    .HasColumnName("obj_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NextId)
                    .HasColumnName("next_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.HasIndex(e => e.AuthUserId)
                    .HasName("auth_user_id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_id");

                entity.Property(e => e.UserRoleId)
                    .HasColumnName("user_role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthUserId)
                    .HasColumnName("auth_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_role_ibfk_2");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_role_ibfk_1");
            });

            modelBuilder.Entity<UserSecurityQuestion>(entity =>
            {
                entity.ToTable("user_security_question");

                entity.HasIndex(e => e.AuthUserId)
                    .HasName("auth_user_id");

                entity.HasIndex(e => e.SecurityQuestionId)
                    .HasName("security_question_id");

                entity.Property(e => e.UserSecurityQuestionId)
                    .HasColumnName("user_security_question_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AuthUserId)
                    .HasColumnName("auth_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("date_added")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateChanged)
                    .HasColumnName("date_changed")
                    .HasColumnType("datetime");

                entity.Property(e => e.SecurityQuestionId)
                    .HasColumnName("security_question_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserAdded)
                    .IsRequired()
                    .HasColumnName("user_added")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserChanged)
                    .HasColumnName("user_changed")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.UserSecurityQuestion)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_security_question_ibfk_2");

                entity.HasOne(d => d.SecurityQuestion)
                    .WithMany(p => p.UserSecurityQuestion)
                    .HasForeignKey(d => d.SecurityQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_security_question_ibfk_1");
            });
        }
    }
}
