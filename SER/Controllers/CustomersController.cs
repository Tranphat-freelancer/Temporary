using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;
using SER.ViewModel.Customer.Requests;

namespace SER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public CustomerController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        // GET: api/Customer
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                return Ok(new ResultApi(_seedService.Customer.GetAll()));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Customer
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetPaged(int pageIndex = 1, int pageSize = 30)
        {

            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Customer.GetPaged(pageIndex, pageSize);
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
        // GET: api/Customer/5
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetById(Guid id)
        {

            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                var customer = _seedService.Customer.GetEntity(id);

                if (customer == null)
                {
                    return NotFound(new ResultApi(customer));
                }

                return Ok(new ResultApi(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CustomerUpdateRequest request)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Customer.UpdateEntity(request);
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

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Customer>> Create(CustomerCreateRequest request)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Customer.CreateEntity(request);
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Customer/5
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound(new ResultApi());
                }
                var data = _seedService.Customer.DeleteEntity(id);

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
