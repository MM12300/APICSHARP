using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace API.Models
{
    public class RecipeItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPicture { get; set; }
        public string Ingredients { get; set; }

        [Range(1, 3)]
        public int Difficulty { get; set; }
        public int Duration { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }
        public int Budget { get; set; }
        public string Recipe { get; set; }
        
    }
}