using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Equipment_Availability")]
    public partial class EquipmentAvailability
    {
        public EquipmentAvailability()
        {
            Equipment = new HashSet<Equipment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Availability_Status_ID")]
        public int AvailabilityStatusId { get; set; }
        [Column("Availability_Status_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? AvailabilityStatusName { get; set; }

        [InverseProperty("AvailabilityStatus")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
