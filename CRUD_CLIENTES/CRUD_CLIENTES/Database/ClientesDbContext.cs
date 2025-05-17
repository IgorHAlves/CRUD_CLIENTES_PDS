using CRUD_CLIENTES.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD_CLIENTES.Database
{
    public partial class ClientesDbContext : DbContext
    {
        public DbSet<Cliente> Clientes{ get; set; }

        public ClientesDbContext()
        {
        }

        public ClientesDbContext(DbContextOptions<ClientesDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseMySql("server=localhost;user id=root;password=1234;database=db_clientes", ServerVersion.Parse("8.1.0-mysql"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
