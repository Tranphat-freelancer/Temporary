using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;
using SER.ViewModel.Shop.Requests;

namespace SER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public ShopController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        // GET: api/Shop
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetAll()
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                return Ok(new ResultApi(_seedService.Shop.GetAll()));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Shop
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetPaged(int pageIndex = 1, int pageSize = 3)
        {

            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Shop.GetPaged(pageIndex, pageSize);
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
        // GET: api/Shop/5
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Shop>> GetById(Guid id)
        {

            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                var Shop = _seedService.Shop.GetEntity(id);

                if (Shop == null)
                {
                    return NotFound(new ResultApi(Shop));
                }

                return Ok(new ResultApi(Shop));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }

        // PUT: api/Shop/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(ShopUpdateRequest request)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Shop.UpdateEntity(request);
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

        // POST: api/Shop
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Shop>> Create(ShopCreateRequest request)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Shop.CreateEntity(request);
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Shop/5
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Shop.DeleteEntity(id);

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
