using System.Dynamic;

namespace API.Models
{
    public class RecipeItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        private string SecretId { get; set; }
    }
}