using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Payment_Status")]
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            RentalTransactions = new HashSet<RentalTransaction>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Payment_Status_ID")]
        public int PaymentStatusId { get; set; }
        [Column("Payment_Status_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? PaymentStatusName { get; set; }

        [InverseProperty("PaymentStatus")]
        public virtual ICollection<RentalTransaction> RentalTransactions { get; set; }
    }
}
