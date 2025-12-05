using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Model;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Store.Model.Categoria> Categorias { get; set; } = default!;

public DbSet<Store.Model.MovimientoInventario> MovimientosInventario { get; set; } = default!;

public DbSet<Store.Model.Producto> Productos { get; set; } = default!;

public DbSet<Store.Model.ProductoProveedor> ProductosProveedor { get; set; } = default!;

public DbSet<Store.Model.Proveedor> Proveedores { get; set; } = default!;
    }
