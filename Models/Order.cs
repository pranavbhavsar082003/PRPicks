using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PRPicks.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        public string UserId { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; } = 0.00M;

        [Required]
        public bool PaymentReceived { get; set; } = false;

        public IdentityUser? User{ get; set; } 

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}