using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Definición de las tablas (DbSets)
    public DbSet<User> Users { get; set; }
    public DbSet<Commerce> Commerces { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }
    public DbSet<State> States { get; set; }

    // Configuración del modelo (opcionalmente puedes usar Fluent API para personalizar más la configuración)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tabla de Users
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId); // Clave primaria UserId

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);  // Definir un tamaño máximo para el Username

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(100);  // Definir un tamaño máximo para la contraseña

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50); // Definir un tamaño máximo para el Role

        modelBuilder.Entity<User>()
            .HasOne(u => u.Commerce)          // Un User tiene un Commerce
            .WithMany(c => c.Users)           // Un Commerce tiene muchos Users
            .HasForeignKey(u => u.CommerceId) // La clave foránea es CommerceId en User
            .OnDelete(DeleteBehavior.Cascade); // Cuando se elimina un Comercio, se eliminan sus Usuarios

        modelBuilder.Entity<User>()
            .HasOne(u => u.State)            // Un User tiene un State
            .WithMany(s => s.Users)           // Un State tiene muchos Users
            .HasForeignKey(u => u.StateId)    // La clave foránea es StateId en User
            .OnDelete(DeleteBehavior.NoAction); // Cambiar de Cascade a NoAction para evitar el ciclo

        // Tabla de Commerces
        modelBuilder.Entity<Commerce>()
            .HasKey(c => c.CommerceId); // Clave primaria CommerceId

        modelBuilder.Entity<Commerce>()
            .Property(c => c.CommerceName)
            .IsRequired()
            .HasMaxLength(200); // Definir un tamaño máximo para el nombre del comercio

        modelBuilder.Entity<Commerce>()
            .Property(c => c.Address)
            .HasMaxLength(500); // Definir un tamaño máximo para la dirección

        modelBuilder.Entity<Commerce>()
            .Property(c => c.RUC)
            .HasMaxLength(20);  // Definir tamaño máximo para el RUC (opcional)

        modelBuilder.Entity<Commerce>()
            .HasOne(c => c.State)
            .WithMany(s => s.Commerces)
            .HasForeignKey(c => c.StateId)
            .OnDelete(DeleteBehavior.NoAction); // Evitar ciclo con NoAction o Restrict si es necesario

        // Tabla de Sales
        modelBuilder.Entity<Sale>()
            .HasKey(s => s.SaleId); // Clave primaria SaleId

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.User) // Relación con User
            .WithMany(u => u.Sales) // Un User tiene muchas Sales
            .HasForeignKey(s => s.UserId) // Sale tiene una clave foránea UserId
            .OnDelete(DeleteBehavior.Restrict);  // Cambiar Cascade a Restrict para evitar ciclo de eliminación en cascada

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Commerce)           // Una venta tiene un comercio
            .WithMany(c => c.Sales)            // Un comercio tiene muchas ventas
            .HasForeignKey(s => s.CommerceId)  // La clave foránea es CommerceId en Sale
            .OnDelete(DeleteBehavior.Restrict); // Restrict para evitar el ciclo de eliminación en cascada

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.State)
            .WithMany(st => st.Sales)
            .HasForeignKey(s => s.StateId)
            .OnDelete(DeleteBehavior.Restrict); // Esto evita el ciclo de eliminación en cascada

        // Tabla de SaleDetails
        modelBuilder.Entity<SaleDetail>()
            .HasKey(sd => sd.DetailId); // Clave primaria DetailId

        modelBuilder.Entity<SaleDetail>()
            .HasOne(sd => sd.Sale)
            .WithMany(s => s.SaleDetails)
            .HasForeignKey(sd => sd.SaleId)
            .OnDelete(DeleteBehavior.Cascade); // Cuando se elimina una venta, se eliminan sus detalles

        modelBuilder.Entity<SaleDetail>()
            .Property(sd => sd.Price)
            .HasColumnType("decimal(18,2)"); // Especificamos tipo decimal con precisión 18 y escala 2

        // Tabla de States
        modelBuilder.Entity<State>()
            .HasKey(s => s.StateId); // Clave primaria StateId

        modelBuilder.Entity<State>()
            .Property(s => s.StateName)
            .IsRequired()  // El nombre del estado es requerido
            .HasMaxLength(50);  // Definir un tamaño máximo para el nombre del estado
    }
}
