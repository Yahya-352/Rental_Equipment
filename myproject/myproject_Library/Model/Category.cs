﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Category_ID")]
        public int CategoryId { get; set; }
        [Column("Category_Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CategoryName { get; set; }

        [Column("Category_Description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }  // ✅ New description field

        [InverseProperty("Category")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
