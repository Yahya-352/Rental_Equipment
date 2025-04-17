using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace myproject_Library.Model
{
    public partial class EquipmentDBContext : DbContext
    {
        public EquipmentDBContext()
        {
        }

        public EquipmentDBContext(DbContextOptions<EquipmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentAvailability> EquipmentAvailabilities { get; set; } = null!;
        public virtual DbSet<EquipmentCondition> EquipmentConditions { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<RentalRequest> RentalRequests { get; set; } = null!;
        public virtual DbSet<RentalTransaction> RentalTransactions { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<ReturnRecord> ReturnRecords { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=STS_LAPTOP_02\\MSSQLSERVER02;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Document_User");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasOne(d => d.AvailabilityStatus)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.AvailabilityStatusId)
                    .HasConstraintName("FK_Equipment_Equipment_Availability");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Equipment_Category");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.ConditionId)
                    .HasConstraintName("FK_Equipment_Condition_ID");
            });

            modelBuilder.Entity<EquipmentCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId)
                    .HasName("PK_Condition_ID");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Feedback_Rental_Transaction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Feedback_User");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Logs_User");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Notifications_User");
            });

            modelBuilder.Entity<RentalRequest>(entity =>
            {
                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.RentalRequests)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_Rental_Request_Equipment");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.RentalRequests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .HasConstraintName("FK_Rental_Request_Request_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RentalRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Rental_Request_User");
            });

            modelBuilder.Entity<RentalTransaction>(entity =>
            {
                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.RentalTransactions)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_Rental_Transaction_Equipment");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.RentalTransactions)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_Rental_Transaction_Payment_Status");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RentalTransactions)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_Rental_Transaction_Rental_Request");
            });

            modelBuilder.Entity<ReturnRecord>(entity =>
            {
                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.ReturnRecords)
                    .HasForeignKey(d => d.ConditionId)
                    .HasConstraintName("FK_Return_Record_Condition_ID");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.ReturnRecords)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Return_Record_Rental_Transaction");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Generate audit logs before saving changes
            GenerateAuditLogs();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Generate audit logs before saving changes asynchronously
            GenerateAuditLogs();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void GenerateAuditLogs()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in entries)
            {
                // Skip the Log entity itself to avoid infinite loops
                if (entry.Entity is Log)
                    continue;

                var entityName = entry.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(entry);
                var action = GetActionName(entry.State);

                // Create the log record
                var log = new Log
                {
                    Action = action,
                    Timestamp = DateTime.Now,
                    Source = entityName,
                    AffectedData = GenerateAffectedDataJson(entry, primaryKey),
                    Exception = null // Will be filled if there's an exception
                };

                // Add the log to the context
                Logs.Add(log);
            }
        }

        private string GetActionName(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return "Insert";
                case EntityState.Modified:
                    return "Update";
                case EntityState.Deleted:
                    return "Delete";
                default:
                    return "Unknown";
            }
        }

        private string GetPrimaryKeyValue(EntityEntry entry)
        {
            var keyName = entry.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).Single();
            return entry.Property(keyName).CurrentValue?.ToString();
        }

        private string GenerateAffectedDataJson(EntityEntry entry, string primaryKey)
        {
            var data = new Dictionary<string, object>();

            data["PrimaryKey"] = primaryKey;
            data["EntityName"] = entry.Entity.GetType().Name;

            if (entry.State == EntityState.Added)
            {
                // For new entities, log all the property values
                foreach (var property in entry.Properties)
                {
                    data[property.Metadata.Name] = property.CurrentValue?.ToString();
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                // For modified entities, log the changed properties
                var changes = new Dictionary<string, object>();
                foreach (var property in entry.Properties.Where(p => p.IsModified))
                {
                    changes[property.Metadata.Name] = new
                    {
                        OldValue = property.OriginalValue?.ToString(),
                        NewValue = property.CurrentValue?.ToString()
                    };
                }
                data["Changes"] = changes;
            }

            return System.Text.Json.JsonSerializer.Serialize(data);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
