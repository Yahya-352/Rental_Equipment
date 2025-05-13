using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;
using System;
using System.Linq;

namespace myproject_Library.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EquipmentDBContext context)
        {
            // Make sure the database is created
            context.Database.EnsureCreated();

            // Look for any equipment to check if the database has been seeded
            if (context.Equipment.Any())
            {
                return; // DB has been seeded
            }

            // Seed Categories
            var categories = new Category[]
            {
                new Category { CategoryName = "Power Tools", Description = "Electric and battery-powered tools" },
                new Category { CategoryName = "Hand Tools", Description = "Manual tools for various tasks" },
                new Category { CategoryName = "Construction Equipment", Description = "Heavy equipment for construction sites" },
                new Category { CategoryName = "Gardening Tools", Description = "Tools for gardening and landscaping" },
                new Category { CategoryName = "Cleaning Equipment", Description = "Equipment for cleaning tasks" }
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            // Seed Equipment Availability Statuses
            var availabilityStatuses = new EquipmentAvailability[]
            {
                new EquipmentAvailability { AvailabilityStatusName = "Available" },
                new EquipmentAvailability { AvailabilityStatusName = "Rented" },
                new EquipmentAvailability { AvailabilityStatusName = "Under Maintenance" },
                new EquipmentAvailability { AvailabilityStatusName = "Reserved" }
            };

            foreach (var status in availabilityStatuses)
            {
                context.EquipmentAvailabilities.Add(status);
            }
            context.SaveChanges();

            // Seed Equipment Conditions
            var conditions = new EquipmentCondition[]
            {
                new EquipmentCondition { ConditionName = "New" },
                new EquipmentCondition { ConditionName = "Excellent" },
                new EquipmentCondition { ConditionName = "Good" },
                new EquipmentCondition { ConditionName = "Fair" },
                new EquipmentCondition { ConditionName = "Poor" }
            };

            foreach (var condition in conditions)
            {
                context.EquipmentConditions.Add(condition);
            }
            context.SaveChanges();

            // Get the saved categories, statuses, and conditions for reference
            var powerToolsCategory = context.Categories.Single(c => c.CategoryName == "Power Tools");
            var handToolsCategory = context.Categories.Single(c => c.CategoryName == "Hand Tools");
            var constructionCategory = context.Categories.Single(c => c.CategoryName == "Construction Equipment");
            var gardeningCategory = context.Categories.Single(c => c.CategoryName == "Gardening Tools");
            var cleaningCategory = context.Categories.Single(c => c.CategoryName == "Cleaning Equipment");

            var availableStatus = context.EquipmentAvailabilities.Single(s => s.AvailabilityStatusName == "Available");
            var rentedStatus = context.EquipmentAvailabilities.Single(s => s.AvailabilityStatusName == "Rented");
            var maintenanceStatus = context.EquipmentAvailabilities.Single(s => s.AvailabilityStatusName == "Under Maintenance");

            var newCondition = context.EquipmentConditions.Single(c => c.ConditionName == "New");
            var excellentCondition = context.EquipmentConditions.Single(c => c.ConditionName == "Excellent");
            var goodCondition = context.EquipmentConditions.Single(c => c.ConditionName == "Good");
            var fairCondition = context.EquipmentConditions.Single(c => c.ConditionName == "Fair");

            // Seed Equipment
            var equipment = new Equipment[]
            {
                // Power Tools
                new Equipment {
                    EquipmentName = "Electric Drill",
                    Description = "Powerful electric drill with variable speed",
                    RentalPrice = 25.00M,
                    Cost = 150.00M,
                    CategoryId = powerToolsCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = newCondition.ConditionId,
                    LatePenaltyPercentage = 10.00M
                },
                new Equipment {
                    EquipmentName = "Circular Saw",
                    Description = "7-1/4 inch circular saw for cutting wood",
                    RentalPrice = 30.00M,
                    Cost = 200.00M,
                    CategoryId = powerToolsCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = excellentCondition.ConditionId,
                    LatePenaltyPercentage = 10.00M
                },
                new Equipment {
                    EquipmentName = "Angle Grinder",
                    Description = "4.5 inch angle grinder for cutting and grinding",
                    RentalPrice = 20.00M,
                    Cost = 120.00M,
                    CategoryId = powerToolsCategory.CategoryId,
                    AvailabilityStatusId = rentedStatus.AvailabilityStatusId,
                    ConditionId = goodCondition.ConditionId,
                    LatePenaltyPercentage = 15.00M
                },
                
                // Hand Tools
                new Equipment {
                    EquipmentName = "Wrench Set",
                    Description = "Complete set of combination wrenches",
                    RentalPrice = 15.00M,
                    Cost = 80.00M,
                    CategoryId = handToolsCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = newCondition.ConditionId,
                    LatePenaltyPercentage = 5.00M
                },
                new Equipment {
                    EquipmentName = "Hammer",
                    Description = "16oz claw hammer",
                    RentalPrice = 8.00M,
                    Cost = 25.00M,
                    CategoryId = handToolsCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = excellentCondition.ConditionId,
                    LatePenaltyPercentage = 5.00M
                },
                
                // Construction Equipment
                new Equipment {
                    EquipmentName = "Concrete Mixer",
                    Description = "3.5 cubic feet concrete mixer",
                    RentalPrice = 75.00M,
                    Cost = 500.00M,
                    CategoryId = constructionCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = goodCondition.ConditionId,
                    LatePenaltyPercentage = 20.00M
                },
                new Equipment {
                    EquipmentName = "Jackhammer",
                    Description = "Electric jackhammer for breaking concrete",
                    RentalPrice = 60.00M,
                    Cost = 400.00M,
                    CategoryId = constructionCategory.CategoryId,
                    AvailabilityStatusId = maintenanceStatus.AvailabilityStatusId,
                    ConditionId = fairCondition.ConditionId,
                    LatePenaltyPercentage = 20.00M
                },
                
                // Gardening Tools
                new Equipment {
                    EquipmentName = "Lawn Mower",
                    Description = "Gas-powered lawn mower with bag",
                    RentalPrice = 40.00M,
                    Cost = 250.00M,
                    CategoryId = gardeningCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = excellentCondition.ConditionId,
                    LatePenaltyPercentage = 15.00M
                },
                new Equipment {
                    EquipmentName = "Hedge Trimmer",
                    Description = "Electric hedge trimmer with 22-inch blade",
                    RentalPrice = 25.00M,
                    Cost = 120.00M,
                    CategoryId = gardeningCategory.CategoryId,
                    AvailabilityStatusId = rentedStatus.AvailabilityStatusId,
                    ConditionId = goodCondition.ConditionId,
                    LatePenaltyPercentage = 10.00M
                },
                
                // Cleaning Equipment
                new Equipment {
                    EquipmentName = "Pressure Washer",
                    Description = "2000 PSI electric pressure washer",
                    RentalPrice = 45.00M,
                    Cost = 300.00M,
                    CategoryId = cleaningCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = newCondition.ConditionId,
                    LatePenaltyPercentage = 15.00M
                },
                new Equipment {
                    EquipmentName = "Carpet Cleaner",
                    Description = "Professional carpet cleaning machine",
                    RentalPrice = 35.00M,
                    Cost = 250.00M,
                    CategoryId = cleaningCategory.CategoryId,
                    AvailabilityStatusId = availableStatus.AvailabilityStatusId,
                    ConditionId = excellentCondition.ConditionId,
                    LatePenaltyPercentage = 10.00M
                }
            };

            foreach (var item in equipment)
            {
                context.Equipment.Add(item);
            }
            context.SaveChanges();
        }
    }
}
