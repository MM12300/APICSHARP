using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class IngredientContext : DbContext
    {
        public IngredientContext(DbContextOptions<IngredientContext> ingredientOptions) : base(ingredientOptions)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
    }
}