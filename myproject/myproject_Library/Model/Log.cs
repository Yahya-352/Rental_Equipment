using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    public partial class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Log_ID")]
        public int LogId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Action { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Exception { get; set; }
        public DateTime? Timestamp { get; set; }
        [Column("affected_data")]
        [StringLength(50)]
        [Unicode(false)]
        public string? AffectedData { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Source { get; set; }
        [Column("User_ID")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Logs")]
        public virtual User? User { get; set; }
    }
}
