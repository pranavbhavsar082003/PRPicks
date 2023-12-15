using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace PRPicks.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        public int OrderId { get; set; } = 0;

        [Required]
        public string ProductName { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0.00M;

        [Required]
        public int Quantity { get; set; } = 0;

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}