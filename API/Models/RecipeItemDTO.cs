using System.Dynamic;

namespace API.Models
{
    public class RecipeItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPicture { get; set; }
        public string[] Ingredients { get; set; }
        public int Severity { get; set; }
        public int Time { get; set; }
        public int Score { get; set; }
        public int Budget { get; set; }
        public string Recipe { get; set; }
    }
}