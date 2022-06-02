using System.Collections.Generic;
using System.Dynamic;

namespace API.Models
{
    public class RecipeItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPicture { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Difficulty { get; set; }
        public int Duration { get; set; }
        public int Score { get; set; }
        public int Budget { get; set; }
        public List<Step> Recipe { get; set; }
        
    }
}