using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Rental_Transaction")]
    public partial class RentalTransaction
    {
        public RentalTransaction()
        {
            Feedbacks = new HashSet<Feedback>();
            ReturnRecords = new HashSet<ReturnRecord>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Transaction_ID")]
        public int TransactionId { get; set; }
        [Column("Rental_Start_Date", TypeName = "date")]
        public DateTime? RentalStartDate { get; set; }
        [Column("Rental_Return_Date", TypeName = "date")]
        public DateTime? RentalReturnDate { get; set; }
        [Column("Rental_Period")]
        public int? RentalPeriod { get; set; }
        [Column("Rental_Fee", TypeName = "numeric(18, 0)")]
        public decimal? RentalFee { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Deposit { get; set; }
        [Column("Equipment_ID")]
        public int? EquipmentId { get; set; }
        [Column("Request_ID")]
        public int? RequestId { get; set; }
        [Column("Payment_Status_ID")]
        public int? PaymentStatusId { get; set; }

        //NEW
        [Column("Amount_Paid", TypeName = "numeric(18, 0)")]
        public decimal? AmountPaid { get; set; }

        [Column("Total_Fee", TypeName = "numeric(18, 0)")]
        public decimal? TotalFee { get; set; }




        [ForeignKey("EquipmentId")]
        [InverseProperty("RentalTransactions")]
        public virtual Equipment? Equipment { get; set; }
        [ForeignKey("PaymentStatusId")]
        [InverseProperty("RentalTransactions")]
        public virtual PaymentStatus? PaymentStatus { get; set; }
        [ForeignKey("RequestId")]
        [InverseProperty("RentalTransactions")]
        public virtual RentalRequest? Request { get; set; }
        [InverseProperty("Transaction")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [InverseProperty("Transaction")]
        public virtual ICollection<ReturnRecord> ReturnRecords { get; set; }

        [InverseProperty("Transaction")]
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}
