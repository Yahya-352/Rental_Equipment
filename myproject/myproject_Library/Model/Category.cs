using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myproject_Library.Model
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Equipment = new HashSet<Equipment>();
        }

        [Key]
        [Column("Category_ID")]
        public int CategoryId { get; set; }
        [Column("Category_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CategoryName { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
