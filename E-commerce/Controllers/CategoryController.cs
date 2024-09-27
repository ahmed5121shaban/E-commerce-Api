using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) {
            try 
            {
               var category = await categoryManager.Get(id);
                if(category == null) return NoContent();
                return Ok(category);
            } 
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel categoryView) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(new {Message="this data not completed"});
            }

            try
            {
                categoryManager.Add(categoryView);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await categoryManager.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryViewModel categoryView) {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "this data not completed" });
            }

            try
            {
                categoryManager.Update(categoryView);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
