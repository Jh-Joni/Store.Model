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

        public DbSet<Store.Model.Categoria> Categoria { get; set; } = default!;

public DbSet<Store.Model.MovimientoInventario> MovimientoInventario { get; set; } = default!;

public DbSet<Store.Model.Producto> Producto { get; set; } = default!;

public DbSet<Store.Model.ProductoProveedor> ProductoProveedor { get; set; } = default!;

public DbSet<Store.Model.Proveedor> Proveedor { get; set; } = default!;
    }
