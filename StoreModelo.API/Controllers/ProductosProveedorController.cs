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
        public async Task<ActionResult<ApiResult<List<ProductoProveedor>>>> GetProductosProveedor()
        {
            try
            {
                var productosProveedor = await _context.ProductosProveedor.ToListAsync();
                return ApiResult<List<ProductoProveedor>>.Ok(productosProveedor);
            }
            catch (Exception ex)
            {
                return ApiResult<List<ProductoProveedor>>.Fail(ex.Message);
            }
        }

        // GET: api/ProductosProveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<ProductoProveedor>>> GetProductoProveedor(int id)
        {
            try
            {
                var productoProveedor = await _context.ProductosProveedor.FindAsync(id);
                if (productoProveedor == null)
                {
                    return ApiResult<ProductoProveedor>.Fail("Datos no encontrados");
                }
                return ApiResult<ProductoProveedor>.Ok(productoProveedor);
            }
            catch (Exception ex)
            {
                return ApiResult<ProductoProveedor>.Fail(ex.Message);
            }
        }

        // PUT: api/ProductosProveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<ProductoProveedor>>> PutProductoProveedor(int id, ProductoProveedor productoProveedor)
        {
            if (id != productoProveedor.Id)
            {
                return ApiResult<ProductoProveedor>.Fail("No coinciden los identificadores");
            }

            _context.Entry(productoProveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductoProveedorExists(id))
                {
                    return ApiResult<ProductoProveedor>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<ProductoProveedor>.Fail(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<ProductoProveedor>.Fail(ex.Message);
            }

            return ApiResult<ProductoProveedor>.Ok(null);
        }

        // POST: api/ProductosProveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<ProductoProveedor>>> PostProductoProveedor(ProductoProveedor productoProveedor)
        {
            try
            {
                _context.ProductosProveedor.Add(productoProveedor);
                await _context.SaveChangesAsync();
                return ApiResult<ProductoProveedor>.Ok(productoProveedor);
            }
            catch (Exception ex)
            {
                return ApiResult<ProductoProveedor>.Fail(ex.Message);
            }
        }

        // DELETE: api/ProductosProveedor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<ProductoProveedor>>> DeleteProductoProveedor(int id)
        {
            try
            {
                var productoProveedor = await _context.ProductosProveedor.FindAsync(id);
                if (productoProveedor == null)
                {
                    return ApiResult<ProductoProveedor>.Fail("Datos no encontrados");
                }

                _context.ProductosProveedor.Remove(productoProveedor);
                await _context.SaveChangesAsync();

                return ApiResult<ProductoProveedor>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<ProductoProveedor>.Fail(ex.Message);
            }
        }

        private bool ProductoProveedorExists(int id)
        {
            return _context.ProductosProveedor.Any(e => e.Id == id);
        }
    }
}
