using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Model;

namespace StoreModelo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosProveedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosProveedorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductosProveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoProveedor>>> GetProductoProveedor()
        {
            return await _context.ProductoProveedor.ToListAsync();
        }

        // GET: api/ProductosProveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoProveedor>> GetProductoProveedor(int id)
        {
            var productoProveedor = await _context.ProductoProveedor.FindAsync(id);

            if (productoProveedor == null)
            {
                return NotFound();
            }

            return productoProveedor;
        }

        // PUT: api/ProductosProveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoProveedor(int id, ProductoProveedor productoProveedor)
        {
            if (id != productoProveedor.Id)
            {
                return BadRequest();
            }

            _context.Entry(productoProveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoProveedorExists(id))
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

        // POST: api/ProductosProveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoProveedor>> PostProductoProveedor(ProductoProveedor productoProveedor)
        {
            _context.ProductoProveedor.Add(productoProveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoProveedor", new { id = productoProveedor.Id }, productoProveedor);
        }

        // DELETE: api/ProductosProveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoProveedor(int id)
        {
            var productoProveedor = await _context.ProductoProveedor.FindAsync(id);
            if (productoProveedor == null)
            {
                return NotFound();
            }

            _context.ProductoProveedor.Remove(productoProveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoProveedorExists(int id)
        {
            return _context.ProductoProveedor.Any(e => e.Id == id);
        }
    }
}
