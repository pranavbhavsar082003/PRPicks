using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRPicks.Models;

namespace PRPicks.Controllers
{
    public class CartsController : Controller
    {
        private readonly string _cartSessionKey;
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _cartSessionKey = "Cart";
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            if (cart.CartItems.Count > 0)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    var product = await _context.Products
                        .Include(p => p.Collection)
                        .FirstOrDefaultAsync(p => p.CollectionId == cartItem.ProductId);

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
    var cart = GetCart();

    if (cart == null)
    {
        return NotFound();
    }

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

    SaveCart(cart);

    return RedirectToAction("Index");
}

        private Cart? GetCart()
        {
            var cartJson = HttpContext.Session.GetString(_cartSessionKey);
            return cartJson == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(_cartSessionKey, cartJson);
        }
    }
}