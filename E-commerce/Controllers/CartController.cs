using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using ViewModel;

namespace E_commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> logger;

        public UserManager<User> UserManager { get; }
        public CartManager cartManager { get; }
        public ProductManager Product { get; }

        public CartController(UserManager<User> user, CartManager _cartManager, ProductManager product,
            ILogger<CartController> _logger)
        {
            UserManager = user;
            cartManager = _cartManager;
            Product = product;
            logger = _logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (user == null)
                return NotFound();

            ProductViewModel pro = Product.GetByID(id).Single().MapToView();
            cartManager.AddToCart(user.Id, pro);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            var cart = cartManager.GetAll().Where(c => c.ID == id).FirstOrDefault();
            if (cart == null) return NotFound();

            cartManager.Delete(cart);
            return Ok();
        }

        //GET /cart - Get cart items for the current user.
        [HttpGet]
        public IActionResult GetCart()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null) return NotFound(new { massage = "the user not found" });
            var cartList = cartManager.GetAll().Where(c => c.UserID == userID).ToList();
            if (cartList.Count == 0) return NoContent();
            return Ok(cartList);
        }
        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null) return NotFound(new {massage="the user not found"});
            var cartItems = cartManager.GetAll().Where(c => c.UserID == userID);
            if(!cartItems.Any()) return NoContent();
            foreach (var item in cartItems)
            {
                cartManager.Delete(item);
            }
            return Ok();
        }
    }
}
