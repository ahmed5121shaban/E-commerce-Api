using Infrastructure;
using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModel;

namespace E_commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductManager ProductManeger;
        AppDbContext dbContext;
        CloudinaryService cloudinaryService;

        public ProductController(ProductManager product,
            AppDbContext _dbContext, CloudinaryService CloudinaryService)
        {
            this.ProductManeger = product;
            this.dbContext = _dbContext;
            this.cloudinaryService = CloudinaryService;

        }
        [HttpGet]
        public IActionResult GetAll(string columnOrder = "Id", int categoryID = 0,
            int price = 0, string productName = "",
            bool IsAscending = false, int PageSize = 4, int PageNumber = 1)
        {
            Pagination<Product> products = ProductManeger.GetAllWithFilter(columnOrderBy: columnOrder,
                IsAscending: IsAscending, productName: productName,
                categoryID: categoryID, price: price, PageSize: PageSize, PageNumber: PageNumber);
            if(products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailsByID(int id)
        {
            ProductViewModel pro = ProductManeger.GetByID(id).Single().MapToView();
            if (pro == null)
                return NotFound();

            return Ok(pro);
        }

        [HttpDelete("{id}")]
        [TraceProductDeleted]
        public IActionResult Delete(int id)
        {
            var product = ProductManeger.GetAll().FirstOrDefault(i => i.ID == id);
            if (product != null)
            {
                if (ProductManeger.Delete(product))
                {
                    return Ok();   
                }
               return BadRequest();
            }
               
            return BadRequest(string.Empty);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductViewModel pro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (pro.DeleteOrNot)
            {
                var attachments = ProductManeger.GetAll().Where(p => p.ID == pro.ID).FirstOrDefault();
                attachments.ProductAttachments.Clear();
                dbContext.SaveChanges();
            }

            foreach (IFormFile item in pro.Attachments)
            {
                var imageUrl = await cloudinaryService.UploadFileAsync(item);
                pro.ProductsImageList.Add(imageUrl);
            }
            ProductManeger.Update(pro);

            return Created();
        }


        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel pro)
        {
            if (!ModelState.IsValid && pro.Attachments != null)
            {
                return BadRequest(ModelState);
            }
            if (pro.Attachments != null)
            {
                foreach (IFormFile item in pro.Attachments)
                {
                    var imageUrl = await cloudinaryService.UploadFileAsync(item);
                    pro.ProductsImageList.Add(imageUrl);
                }
            }

            ProductManeger.Add(pro);
            return Created();


        }

    }
}
