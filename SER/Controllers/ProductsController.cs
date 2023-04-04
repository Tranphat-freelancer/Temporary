using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;

namespace SER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public ProductController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound();
                }
                return Ok(_seedService.Product.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Product
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductPaged(int pageIndex = 1, int pageSize = 30)
        {

            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound();
                }
                //sắp xếp theo Email tăng dần
                return Ok(new ResultApi(_seedService.Product.GetPaged(pageIndex, pageSize)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }
        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {

            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound();
                }
                var Product = _seedService.Product.GetEntity(id);

                if (Product == null)
                {
                    return NotFound();
                }

                return Ok(new ResultApi(Product));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost][Route("[action]")]
        public async Task<IActionResult> PutProduct(Product Product)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
                }
                var data = _seedService.Product.UpdateEntity(Product);
                if (data == null)
                    return NoContent();
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));

            }

        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product Product)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
                }
                var data = _seedService.Product.CreateEntity(Product);
                if (data == null)
                    return NoContent();
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
                }
                var data = _seedService.Product.DeleteEntity(id);
                if (data == null)
                    return NoContent();

                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));

            }

        }

    }
}
