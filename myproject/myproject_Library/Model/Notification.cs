using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    public partial class Notification
    {
        [Key]
        [Column("Notification_ID")]
        public int NotificationId { get; set; }
        [Column("Message_content")]
        [StringLength(50)]
        [Unicode(false)]
        public string? MessageContent { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Type { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; }
        [Column("User_ID")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notifications")]
        public virtual User? User { get; set; }
    }
}
