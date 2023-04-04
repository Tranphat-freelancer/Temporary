using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;

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
        public async Task<ActionResult<IEnumerable<Shop>>> GetShop()
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound();
                }
                return Ok(_seedService.Shop.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Shop
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShopPaged(int pageIndex = 1, int pageSize = 30)
        {

            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound();
                }
                //sắp xếp theo Email tăng dần
                return Ok(new ResultApi(_seedService.Shop.GetPaged(pageIndex, pageSize)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }
        // GET: api/Shop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(Guid id)
        {

            try
            {
                if (_seedService.Shop == null)
                {
                    return NotFound();
                }
                var Shop = _seedService.Shop.GetEntity(id);

                if (Shop == null)
                {
                    return NotFound();
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
        [HttpPost][Route("[action]")]
        public async Task<IActionResult> PutShop(Shop Shop)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Shop'  is null.");
                }
                var data = _seedService.Shop.UpdateEntity(Shop);
                if (data == null)
                    return NoContent();
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
        public async Task<ActionResult<Shop>> PostShop(Shop Shop)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Shop'  is null.");
                }
                var data = _seedService.Shop.CreateEntity(Shop);
                if (data == null)
                    return NoContent();
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Shop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(Guid id)
        {
            try
            {
                if (_seedService.Shop == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Shop'  is null.");
                }
                var data = _seedService.Shop.DeleteEntity(id);
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
