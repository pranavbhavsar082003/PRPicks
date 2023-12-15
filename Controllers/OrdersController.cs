using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRPicks.Models;
using PRPicks.Services;

namespace PRPicks.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CartService _cartService;
        private ApplicationDbContext _context;

        public OrdersController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }


        [Authorize()]
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }
            var order = new Order{
                UserId = userId,
                Total = cart.CartItems.Sum(CartItem => (decimal)(CartItem.Quantity * CartItem.Product.MSRP)),
                OrderItems = new List<OrderItem>()
            };
            
            foreach(var CartItem in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem{
                    OrderId = order.Id,
                    ProductName = CartItem.Product.Name,
                    Quantity = CartItem.Quantity,
                    Price = CartItem.Product.MSRP
                });
            }

            return View("Details", order);
        }
    }
}