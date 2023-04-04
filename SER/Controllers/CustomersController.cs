using Microsoft.AspNetCore.Mvc;
using SER.Domain;
using SER.Domain.Entities;
using SER.ViewModel;

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
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound();
                }
                return Ok(_seedService.Customer.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }
        // GET: api/Customer
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerPaged(int pageIndex = 1, int pageSize = 30)
        {

            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound();
                }
                //sắp xếp theo Email tăng dần
                return Ok(new ResultApi(_seedService.Customer.GetPaged(pageIndex, pageSize)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }
        }
        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {

            try
            {
                if (_seedService.Customer == null)
                {
                    return NotFound();
                }
                var customer = _seedService.Customer.GetEntity(id);

                if (customer == null)
                {
                    return NotFound();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Customer customer)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Customer'  is null.");
                }
                var data = _seedService.Customer.UpdateEntity(customer);
                if (data == null)
                    return NoContent();
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
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Customer'  is null.");
                }
                var data = _seedService.Customer.CreateEntity(customer);
                if (data == null)
                    return NoContent();
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));
            }

        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                if (_seedService.Customer == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Customer'  is null.");
                }
                var data = _seedService.Customer.GetEntity(id);
                if (data == null)
                    return NoContent();
                _seedService.Customer.DeleteEntity(data);
                return Ok(new ResultApi(data));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi(ex.Message));

            }

        }

    }
}
