using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRPicks.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required, StringLength(300)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;  

        public virtual ICollection<Product>? Products { get; set; }
    }
}
