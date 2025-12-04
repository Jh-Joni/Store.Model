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
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientosInventarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MovimientosInventario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientoInventario()
        {
            return await _context.MovimientoInventario.ToListAsync();
        }

        // GET: api/MovimientosInventario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimientoInventario(int id)
        {
            var movimientoInventario = await _context.MovimientoInventario.FindAsync(id);

            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return movimientoInventario;
        }

        // PUT: api/MovimientosInventario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientoInventario(int id, MovimientoInventario movimientoInventario)
        {
            if (id != movimientoInventario.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientoInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoInventarioExists(id))
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

        // POST: api/MovimientosInventario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> PostMovimientoInventario(MovimientoInventario movimientoInventario)
        {
            _context.MovimientoInventario.Add(movimientoInventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientoInventario", new { id = movimientoInventario.Id }, movimientoInventario);
        }

        // DELETE: api/MovimientosInventario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientoInventario(int id)
        {
            var movimientoInventario = await _context.MovimientoInventario.FindAsync(id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }

            _context.MovimientoInventario.Remove(movimientoInventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientoInventario.Any(e => e.Id == id);
        }
    }
}
