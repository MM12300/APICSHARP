using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeItemsDtoController : ControllerBase
    {
        private readonly RecipeContext _context;

        public RecipeItemsDtoController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/RecipeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeItem>>> GetRecipeItems()
        {
            return await _context.RecipeItems.ToListAsync();
        }

        // GET: api/RecipeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeItem>> GetRecipeItem(long id)
        {
            var recipeItem = await _context.RecipeItems.FindAsync(id);

            if (recipeItem == null)
            {
                return NotFound();
            }

            return recipeItem;
        }

        // PUT: api/RecipeItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RecipeItem>> PostRecipeItem(RecipeItem recipeItem)
        {
            _context.RecipeItems.Add(recipeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeItem), new { id = recipeItem.Id }, recipeItem);
        }

        // DELETE: api/RecipeItems/5
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

        private static RecipeItemDto ItemToDto(RecipeItem recipeItem) => new RecipeItemDto
        {
            Id = recipeItem.Id,
            Name = recipeItem.Name,
            Description = recipeItem.Description,
            UrlPicture = recipeItem.UrlPicture,
            Ingredients = recipeItem.Ingredients,
            Severity = recipeItem.Severity,
            Time = recipeItem.Time,
            Score = recipeItem.Score,
            Budget = recipeItem.Budget,
            Recipe = recipeItem.Recipe
        };
    }
}
