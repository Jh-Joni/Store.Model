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
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProveedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Proveedor>>>> GetProveedor()
        {
            try
            {
                var proveedores = await _context.Proveedores.ToListAsync();
                return ApiResult<List<Proveedor>>.Ok(proveedores);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Proveedor>>.Fail(ex.Message);
            }
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Proveedor>>> GetProveedor(int id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return ApiResult<Proveedor>.Fail("Datos no encontrados");
                }
                return ApiResult<Proveedor>.Ok(proveedor);
            }
            catch (Exception ex)
            {
                return ApiResult<Proveedor>.Fail(ex.Message);
            }
        }

        // PUT: api/Proveedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Proveedor>>> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return ApiResult<Proveedor>.Fail("No coinciden los identificadores");
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProveedorExists(id))
                {
                    return ApiResult<Proveedor>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Proveedor>.Fail(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Proveedor>.Fail(ex.Message);
            }

            return ApiResult<Proveedor>.Ok(null);
        }

        // POST: api/Proveedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Proveedor>>> PostProveedor(Proveedor proveedor)
        {
            try
            {
                _context.Proveedores.Add(proveedor);
                await _context.SaveChangesAsync();
                return ApiResult<Proveedor>.Ok(proveedor);
            }
            catch (Exception ex)
            {
                return ApiResult<Proveedor>.Fail(ex.Message);
            }
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Proveedor>>> DeleteProveedor(int id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return ApiResult<Proveedor>.Fail("Datos no encontrados");
                }

                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();

                return ApiResult<Proveedor>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Proveedor>.Fail(ex.Message);
            }
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id == id);
        }
    }
}
