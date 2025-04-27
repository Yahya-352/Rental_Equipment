using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Role")]
    public partial class Role: IdentityRole<int>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column("Role_ID")]
        public override int Id { get => base.Id; set => base.Id = value; }
        [Column("Role_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public override string Name { get => base.Name; set => base.Name = value; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
    }
}
