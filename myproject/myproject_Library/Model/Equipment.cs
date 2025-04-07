using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    public partial class Equipment
    {
        public Equipment()
        {
            RentalRequests = new HashSet<RentalRequest>();
            RentalTransactions = new HashSet<RentalTransaction>();
        }

        [Key]
        [Column("Equipment_ID")]
        public int EquipmentId { get; set; }
        [Column("Equipment_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EquipmentName { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("Rental_Price", TypeName = "numeric(18, 0)")]
        public decimal? RentalPrice { get; set; }
        [Column("Availability_Status_ID")]
        public int? AvailabilityStatusId { get; set; }
        [Column("Category_ID")]
        public int? CategoryId { get; set; }
        [Column("Condition_ID")]
        public int? ConditionId { get; set; }
        [Column("Late_Penalty_Percentage", TypeName = "numeric(18, 0)")]
        public decimal? LatePenaltyPercentage { get; set; }

        [ForeignKey("AvailabilityStatusId")]
        [InverseProperty("Equipment")]
        public virtual EquipmentAvailability? AvailabilityStatus { get; set; }
        [ForeignKey("CategoryId")]
        [InverseProperty("Equipment")]
        public virtual Category? Category { get; set; }
        [ForeignKey("ConditionId")]
        [InverseProperty("Equipment")]
        public virtual EquipmentCondition? Condition { get; set; }
        [InverseProperty("Equipment")]
        public virtual ICollection<RentalRequest> RentalRequests { get; set; }
        [InverseProperty("Equipment")]
        public virtual ICollection<RentalTransaction> RentalTransactions { get; set; }
    }
}
