using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Web.Http.Cors; 

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeContext _context;

        public RecipeController(RecipeContext context)
        {
            _context = context;
            var recipe1 = new RecipeItem();
            recipe1.Name = "First recipe";
            context.Add(recipe1);
            context.SaveChanges();
        }
        
        // GET: api/RecipeItems
        /// <summary>
        /// Get the list of recipes in the cookbook
        /// </summary>
        /// <returns>Return a list of recipes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeItem>>> GetRecipeItems()
        {
            return await _context.RecipeItems.Include(i=>i.Ingredients).ToListAsync();
        }

        // GET: api/RecipeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeItem>> GetRecipeItem(long id)
        {
            var recipeItem = await _context.RecipeItems.Where(i=>i.Id == id).Include(i => i.Ingredients).FirstAsync();

            if (recipeItem == null)
            {
                return NotFound();
            }

            return recipeItem;
        }

        // PUT: api/RecipeItems/5
        /// <summary>
        /// Modify a recipe in the cookbook
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipeItem"></param>
        /// <returns>Returns a 400 HTTP code in case of bad request, a 404 HTTP code if the recipe is not found, or a 200 HTTP code if the request is a sucess</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeItem(long id, RecipeItem recipeItem)
        {
            if (id != recipeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecipeItems
        /// <summary>
        /// Add a recipe to the cookbook
        /// </summary>
        /// <param name="recipeItem"></param>
        /// <returns>Returns a 201 HTTP code if the creation is a success</returns>
        [HttpPost]
        public async Task<ActionResult<RecipeItem>> PostRecipeItem(RecipeItem recipeItem)
        {
            _context.RecipeItems.Add(recipeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeItem), new { id = recipeItem.Id }, recipeItem);
        }

        // DELETE: api/RecipeItems/5
        /// <summary>
        /// Delete a recipe in the cookbook
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the deleted recipe</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeItem>> DeleteRecipeItem(long id)
        {
            var recipeItem = await _context.RecipeItems.FindAsync(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            _context.RecipeItems.Remove(recipeItem);
            await _context.SaveChangesAsync();

            return recipeItem;
        }

        private bool RecipeItemExists(long id)
        {
            return _context.RecipeItems.Any(e => e.Id == id);
        }

        private static RecipeItem ItemTo(RecipeItem recipeItem) => new RecipeItem
        {
            Id = recipeItem.Id,
            Name = recipeItem.Name,
            Description = recipeItem.Description,
            UrlPicture = recipeItem.UrlPicture,
            Ingredients = recipeItem.Ingredients.ToList(),
            Difficulty = recipeItem.Difficulty,
            Duration = recipeItem.Duration,
            Score = recipeItem.Score,
            Budget = recipeItem.Budget,
            Recipe = recipeItem.Recipe
        };
    }
}
