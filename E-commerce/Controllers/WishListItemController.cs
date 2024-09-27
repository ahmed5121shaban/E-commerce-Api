using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListItemController : ControllerBase
    {
        private readonly WishListItemManager wishList;

        public WishListItemController(WishListItemManager wishList)
        {
            this.wishList = wishList;
        }

        [HttpGet("wishListitems")]
        public IActionResult GetWishListItems()
        {
            try
            {
                var wishListitem = wishList.GetAll();
                if (!wishListitem.Any()) return NoContent();
                return Ok(wishListitem);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        [HttpGet("WishListItem/{id:int}")]
        public async Task<IActionResult> GetWishListItem(int id)
        {
            try
            {
                var wishListitem = wishList.Get(id);
                if (wishListitem == null) return NoContent();
                return Ok(wishListitem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult AddWishListItem(WishListItemViewModel _wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "this data not completed" });
            }

            try
            {
                wishList.Add(_wishList);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWishListItem(int id)
        {
            try
            {
                await wishList.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("wishlistuser/{id:alpha}")]
        public IActionResult WishListUser(string id) {
            try 
            {
                var wishListItems = wishList.GetWishListItemOfUser(id);
                return Ok(wishListItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
