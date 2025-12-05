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
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Categoria>>>> GetCategoria()
        {
            try
            {
                var categorias = await _context.Categorias.ToListAsync();
                return ApiResult<List<Categoria>>.Ok(categorias);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Categoria>>.Fail(ex.Message);
            }
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Categoria>>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return ApiResult<Categoria>.Fail("Datos no encontrados");
                }
                return ApiResult<Categoria>.Ok(categoria);
            }
            catch (Exception ex)
            {
                return ApiResult<Categoria>.Fail(ex.Message);
            }
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Categoria>>> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return ApiResult<Categoria>.Fail("No coinciden los identificadores");
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoriaExists(id))
                {
                    return ApiResult<Categoria>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Categoria>.Fail(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Categoria>.Fail(ex.Message);
            }

            return ApiResult<Categoria>.Ok(null);
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Categoria>>> PostCategoria(Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return ApiResult<Categoria>.Ok(categoria);
            }
            catch (Exception ex)
            {
                return ApiResult<Categoria>.Fail(ex.Message);
            }
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Categoria>>> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return ApiResult<Categoria>.Fail("Datos no encontrados");
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return ApiResult<Categoria>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Categoria>.Fail(ex.Message);
            }
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
