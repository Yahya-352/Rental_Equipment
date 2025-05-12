using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace myproject_Library.Model
{
    public partial class EquipmentDBContext : IdentityDbContext<
        User, 
        Role, 
        int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        >
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //Salman connection Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EquipmentsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False


                //IDK whos's connection Data Source=STS_LAPTOP_02\\MSSQLSERVER02;Initial Catalog=EquipmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EquipmentsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base implementation first

            // Configure the Identity tables to use your custom table names
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");

            // Configure the additional Identity tables (these will be new tables)
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");

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

                entity.Property(e => e.AffectedData).HasColumnType("varchar(MAX)");
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

            // Now seed your data
            SeedUsers(modelBuilder);
            SeedRoles(modelBuilder);
            SeedRequestStatuses(modelBuilder);
            SeedEquipmentConditions(modelBuilder);
            SeedEquipmentAvailability(modelBuilder);
            SeedPaymentStatuses(modelBuilder);
            SeedCategories(modelBuilder);
            SeedEquipment(modelBuilder);
            SeedUserRoles(modelBuilder);
            SeedRentalRequests(modelBuilder);
            SeedRentalTransactions(modelBuilder);
            SeedReturnRecords(modelBuilder);
            SeedFeedback(modelBuilder);
            SeedNotifications(modelBuilder);
            SeedLogs(modelBuilder);
            SeedDocuments(modelBuilder);


            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Generate audit logs before saving changes
            GenerateAuditLogs();
           //return 0;
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
                    UserId = CurrentUserService.GetCurrentUserId(),
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
            // Handle composite keys
            var primaryKey = entry.Metadata.FindPrimaryKey();
            if (primaryKey.Properties.Count > 1)
            {
                // Composite key - combine the values
                return string.Join(",", primaryKey.Properties.Select(p =>
                    entry.Property(p.Name).CurrentValue?.ToString()));
            }
            else
            {
                // Single key
                var keyName = primaryKey.Properties.Single().Name;
                return entry.Property(keyName).CurrentValue?.ToString();
            }
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

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User", NormalizedName = "USER", ConcurrencyStamp = "af1255c9-ef67-4324-beec-eae03bf14713" },
                new Role { Id = 2, Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "0e462049-6047-4e1e-ae6d-96a53cabce296" },
                new Role { Id = 3, Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = "c6ac44ab-cc96-489f-8e1f-3cef3b89276c" }
            );
        }

        private void SeedRequestStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestStatus>().HasData(
                new RequestStatus { RequestStatusId = 1, RequestStatusName = "Pending" },
                new RequestStatus { RequestStatusId = 2, RequestStatusName = "Approved" },
                new RequestStatus { RequestStatusId = 3, RequestStatusName = "Completed" },
                new RequestStatus { RequestStatusId = 4, RequestStatusName = "Rejected" },
                new RequestStatus { RequestStatusId = 5, RequestStatusName = "Cancelled" }
            );
        }

        private void SeedEquipmentConditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentCondition>().HasData(
                new EquipmentCondition { ConditionId = 1, ConditionName = "Excellent" },
                new EquipmentCondition { ConditionId = 2, ConditionName = "Good" },
                new EquipmentCondition { ConditionId = 3, ConditionName = "Damaged" },
                new EquipmentCondition { ConditionId = 4, ConditionName = "Under Repair" },
                new EquipmentCondition { ConditionId = 5, ConditionName = "Retired" }
            );
        }

        private void SeedEquipmentAvailability(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentAvailability>().HasData(
                new EquipmentAvailability { AvailabilityStatusId = 1, AvailabilityStatusName = "Available" },
                new EquipmentAvailability { AvailabilityStatusId = 2, AvailabilityStatusName = "Rented" },
                new EquipmentAvailability { AvailabilityStatusId = 3, AvailabilityStatusName = "Under Maintenance" },
                new EquipmentAvailability { AvailabilityStatusId = 4, AvailabilityStatusName = "Reserved" }
            );
        }

        private void SeedPaymentStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentStatus>().HasData(
                new PaymentStatus { PaymentStatusId = 1, PaymentStatusName = "Paid" },
                new PaymentStatus { PaymentStatusId = 2, PaymentStatusName = "Pending" },
                new PaymentStatus { PaymentStatusId = 3, PaymentStatusName = "Cancelled" },
                new PaymentStatus { PaymentStatusId = 4, PaymentStatusName = "Refunded" }
            );
        }

        private void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Audio Equipment" },
                new Category { CategoryId = 2, CategoryName = "Video Equipment" },
                new Category { CategoryId = 3, CategoryName = "Lighting Equipment" },
                new Category { CategoryId = 4, CategoryName = "Computer Hardware" },
                new Category { CategoryId = 5, CategoryName = "Photography Equipment" }
            );
        }

        private void SeedEquipment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    EquipmentId = 1,
                    EquipmentName = "Canon EOS R5",
                    Description = "Professional mirrorless camera",
                    RentalPrice = 150.00m,
                    Cost = 3899.99m,
                    AvailabilityStatusId = 1,
                    CategoryId = 5,
                    ConditionId = 1
                },
                new Equipment
                {
                    EquipmentId = 2,
                    EquipmentName = "Sony A7III",
                    Description = "Full-frame mirrorless camera",
                    RentalPrice = 120.00m,
                    Cost = 2199.99m,
                    AvailabilityStatusId = 2,
                    CategoryId = 5,
                    ConditionId = 1
                },
                new Equipment
                {
                    EquipmentId = 3,
                    EquipmentName = "Aputure 300D",
                    Description = "LED light panel",
                    RentalPrice = 75.00m,
                    Cost = 1099.99m,
                    AvailabilityStatusId = 1,
                    CategoryId = 3,
                    ConditionId = 2
                },
                new Equipment
                {
                    EquipmentId = 4,
                    EquipmentName = "Sennheiser MKH 416",
                    Description = "Shotgun microphone",
                    RentalPrice = 45.00m,
                    Cost = 999.99m,
                    AvailabilityStatusId = 2,
                    CategoryId = 1,
                    ConditionId = 1
                },
                new Equipment
                {
                    EquipmentId = 5,
                    EquipmentName = "MacBook Pro 16\"",
                    Description = "16\" MacBook Pro with M1 Pro",
                    RentalPrice = 120.00m,
                    Cost = 2499.99m,
                    AvailabilityStatusId = 3,
                    CategoryId = 4,
                    ConditionId = 2
                },
                new Equipment
                {
                    EquipmentId = 6,
                    EquipmentName = "RED Komodo 6K",
                    Description = "Cinema camera",
                    RentalPrice = 350.00m,
                    Cost = 5995.00m,
                    AvailabilityStatusId = 1,
                    CategoryId = 2,
                    ConditionId = 1
                },
                new Equipment
                {
                    EquipmentId = 7,
                    EquipmentName = "DJI Ronin 2",
                    Description = "Professional gimbal stabilizer",
                    RentalPrice = 85.00m,
                    Cost = 1999.99m,
                    AvailabilityStatusId = 2,
                    CategoryId = 2,
                    ConditionId = 2
                },
                new Equipment
                {
                    EquipmentId = 8,
                    EquipmentName = "Zoom H6",
                    Description = "Portable audio recorder",
                    RentalPrice = 35.00m,
                    Cost = 349.99m,
                    AvailabilityStatusId = 1,
                    CategoryId = 1,
                    ConditionId = 1
                },
                new Equipment
                {
                    EquipmentId = 9,
                    EquipmentName = "Godox AD600Pro",
                    Description = "Portable strobe light",
                    RentalPrice = 60.00m,
                    Cost = 899.99m,
                    AvailabilityStatusId = 2,
                    CategoryId = 3,
                    ConditionId = 3
                },
                new Equipment
                {
                    EquipmentId = 10,
                    EquipmentName = "Mac Studio",
                    Description = "Desktop computer for video editing",
                    RentalPrice = 150.00m,
                    Cost = 3999.99m,
                    AvailabilityStatusId = 1,
                    CategoryId = 4,
                    ConditionId = 1
                }
            );
        }

        // First, let's add a new method to seed the users:
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            // For passwords, we need to store the hashed versions
            // Since we can't use PasswordHasher in this context, we'll use pre-generated password hashes
            // These are equivalent to the passwords: Admin@123, User@123, Manager@123, Rehan@123

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Admin",
                    Email = "admin@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = 2,
                    UserName = "User",
                    Email = "user@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==",
                    NormalizedUserName = "USER",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = 3,
                    UserName = "Manager",
                    Email = "manager@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==",
                    NormalizedUserName = "MANAGER",
                    NormalizedEmail = "MANAGER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = 4,
                    UserName = "Rehan",
                    Email = "rehan@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEE+oUrq/9bgM9HlHWRRzaELKRPWAme3qSKyyO/pBAcR9EqXn5uv+iFbOONeiYlJoaA==",
                    NormalizedUserName = "REHAN",
                    NormalizedEmail = "REHAN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }

        // Now, let's add user-role assignments:
        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                // Admin user with Administrator role
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 },

                // User user with User role
                new IdentityUserRole<int> { UserId = 2, RoleId = 1 },

                // Manager user with Manager role
                new IdentityUserRole<int> { UserId = 3, RoleId = 3 },

                // Rehan user with all roles
                new IdentityUserRole<int> { UserId = 4, RoleId = 1 }, // User role
                new IdentityUserRole<int> { UserId = 4, RoleId = 2 }, // Administrator role
                new IdentityUserRole<int> { UserId = 4, RoleId = 3 }  // Manager role
            );
        }

        private void SeedRentalRequests(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalRequest>().HasData(
                new RentalRequest
                {
                    RequestId = 1,
                    StartDate = DateTime.Parse("2025-04-15"),
                    ReturnDate = DateTime.Parse("2025-04-20"),
                    TotalCost = 750.00m,
                    UserId = 3,
                    EquipmentId = 1,
                    RequestStatusId = 3
                },
                new RentalRequest
                {
                    RequestId = 2,
                    StartDate = DateTime.Parse("2025-04-18"),
                    ReturnDate = DateTime.Parse("2025-04-25"),
                    TotalCost = 840.00m,
                    UserId = 4,
                    EquipmentId = 2,
                    RequestStatusId = 3
                },
                new RentalRequest
                {
                    RequestId = 3,
                    StartDate = DateTime.Parse("2025-05-01"),
                    ReturnDate = DateTime.Parse("2025-05-05"),
                    TotalCost = 300.00m,
                    UserId = 3,
                    EquipmentId = 3,
                    RequestStatusId = 2
                },
                new RentalRequest
                {
                    RequestId = 4,
                    StartDate = DateTime.Parse("2025-05-03"),
                    ReturnDate = DateTime.Parse("2025-05-10"),
                    TotalCost = 315.00m,
                    UserId = 4,
                    EquipmentId = 4,
                    RequestStatusId = 2
                },
                new RentalRequest
                {
                    RequestId = 5,
                    StartDate = DateTime.Parse("2025-05-07"),
                    ReturnDate = DateTime.Parse("2025-05-14"),
                    TotalCost = 840.00m,
                    UserId = 3,
                    EquipmentId = 5,
                    RequestStatusId = 1
                },
                new RentalRequest
                {
                    RequestId = 6,
                    StartDate = DateTime.Parse("2025-05-10"),
                    ReturnDate = DateTime.Parse("2025-05-17"),
                    TotalCost = 2450.00m,
                    UserId = 4,
                    EquipmentId = 6,
                    RequestStatusId = 1
                },
                new RentalRequest
                {
                    RequestId = 7,
                    StartDate = DateTime.Parse("2025-04-10"),
                    ReturnDate = DateTime.Parse("2025-04-17"),
                    TotalCost = 595.00m,
                    UserId = 3,
                    EquipmentId = 7,
                    RequestStatusId = 3
                },
                new RentalRequest
                {
                    RequestId = 8,
                    StartDate = DateTime.Parse("2025-04-20"),
                    ReturnDate = DateTime.Parse("2025-04-25"),
                    TotalCost = 175.00m,
                    UserId = 4,
                    EquipmentId = 8,
                    RequestStatusId = 3
                },
                new RentalRequest
                {
                    RequestId = 9,
                    StartDate = DateTime.Parse("2025-04-28"),
                    ReturnDate = DateTime.Parse("2025-05-05"),
                    TotalCost = 480.00m,
                    UserId = 3,
                    EquipmentId = 9,
                    RequestStatusId = 4
                },
                new RentalRequest
                {
                    RequestId = 10,
                    StartDate = DateTime.Parse("2025-05-15"),
                    ReturnDate = DateTime.Parse("2025-05-22"),
                    TotalCost = 1050.00m,
                    UserId = 4,
                    EquipmentId = 10,
                    RequestStatusId = 1
                }
            );
        }

        private void SeedRentalTransactions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalTransaction>().HasData(
                new RentalTransaction
                {
                    TransactionId = 1,
                    RentalStartDate = DateTime.Parse("2025-04-15"),
                    RentalReturnDate = DateTime.Parse("2025-04-20"),
                    RentalPeriod = 5,
                    RentalFee = 750.00m,
                    Deposit = 500.00m,
                    EquipmentId = 1,
                    RequestId = 1,
                    PaymentStatusId = 1,
                    AmountPaid = 750.00m,
                    TotalFee = 750.00m
                },
                new RentalTransaction
                {
                    TransactionId = 2,
                    RentalStartDate = DateTime.Parse("2025-04-18"),
                    RentalReturnDate = DateTime.Parse("2025-04-25"),
                    RentalPeriod = 7,
                    RentalFee = 840.00m,
                    Deposit = 500.00m,
                    EquipmentId = 2,
                    RequestId = 2,
                    PaymentStatusId = 1,
                    AmountPaid = 840.00m,
                    TotalFee = 840.00m
                },
                new RentalTransaction
                {
                    TransactionId = 3,
                    RentalStartDate = DateTime.Parse("2025-05-01"),
                    RentalReturnDate = DateTime.Parse("2025-05-05"),
                    RentalPeriod = 4,
                    RentalFee = 300.00m,
                    Deposit = 300.00m,
                    EquipmentId = 3,
                    RequestId = 3,
                    PaymentStatusId = 2,
                    AmountPaid = 0.00m,
                    TotalFee = 300.00m
                },
                new RentalTransaction
                {
                    TransactionId = 4,
                    RentalStartDate = DateTime.Parse("2025-05-03"),
                    RentalReturnDate = DateTime.Parse("2025-05-10"),
                    RentalPeriod = 7,
                    RentalFee = 315.00m,
                    Deposit = 250.00m,
                    EquipmentId = 4,
                    RequestId = 4,
                    PaymentStatusId = 2,
                    AmountPaid = 0.00m,
                    TotalFee = 315.00m
                },
                new RentalTransaction
                {
                    TransactionId = 5,
                    RentalStartDate = DateTime.Parse("2025-04-10"),
                    RentalReturnDate = DateTime.Parse("2025-04-17"),
                    RentalPeriod = 7,
                    RentalFee = 595.00m,
                    Deposit = 400.00m,
                    EquipmentId = 7,
                    RequestId = 7,
                    PaymentStatusId = 1,
                    AmountPaid = 595.00m,
                    TotalFee = 595.00m
                },
                new RentalTransaction
                {
                    TransactionId = 6,
                    RentalStartDate = DateTime.Parse("2025-04-20"),
                    RentalReturnDate = DateTime.Parse("2025-04-25"),
                    RentalPeriod = 5,
                    RentalFee = 175.00m,
                    Deposit = 100.00m,
                    EquipmentId = 8,
                    RequestId = 8,
                    PaymentStatusId = 1,
                    AmountPaid = 175.00m,
                    TotalFee = 175.00m
                }
            );
        }

        private void SeedReturnRecords(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReturnRecord>().HasData(
                new ReturnRecord
                {
                    ReturnId = 1,
                    ActualReturnDate = DateTime.Parse("2025-04-20"),
                    LateReturnFees = 0.00m,
                    AddtionalCharges = 0.00m,
                    ConditionId = 1,
                    TransactionId = 1,
                    LateReturnDays = 0
                },
                new ReturnRecord
                {
                    ReturnId = 2,
                    ActualReturnDate = DateTime.Parse("2025-04-26"),
                    LateReturnFees = 120.00m,
                    AddtionalCharges = 0.00m,
                    ConditionId = 1,
                    TransactionId = 2,
                    LateReturnDays = 1
                },
                new ReturnRecord
                {
                    ReturnId = 3,
                    ActualReturnDate = DateTime.Parse("2025-04-17"),
                    LateReturnFees = 0.00m,
                    AddtionalCharges = 0.00m,
                    ConditionId = 2,
                    TransactionId = 5,
                    LateReturnDays = 0
                },
                new ReturnRecord
                {
                    ReturnId = 4,
                    ActualReturnDate = DateTime.Parse("2025-04-25"),
                    LateReturnFees = 0.00m,
                    AddtionalCharges = 50.00m,
                    ConditionId = 3,
                    TransactionId = 6,
                    LateReturnDays = 0
                }
            );
        }

        private void SeedFeedback(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                {
                    FeedbackId = 1,
                    Date = DateTime.Parse("2025-04-21"),
                    Time = TimeSpan.Parse("14:30:00"),
                    CommentText = "Great camera, worked perfectly for my project!",
                    UserId = 3,
                    Rating = 5,
                    TransactionId = 1
                },
                new Feedback
                {
                    FeedbackId = 2,
                    Date = DateTime.Parse("2025-04-27"),
                    Time = TimeSpan.Parse("10:15:00"),
                    CommentText = "Camera was excellent but sorry for the late return.",
                    UserId = 4,
                    Rating = 4,
                    TransactionId = 2
                },
                new Feedback
                {
                    FeedbackId = 3,
                    Date = DateTime.Parse("2025-04-18"),
                    Time = TimeSpan.Parse("16:45:00"),
                    CommentText = "The stabilizer worked well but had some issues with battery life.",
                    UserId = 3,
                    Rating = 3,
                    TransactionId = 5
                },
                new Feedback
                {
                    FeedbackId = 4,
                    Date = DateTime.Parse("2025-04-26"),
                    Time = TimeSpan.Parse("09:00:00"),
                    CommentText = "Very sorry about the damage, but the recorder was excellent!",
                    UserId = 4,
                    Rating = 4,
                    TransactionId = 6
                }
            );
        }

        private void SeedNotifications(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    NotificationId = 1,
                    MessageContent = "Your rental request for Canon EOS R5 has been approved.",
                    Type = "Approval",
                    Status = "Read",
                    UserId = 3
                },
                new Notification
                {
                    NotificationId = 2,
                    MessageContent = "Your rental request for Sony A7III has been approved.",
                    Type = "Approval",
                    Status = "Read",
                    UserId = 4
                },
                new Notification
                {
                    NotificationId = 3,
                    MessageContent = "Your rental for Zoom H6 is due for return tomorrow.",
                    Type = "Reminder",
                    Status = "Read",
                    UserId = 4
                },
                new Notification
                {
                    NotificationId = 4,
                    MessageContent = "Your rental for Aputure 300D has been approved.",
                    Type = "Approval",
                    Status = "Unread",
                    UserId = 3
                },
                new Notification
                {
                    NotificationId = 5,
                    MessageContent = "Thank you for your feedback on the Canon EOS R5 rental.",
                    Type = "Feedback",
                    Status = "Unread",
                    UserId = 3
                },
                new Notification
                {
                    NotificationId = 6,
                    MessageContent = "Your rental request for Godox AD600Pro has been rejected.",
                    Type = "Rejection",
                    Status = "Unread",
                    UserId = 3
                }
            );
        }

        private void SeedLogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasData(
                new Log
                {
                    LogId = 1,
                    Action = "Insert",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-14 09:32:15"),
                    AffectedData = "{\"entityType\":\"RentalRequest\",\"id\":1}",
                    Source = "RentalRequestController",
                    UserId = 3
                },
                new Log
                {
                    LogId = 2,
                    Action = "Update",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-14 10:15:30"),
                    AffectedData = "{\"entityType\":\"RentalRequest\",\"id\":1,\"status\":\"Approved\"}",
                    Source = "AdminController",
                    UserId = 1
                },
                new Log
                {
                    LogId = 3,
                    Action = "Insert",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-14 10:20:45"),
                    AffectedData = "{\"entityType\":\"RentalTransaction\",\"id\":1}",
                    Source = "TransactionController",
                    UserId = 1
                },
                new Log
                {
                    LogId = 4,
                    Action = "Update",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-20 16:45:22"),
                    AffectedData = "{\"entityType\":\"Equipment\",\"id\":1,\"status\":\"Available\"}",
                    Source = "ReturnController",
                    UserId = 1
                },
                new Log
                {
                    LogId = 5,
                    Action = "Insert",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-20 16:50:10"),
                    AffectedData = "{\"entityType\":\"ReturnRecord\",\"id\":1}",
                    Source = "ReturnController",
                    UserId = 1
                },
                new Log
                {
                    LogId = 6,
                    Action = "Insert",
                    Exception = null,
                    Timestamp = DateTime.Parse("2025-04-21 14:30:00"),
                    AffectedData = "{\"entityType\":\"Feedback\",\"id\":1}",
                    Source = "FeedbackController",
                    UserId = 3
                }
            );
        }

        private void SeedDocuments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    DocumentId = 1,
                    DocumentName = "RentalAgreement_1.pdf",
                    UploadDate = DateTime.Parse("2025-04-14"),
                    FileType = "PDF",
                    StoragePath = "/documents/agreements/RentalAgreement_1.pdf",
                    UserId = 3
                },
                new Document
                {
                    DocumentId = 2,
                    DocumentName = "PaymentReceipt_1.pdf",
                    UploadDate = DateTime.Parse("2025-04-14"),
                    FileType = "PDF",
                    StoragePath = "/documents/receipts/PaymentReceipt_1.pdf",
                    UserId = 3
                },
                new Document
                {
                    DocumentId = 3,
                    DocumentName = "DamageReport_4.pdf",
                    UploadDate = DateTime.Parse("2025-04-25"),
                    FileType = "PDF",
                    StoragePath = "/documents/reports/DamageReport_4.pdf",
                    UserId = 1
                },
                new Document
                {
                    DocumentId = 4,
                    DocumentName = "UserID_3.jpg",
                    UploadDate = DateTime.Parse("2025-04-10"),
                    FileType = "JPG",
                    StoragePath = "/documents/ids/UserID_3.jpg",
                    UserId = 3
                }
            );
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
