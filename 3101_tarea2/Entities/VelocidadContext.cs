using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Velocidad.Entities;

public partial class VelocidadContext : DbContext
{
    public VelocidadContext()
    {
    }

    public VelocidadContext(DbContextOptions<VelocidadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Corredor> Corredors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrera__3214EC07E767E318");
        });

        modelBuilder.Entity<Corredor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__corredor__3214EC0782F00D38");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Corredors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__corredor__IdCarr__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
