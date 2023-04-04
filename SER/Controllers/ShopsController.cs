using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SER.DataAccess;
using SER.Models;

namespace SER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            if (_context.Shops == null)
            {
                return NotFound();
            }
            return await _context.Shops.ToListAsync();
        }
        // GET: api/Products
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShopsPaged(int PageIndex = 1, int PageSize = 3)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            //sắp xếp theo Location giảm dần
            if (PageIndex > 0 && PageSize > 0)
                return await _context.Shops.Skip(PageSize * (PageIndex - 1)).OrderByDescending(e => e.Location).ToListAsync();
            else
                return await _context.Shops.OrderByDescending(e => e.Location).ToListAsync();
        }
        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(Guid id)
        {
            if (_context.Shops == null)
            {
                return NotFound();
            }
            var shop = await _context.Shops.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        // PUT: api/Shops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(Guid id, Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(Shop shop)
        {
            if (_context.Shops == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shops'  is null.");
            }
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.Id }, shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(Guid id)
        {
            if (_context.Shops == null)
            {
                return NotFound();
            }
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(Guid id)
        {
            return (_context.Shops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
