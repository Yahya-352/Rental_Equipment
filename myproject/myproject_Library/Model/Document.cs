using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Document")]
    public partial class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Document_ID")]
        public int DocumentId { get; set; }
        [Column("Document_Name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? DocumentName { get; set; }
        [Column("Upload_Date", TypeName = "date")]
        public DateTime? UploadDate { get; set; }
        [Column("File_Type")]
        [StringLength(50)]
        [Unicode(false)]
        public string? FileType { get; set; }
        [Column("Storage_Path")]
        [StringLength(50)]
        [Unicode(false)]
        public string? StoragePath { get; set; }
        [Column("User_ID")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Documents")]
        public virtual User? User { get; set; }

        [Column("Transaction_ID")]
        public int? TransactionId { get; set; }

        [ForeignKey("TransactionId")]
        [InverseProperty("Documents")]
        public virtual RentalTransaction? Transaction { get; set; }

    }
}
