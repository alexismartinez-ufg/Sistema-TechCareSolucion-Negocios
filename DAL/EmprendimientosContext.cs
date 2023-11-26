using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class EmprendimientosContext : DbContext
{
    public EmprendimientosContext()
    {
    }

    public EmprendimientosContext(DbContextOptions<EmprendimientosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Servicio> Servicios { get; set; }
    public virtual DbSet<DetalleServicio> DetalleServicios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<ComentarioServicio> ComentarioServicio { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Servicio>()
        .HasOne(s => s.Cliente)
        .WithMany()
        .HasForeignKey(s => s.IdCliente)
        .OnDelete(DeleteBehavior.Restrict);  // Puedes cambiar esto a .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Servicio>()
            .HasOne(s => s.Tecnico)
            .WithMany()
            .HasForeignKey(s => s.IdTecnico)
            .OnDelete(DeleteBehavior.Restrict);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
