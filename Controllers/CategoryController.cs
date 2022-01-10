using System;
using BlogNew.Mapping.Data;
using BlogNew.Models;
using BlogNew.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogNew.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {


            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {

                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna do servidor"));
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {


            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"ERRCAT-01 - Não foi possível incluir uma categoria.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");

            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug.ToLower();
            context.Categories.Update(category); // não possui Async
            await context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpDelete("v1/categories/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            context.Categories.Remove(category); // não possui Async 
            await context.SaveChangesAsync();
            return Ok(category);

        }

    }
}