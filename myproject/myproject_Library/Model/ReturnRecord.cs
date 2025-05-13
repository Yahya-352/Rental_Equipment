using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Return_Record")]
    public partial class ReturnRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Return_ID")]
        public int ReturnId { get; set; }
        [Column("Actual_Return_Date", TypeName = "date")]
        public DateTime? ActualReturnDate { get; set; }
        [Column("Late_Return_fees", TypeName = "numeric(18, 0)")]
        public decimal? LateReturnFees { get; set; }
        [Column("Addtional_Charges", TypeName = "numeric(18, 0)")]
        public decimal? AddtionalCharges { get; set; }
        [Column("Condition_ID")]
        public int? ConditionId { get; set; }
        [Column("Transaction_ID")]
        public int? TransactionId { get; set; }

        [ForeignKey("ConditionId")]
        [InverseProperty("ReturnRecords")]
        public virtual EquipmentCondition? Condition { get; set; }
        [ForeignKey("TransactionId")]
        [InverseProperty("ReturnRecords")]
        public virtual RentalTransaction? Transaction { get; set; }
    }
}
