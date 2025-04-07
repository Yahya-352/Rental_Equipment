using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Rental_Request")]
    public partial class RentalRequest
    {
        public RentalRequest()
        {
            RentalTransactions = new HashSet<RentalTransaction>();
        }

        [Key]
        [Column("Request_ID")]
        public int RequestId { get; set; }
        [Column("Start_Date", TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column("return_Date", TypeName = "date")]
        public DateTime? ReturnDate { get; set; }
        [Column("Total_Cost", TypeName = "numeric(18, 0)")]
        public decimal? TotalCost { get; set; }
        [Column("User_ID")]
        public int? UserId { get; set; }
        [Column("Equipment_ID")]
        public int? EquipmentId { get; set; }
        [Column("Request_Status_ID")]
        public int? RequestStatusId { get; set; }

        [ForeignKey("EquipmentId")]
        [InverseProperty("RentalRequests")]
        public virtual Equipment? Equipment { get; set; }
        [ForeignKey("RequestStatusId")]
        [InverseProperty("RentalRequests")]
        public virtual RequestStatus? RequestStatus { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("RentalRequests")]
        public virtual User? User { get; set; }
        [InverseProperty("Request")]
        public virtual ICollection<RentalTransaction> RentalTransactions { get; set; }
    }
}
