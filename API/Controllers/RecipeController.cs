using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

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
        }

        
        // GET: api/RecipeItems
        /// <summary>
        /// Get the list of recipes in the cookbook
        /// </summary>
        /// <returns>Return a list of recipes</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeItem>>> GetRecipeItems()
        {
            var recipeItem = await _context.RecipeItems.FindAsync(1L);
            if (recipeItem is null)
            {
                // RECIPE 1
                var recipe1 = new RecipeItem();
                recipe1.Name = "Easy lemon layer cake";
                recipe1.Description = "Indulge in a slice of lemon cake for afternoon tea. With lovely light sponge layers, a citrus zing offsets the rich and creamy soft cheese icing";
                recipe1.UrlPicture = "https://images.unsplash.com/photo-1598795164852-d2b5472d8bbb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80";
                recipe1.Ingredients = "unsalted butter : 225g, caster sugar : 225g, eggs : 4, self-raising flour : 225g, natural yogurt : 75g, lemons, zested : 3";
                recipe1.Difficulty = 2;
                recipe1.Duration = 185;
                recipe1.Score = 5;
                recipe1.Budget = 50;
                recipe1.Recipe = "STEP 1 : Heat the oven to 180C / 160C fan/ gas 4 and line the base of two 20cm sandwich tins with baking parchment. Beat the butter and sugar together for 3 mins using an electric whisk until smooth and fluffy.Add the eggs, one at a time, beating well between each addition and scraping down the sides of the bowl. Fold in the flour and baking powder until well incorporated, then fold in the yogurt, vanilla and lemon zest.Divide between the tins and bake for 30 - 35 mins until golden and a skewer inserted into the middles comes out clean.\n" +
                    "STEP 2 : Meanwhile, make the drizzle.Tip the sugar, lemon juice and 100ml water into a small pan set over a medium heat and stir until dissolved.Add the lemon zest, bring to the boil and simmer for 2 - 3 mins until the zest has softened and the liquid is syrupy.Remove the zest to a sheet of baking parchment using a slotted spoon, and remove the syrup from the heat.\n" +
                    "STEP 3 : Leave the sponges to cool for 10 mins in the tins, then pour over the warm drizzle.Leave to cool completely.";


                // RECIPE 2
                var recipe2 = new RecipeItem();
                recipe2.Name = "Vegan birthday cake";
                recipe2.Description = "This decadent plant-based chocolate cake is guaranteed to bring joy to any birthday party. Everyone will love the dairy-free buttercream & colourful sprinkles.";
                recipe2.UrlPicture = "https://images.unsplash.com/photo-1621303837174-89787a7d4729?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=436&q=80";
                recipe2.Ingredients = "sunflower oil : 320g, soy , almond or coconut milk : 450g, vanilla extract or vanilla bean paste : 2, self-raising flour : 450g, dairy-free dark chocolate : 200g, colourful sprinkles (make sure they are suitable for vegans) : many";
                recipe2.Difficulty = 3;
                recipe2.Duration = 90;
                recipe2.Score = 4;
                recipe2.Budget = 30;
                recipe2.Recipe = "STEP 1 : Heat the oven to 180C / 160C fan/ gas 4.Oil three 20cm round cake tins and line the bases and sides with baking parchment(if you don’t have three tins, cook the batter in batches). Whisk the milk and vinegar together in a jug – the milk should curdle slightly.Set aside.\n" +
                    "STEP 2 : Whisk the sugar, oil and vanilla extract together in a bowl, then whisk in the yogurt, making sure to break down any sugar lumps. Pour in the soured milk and mix well.\n" +
                    "STEP 3 : Sift the flour, cocoa powder, baking powder, bicarbonate of soda and ½ tsp salt into a separate bowl and stir well to combine.Gradually whisk the wet ingredients into the dry until you have a smooth batter, but be careful not to over - mix.";

                // RECIPE 3
                var recipe3 = new RecipeItem();
                recipe3.Name = "Coffee cupcakes";
                recipe3.Description = "These individual cupcakes with coffee frosting are easy to make and look great on a cake stand";
                recipe3.UrlPicture = "https://images.unsplash.com/photo-1603532648955-039310d9ed75?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80";
                recipe3.Ingredients = "golden caster sugar : 140g, butter , softened : 140g, eggs : 3, self-raising flour : 140g, instant espresso mixed with 1 tbsp water : 2";
                recipe3.Difficulty = 1;
                recipe3.Duration = 40;
                recipe3.Score = 2;
                recipe3.Budget = 15;
                recipe3.Recipe = "STEP 1 : Heat oven to 170C / 150C fan/ gas 3.Line 18 holes in 2 bun tins with fairy cake cases. Beat the sugar with the butter until light and creamy. Beat in the eggs, one by one, adding 1 tbsp flour at the same time. Beat in the rest of the flour along with the espresso.Spoon into the cake cases and bake for 17 mins, swapping the trays after 12 mins.Cool on a wire rack.\n" +
                    "STEP 2 : To make the icing, beat the butter until pale, then gradually beat in the icing sugar, followed by the espresso and the melted chocolate. When the cakes are completely cool, swirl the icing generously on top and decorate with coffee beans, if you like.";

                _context.Add(recipe1);
                _context.Add(recipe2);
                _context.Add(recipe3);

                _context.SaveChanges();
            }
            return await _context.RecipeItems.ToListAsync();
        }

        // GET: api/RecipeItems/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeItem>> GetRecipeItem(long id)
        {
            var recipeItem = await _context.RecipeItems.Where(i=>i.Id == id).FirstAsync();

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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
            Ingredients = recipeItem.Ingredients,
            Difficulty = recipeItem.Difficulty,
            Duration = recipeItem.Duration,
            Score = recipeItem.Score,
            Budget = recipeItem.Budget,
            Recipe = recipeItem.Recipe
        };
    }
}
