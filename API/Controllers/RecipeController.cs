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
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeContext _context;

        public RecipeController(RecipeContext context)
        {
            _context = context;
            var recipe1 = new RecipeItem();
            recipe1.Name = "Easy lemon layer cake";
            recipe1.Description = "Indulge in a slice of lemon cake for afternoon tea. With lovely light sponge layers, a citrus zing offsets the rich and creamy soft cheese icing";
            recipe1.UrlPicture = "https://images.unsplash.com/photo-1598795164852-d2b5472d8bbb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80";
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(new Ingredient() { Name = "unsalted butter", Quantity = 225 });
            ingredients.Add(new Ingredient() { Name = "caster sugar", Quantity = 225 });
            ingredients.Add(new Ingredient() { Name = "eggs", Quantity = 4 });
            ingredients.Add(new Ingredient() { Name = "self-raising flour", Quantity = 225 });
            ingredients.Add(new Ingredient() { Name = "natural yogurt", Quantity = 75 });
            ingredients.Add(new Ingredient() { Name = "lemons, zested", Quantity = 3 });
            recipe1.Ingredients = ingredients;
            recipe1.Difficulty = 2;
            recipe1.Duration = 185;
            recipe1.Score = 5;
            recipe1.Budget = 50;
            recipe1.Recipe = "STEP 1: Heat the oven to 180C / 160C fan/ gas 4 and line the base of two 20cm sandwich tins with baking parchment. Beat the butter and sugar together for 3 mins using an electric whisk until smooth and fluffy.Add the eggs, one at a time, beating well between each addition and scraping down the sides of the bowl. Fold in the flour and baking powder until well incorporated, then fold in the yogurt, vanilla and lemon zest.Divide between the tins and bake for 30 - 35 mins until golden and a skewer inserted into the middles comes out clean. STEP 2: Meanwhile, make the drizzle.Tip the sugar, lemon juice and 100ml water into a small pan set over a medium heat and stir until dissolved.Add the lemon zest, bring to the boil and simmer for 2 - 3 mins until the zest has softened and the liquid is syrupy.Remove the zest to a sheet of baking parchment using a slotted spoon, and remove the syrup from the heat. STEP 3: Leave the sponges to cool for 10 mins in the tins, then pour over the warm drizzle.Leave to cool completely. STEP 4: For the icing, beat the butter and icing sugar together using an electric whisk for 4 - 5 mins until smooth, scraping down the sides of the bowl as you go.Add the vanilla and soft cheese and beat for 4 mins more until thick and creamy.Don’t worry if it doesn’t look thick at first – it will loosen, then thicken again as you beat it. STEP 5: Remove the cooled sponges from the tins.Spoon the icing into a piping bag fitted with a star nozzle.Put one sponge on a cake stand or serving plate, and pipe just under half the icing around the edge using a circular motion for a wavy effect.Pipe a little more icing over the empty middle(this doesn’t need to be neat) and smooth with the back of a spoon.Chill for 45 mins - 1 hr until set.Top with the second sponge, then pipe eight blobs of icing around the edge at regular intervals, leaving a gap between each.Spoon the candied lemon zest into each gap, then serve.";
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
