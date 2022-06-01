using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }
        
        public DbSet<RecipeItem> RecipeItems { get; set; }
    }
}