using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Feedback_ID")]
        public int FeedbackId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        [Column("time")]
        public TimeSpan? Time { get; set; }
        [Column("Comment_Text")]
        [StringLength(150)]
        [Unicode(false)]
        public string? CommentText { get; set; }
        [Column("User_ID")]
        public int? UserId { get; set; }
        [Column("Transaction_ID")]
        public int? TransactionId { get; set; }

        [ForeignKey("TransactionId")]
        [InverseProperty("Feedbacks")]
        public virtual RentalTransaction? Transaction { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Feedbacks")]
        public virtual User? User { get; set; }
    }
}
