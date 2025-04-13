using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Request_Status")]
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            RentalRequests = new HashSet<RentalRequest>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Request_Status_ID")]
        public int RequestStatusId { get; set; }
        [Column("Request_Status_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? RequestStatusName { get; set; }

        [InverseProperty("RequestStatus")]
        public virtual ICollection<RentalRequest> RentalRequests { get; set; }
    }
}
