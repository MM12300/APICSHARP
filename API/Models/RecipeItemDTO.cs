using System.Dynamic;

namespace API.Models
{
    public class RecipeItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}