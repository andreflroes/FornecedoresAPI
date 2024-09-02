using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class FornecedoresContext : DbContext
    {
        public FornecedoresContext(DbContextOptions<FornecedoresContext> options) : base(options) { }

        public DbSet<Fornecedor> Fornecedores { get; set; }
    }

}
