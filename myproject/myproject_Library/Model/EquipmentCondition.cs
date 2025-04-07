using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Equipment_Condition")]
    public partial class EquipmentCondition
    {
        public EquipmentCondition()
        {
            Equipment = new HashSet<Equipment>();
            ReturnRecords = new HashSet<ReturnRecord>();
        }

        [Key]
        [Column("Condition_ID")]
        public int ConditionId { get; set; }
        [Column("Condition_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ConditionName { get; set; }

        [InverseProperty("Condition")]
        public virtual ICollection<Equipment> Equipment { get; set; }
        [InverseProperty("Condition")]
        public virtual ICollection<ReturnRecord> ReturnRecords { get; set; }
    }
}
