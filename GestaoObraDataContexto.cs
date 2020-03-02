using Microsoft.EntityFrameworkCore;
using GestaoObras.Models;

namespace GestaoObras
{
    public class GestaoObraDataContexto : DbContext
    {
        public GestaoObraDataContexto (DbContextOptions<GestaoObraDataContexto> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Engenheiro> Engenheiro { get; set; }
        public DbSet<Obra> Obra { get; set; }
    }
}