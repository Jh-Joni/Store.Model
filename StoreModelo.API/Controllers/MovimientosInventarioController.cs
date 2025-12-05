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
        public async Task<ActionResult<ApiResult<List<MovimientoInventario>>>> GetMovimientoInventario()
        {
            try
            {
                var movimientos = await _context.MovimientosInventario.ToListAsync();
                return ApiResult<List<MovimientoInventario>>.Ok(movimientos);
            }
            catch (Exception ex)
            {
                return ApiResult<List<MovimientoInventario>>.Fail(ex.Message);
            }
        }

        // GET: api/MovimientosInventario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<MovimientoInventario>>> GetMovimientoInventario(int id)
        {
            try
            {
                var movimientoInventario = await _context.MovimientosInventario.FindAsync(id);
                if (movimientoInventario == null)
                {
                    return ApiResult<MovimientoInventario>.Fail("Datos no encontrados");
                }
                return ApiResult<MovimientoInventario>.Ok(movimientoInventario);
            }
            catch (Exception ex)
            {
                return ApiResult<MovimientoInventario>.Fail(ex.Message);
            }
        }

        // PUT: api/MovimientosInventario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<MovimientoInventario>>> PutMovimientoInventario(int id, MovimientoInventario movimientoInventario)
        {
            if (id != movimientoInventario.Id)
            {
                return ApiResult<MovimientoInventario>.Fail("No coinciden los identificadores");
            }

            _context.Entry(movimientoInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MovimientoInventarioExists(id))
                {
                    return ApiResult<MovimientoInventario>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<MovimientoInventario>.Fail(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<MovimientoInventario>.Fail(ex.Message);
            }

            return ApiResult<MovimientoInventario>.Ok(null);
        }

        // POST: api/MovimientosInventario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<MovimientoInventario>>> PostMovimientoInventario(MovimientoInventario movimientoInventario)
        {
            try
            {
                _context.MovimientosInventario.Add(movimientoInventario);
                await _context.SaveChangesAsync();
                return ApiResult<MovimientoInventario>.Ok(movimientoInventario);
            }
            catch (Exception ex)
            {
                return ApiResult<MovimientoInventario>.Fail(ex.Message);
            }
        }

        // DELETE: api/MovimientosInventario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<MovimientoInventario>>> DeleteMovimientoInventario(int id)
        {
            try
            {
                var movimientoInventario = await _context.MovimientosInventario.FindAsync(id);
                if (movimientoInventario == null)
                {
                    return ApiResult<MovimientoInventario>.Fail("Datos no encontrados");
                }

                _context.MovimientosInventario.Remove(movimientoInventario);
                await _context.SaveChangesAsync();

                return ApiResult<MovimientoInventario>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<MovimientoInventario>.Fail(ex.Message);
            }
        }

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientosInventario.Any(e => e.Id == id);
        }
    }
}
