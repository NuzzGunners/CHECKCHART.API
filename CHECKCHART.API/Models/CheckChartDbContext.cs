using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CHECKCHART.API.Models
{
    public partial class CheckChartDbContext : DbContext
    {
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<AuditCategory> AuditCategory { get; set; }
        public virtual DbSet<Checkchart> Checkchart { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Reason> Reason { get; set; }
        public virtual DbSet<ReportLog> ReportLog { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Userposition> Userposition { get; set; }
        public virtual DbSet<V_CheckchartLog> V_CheckchartLog { get; set; }

        public CheckChartDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.An)
                    .IsRequired()
                    .HasColumnName("an")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Coder)
                    .HasColumnName("coder")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Cxlbydatetime)
                    .HasColumnName("cxlbydatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cxlbyuser)
                    .HasColumnName("cxlbyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Doctor)
                    .HasColumnName("doctor")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Doctorconsult)
                    .HasColumnName("doctorconsult")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Doctormaster)
                    .HasColumnName("doctormaster")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Entrybydatetime)
                    .HasColumnName("entrybydatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Entrybyuser)
                    .IsRequired()
                    .HasColumnName("entrybyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Los)
                    .HasColumnName("los")
                    .HasColumnType("decimal");

                entity.Property(e => e.Nurse)
                    .HasColumnName("nurse")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Rwafter)
                    .HasColumnName("rwafter")
                    .HasColumnType("decimal");

                entity.Property(e => e.Rwbefore)
                    .HasColumnName("rwbefore")
                    .HasColumnType("decimal");

                entity.Property(e => e.Updatebydatetime)
                    .HasColumnName("updatebydatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updatebyuser)
                    .HasColumnName("updatebyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Ward)
                    .HasColumnName("ward")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_audit_category");

                entity.HasOne(d => d.CxlbyuserNavigation)
                    .WithMany(p => p.AuditCxlbyuserNavigation)
                    .HasForeignKey(d => d.Cxlbyuser)
                    .HasConstraintName("FK_audit_cxlbyuser_user");

                entity.HasOne(d => d.EntrybyuserNavigation)
                    .WithMany(p => p.AuditEntrybyuserNavigation)
                    .HasForeignKey(d => d.Entrybyuser)
                    .HasConstraintName("FK_audit_entrybyuser_user");

                entity.HasOne(d => d.UpdatebyuserNavigation)
                    .WithMany(p => p.AuditUpdatebyuserNavigation)
                    .HasForeignKey(d => d.Updatebyuser)
                    .HasConstraintName("FK_audit_updatebyuser_user");
            });

            modelBuilder.Entity<AuditCategory>(entity =>
            {
                entity.ToTable("audit_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Checkchart>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.An })
                    .HasName("pk_checkchart");

                entity.ToTable("checkchart");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.An)
                    .HasColumnName("an")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Cxlbyuser)
                    .HasColumnName("cxlbyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Cxlbyuserdatetime)
                    .HasColumnName("cxlbyuserdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cxlbyuserreason).HasColumnName("cxlbyuserreason");

                entity.Property(e => e.Receivebydatetime)
                    .HasColumnName("receivebydatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Receivebyposition).HasColumnName("receivebyposition");

                entity.Property(e => e.Receivebyuser)
                    .IsRequired()
                    .HasColumnName("receivebyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Sendtobyuser)
                    .HasColumnName("sendtobyuser")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Sendtodatetime)
                    .HasColumnName("sendtodatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sendtoposition).HasColumnName("sendtoposition");

                entity.Property(e => e.Sendtouser)
                    .HasColumnName("sendtouser")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.CxlbyuserNavigation)
                    .WithMany(p => p.CheckchartCxlbyuserNavigation)
                    .HasForeignKey(d => d.Cxlbyuser)
                    .HasConstraintName("FK_cxlbyuser_user");

                entity.HasOne(d => d.CxlbyuserreasonNavigation)
                    .WithMany(p => p.Checkchart)
                    .HasForeignKey(d => d.Cxlbyuserreason)
                    .HasConstraintName("FK_cxlbyuserreason_reason");

                entity.HasOne(d => d.ReceivebypositionNavigation)
                    .WithMany(p => p.CheckchartReceivebypositionNavigation)
                    .HasForeignKey(d => d.Receivebyposition)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_receivebyposition_position");

                entity.HasOne(d => d.ReceivebyuserNavigation)
                    .WithMany(p => p.CheckchartReceivebyuserNavigation)
                    .HasForeignKey(d => d.Receivebyuser)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_receivebyuser_user");

                entity.HasOne(d => d.SendtobyuserNavigation)
                    .WithMany(p => p.CheckchartSendtobyuserNavigation)
                    .HasForeignKey(d => d.Sendtobyuser)
                    .HasConstraintName("FK_sendtobyuser_user");

                entity.HasOne(d => d.SendtopositionNavigation)
                    .WithMany(p => p.CheckchartSendtopositionNavigation)
                    .HasForeignKey(d => d.Sendtoposition)
                    .HasConstraintName("FK_sendtoposition_position");

                entity.HasOne(d => d.SendtouserNavigation)
                    .WithMany(p => p.CheckchartSendtouserNavigation)
                    .HasForeignKey(d => d.Sendtouser)
                    .HasConstraintName("FK_sendtouser_user");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Englishname)
                    .HasColumnName("englishname")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Localname)
                    .HasColumnName("localname")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PositionOrder).HasColumnName("position_order");
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.ToTable("reason");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ReportLog>(entity =>
            {
                entity.ToTable("report_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Clientip)
                    .HasColumnName("clientip")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Msg)
                    .HasColumnName("msg")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Reportdatetime)
                    .HasColumnName("reportdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Reportname)
                    .HasColumnName("reportname")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("pk_user");

                entity.ToTable("user");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Roleuser).HasColumnName("roleuser");

                entity.HasOne(d => d.RoleuserNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Roleuser)
                    .HasConstraintName("fk_user_role");
            });

            modelBuilder.Entity<Userposition>(entity =>
            {
                entity.ToTable("userposition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Userposition)
                    .HasForeignKey(d => d.Position)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_userposition_position");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Userposition)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_userposition_user");
            });
        }
    }
}