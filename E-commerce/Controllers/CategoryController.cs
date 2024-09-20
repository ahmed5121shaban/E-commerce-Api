using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager categoryManager;

        public ILogger<CategoryController> Logger { get; }

        public CategoryController(ILogger<CategoryController> _logger,CategoryManager _categoryManager)
        {
            Logger = _logger;
            categoryManager = _categoryManager;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories() 
        {
            try 
            {
                var categories = categoryManager.GetAll();
                if (!categories.Any()) return NoContent();
                return Ok(categories);
            }
            catch (Exception ex) 
            {
                throw ex.InnerException;
            }
           
        }

    }
}
