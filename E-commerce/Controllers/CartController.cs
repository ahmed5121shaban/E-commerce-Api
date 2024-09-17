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
        public UserManager<User> UserManager { get; }
        public CartManager cartManager { get; }
        public ProductManager Product { get; }

        public CartController(UserManager<User> user, CartManager _cartManager, ProductManager product)
        {
            UserManager = user;
            cartManager = _cartManager;
            Product = product;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ProductViewModel pro = Product.GetByID(id).Single().MapToView();

            cartManager.AddToCart(user.Id, pro);
            return RedirectToAction("AddToCartPage", "cart");
        }


        public IActionResult Delete(int id)
        {
            var cart = cartManager.GetAll().Where(c => c.ID == id).FirstOrDefault();

            cartManager.Delete(cart);
            return RedirectToAction("AddToCartPage");
        }
    }
}
