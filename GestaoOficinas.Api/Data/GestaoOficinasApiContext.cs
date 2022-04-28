#nullable disable
using GestaoOficinas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoOficinas.Api.Data
{
    public class GestaoOficinasApiContext : DbContext
    {
        public GestaoOficinasApiContext (DbContextOptions<GestaoOficinasApiContext> options)
            : base(options)
        {
        }
        public DbSet<Oficina> Oficina { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<OficinaServico> OficinaServico { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
