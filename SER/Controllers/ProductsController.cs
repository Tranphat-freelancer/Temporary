using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;
using SER.ViewModel.Product.Requests;

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
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                return Ok(new ResultApi(_seedService.Product.GetAll()));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Product
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPaged(int pageIndex = 1, int pageSize = 30)
        {

            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Product.GetPaged(pageIndex, pageSize);
                if (data == null)
                {
                    return NotFound(new ResultApi(data));
                }
                //sắp xếp theo Email tăng dần
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }
        // GET: api/Product/5
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {

            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                var Product = _seedService.Product.GetEntity(id);

                if (Product == null)
                {
                    return NotFound(new ResultApi(Product));
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
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Product.UpdateEntity(request);
                if (data == null)
                {
                    return NotFound(new ResultApi(data));
                }
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
        [Route("[action]")]
        public async Task<ActionResult<Product>> Create(ProductCreateRequest request)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Product.CreateEntity(request);
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Product/5
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            try
            {
                if (_seedService.Product == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Product.DeleteEntity(id);

                if (data == null)
                {
                    return NotFound(new ResultApi(data));
                }

                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));

            }

        }

    }
}
