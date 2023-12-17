using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRPicks.Models;
using PRPicks.Services;

namespace PRPicks.Controllers
{
    public class CartsController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartsController(CartService cartService, ApplicationDbContext context)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            // Get our cart (either an existing or generate a new one)
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            // If the cart has items, we need to add the product reference for those items
            if (cart.CartItems.Count > 0)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    /*
                        SELECT * FROM Products
                        JOIN Departments ON Products.DepartmentId = Departments.Id
                        WHERE Products.Id = 1 LIMIT 1
                    */
                    var product = await _context.Products
                        .Include(p => p.Collection)
                        .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

                    if (product != null)
                    {
                        cartItem.Product = product;
                    }
                }
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Getting the active cart
            var cart = _cartService.GetCart();

            if (cart == null) {
                return NotFound();
            }

            // Checking if item already is in the cart
            var cartItem = cart.CartItems.Find(cartItem => cartItem.ProductId == productId);

            if (cartItem != null && cartItem.Product != null)
            {
                cartItem.Quantity += quantity;
            }

            else
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                {
                    return NotFound();
                }

                cartItem = new CartItem { ProductId = productId, Quantity = quantity, Product = product };
                cart.CartItems.Add(cartItem);
                
            }

            _cartService.SaveCart(cart);
            
            return RedirectToAction("");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);

                _cartService.SaveCart(cart);
            }

            return RedirectToAction("Index");
        }      
    }

}