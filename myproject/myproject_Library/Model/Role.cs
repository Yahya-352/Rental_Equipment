using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("Role_ID")]
        public int RoleId { get; set; }
        [Column("Role_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? RoleName { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
    }
}
