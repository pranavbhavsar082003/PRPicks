using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRPicks.Models
{
    public class Product
    {
        public int Id { get; set; } = 0;

        [Required]
        [Display(Name = "Collection")]
        public int CollectionId { get; set; } = 0;  

        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Image { get; set; } = string.Empty;

        [Key]
        public int SKU { get; set; } = 0;

        [Required]
        [Range(0.01, 999999.99)]
        [DataType(DataType.Currency)]
        public decimal MSRP { get; set; } = 0.01M;

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Weight { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        
        [ForeignKey("CollectionId")]  
        public virtual Collection? Collection { get; set; } 
    }
}

