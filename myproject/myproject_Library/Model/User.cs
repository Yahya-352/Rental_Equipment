using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("User")]
    public partial class User: IdentityUser<int>
    {
        public User()
        {
            Documents = new HashSet<Document>();
            Feedbacks = new HashSet<Feedback>();
            Logs = new HashSet<Log>();
            Notifications = new HashSet<Notification>();
            RentalRequests = new HashSet<RentalRequest>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("User_ID")]
        public override int Id { get => base.Id; set => base.Id = value; }
        [Column("username")]
        [StringLength(50)]
        [Unicode(false)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        [Column("password", TypeName = "varchar(MAX)")]
        [Unicode(false)]
        public string Password { get => PasswordHash; set => PasswordHash = value; }
        [Column("email")]
        [StringLength(50)]
        [Unicode(false)]
        public override string Email { get => base.Email; set => base.Email = value; }
        [Column("Role_ID")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role? Role { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Document> Documents { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Log> Logs { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<RentalRequest> RentalRequests { get; set; }
    }
}
