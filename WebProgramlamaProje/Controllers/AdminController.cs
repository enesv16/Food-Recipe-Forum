using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin



        // KATEGORİ LİSTE
        public async Task<IActionResult> Categories()
        {
            return View(await _context.Categories.ToListAsync());
        }



        // TARİF DETAY 
        public async Task<IActionResult> RecipeDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // KATEGORİ DETAY
        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }





        // TARİF OLUŞTUR
        public IActionResult CreateRecipe()
        {
            return View();
        }
        public IActionResult CreateCategory()
        {
            return View();
        }



        // TARİF OLUŞTUR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecipe([Bind("Id,Title,FoodRecipe,ImageUrl,IsConfirmed,PublishTime")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Recipes));
            }
            return View(recipe);
        }

        // KATEGORİ OLUŞTUR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }


        // TARİF DÜZENLE
        public async Task<IActionResult> EditRecipe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }
        // KATEGORİ DÜZENLE
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // TARİF DÜZENLEME {POST}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecipe(int id, [Bind("Id,Title,FoodRecipe,ImageUrl,IsConfirmed,PublishTime")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Recipes));
            }
            return View(recipe);
        }
        // KATEGORİ DÜZENLEME {POST}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }
        // GET: Admin/Delete/5

        // TARİF SİLME
        public async Task<IActionResult> DeleteRecipe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        // KATEGORİ SİLME
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // TARİF SİLME {POST}
        [HttpPost, ActionName("DeleteRecipe")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecipeConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Recipes));
        }

        // KATEGORİ SİLME {POST}
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Categories));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }



        //-----------------------------------------------------
        public async Task<IActionResult> Users()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Recipes()
        {
            var recipes = await _context.Recipes.Include(s => s.AppUser).Include(d => d.Comments).Include(f => f.Category).ToListAsync();
            return View(recipes);
        }

    }
}