using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
       

        // GET: Admin



        // KATEGORİ LİSTE
        
       



        // TARİF DETAY 
        public async Task<IActionResult> RecipeDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Recipes");
            }

            var recipe = await _context.Recipes.Include(a => a.AppUser).Include(s => s.Comments).Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return RedirectToAction("Recipes");
            }

            return View(recipe);
        }


   

      
        
        // GET: Admin/Delete/5

        // TARİF SİLME
        public async Task<IActionResult> DeleteRecipe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Recipes");
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return RedirectToAction("Recipes");
            }

            return View(recipe);
        }









        public async Task<IActionResult> Categories()
        {

            return View(await _context.Categories.ToListAsync());
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

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
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Categories");
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return RedirectToAction("Categories");
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
        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id)
        {
            var recipe = await _context.Recipes.Include(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
            var data = new ConfirmRecipeModel()
            {
                Title = recipe.Title,
                FoodRecipe = recipe.FoodRecipe,
                IsApproved = recipe.IsConfirmed,
                Id = recipe.Id
               
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditRecipe(ConfirmRecipeModel model)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(s => s.Id == model.Id);
            recipe.IsConfirmed = model.IsApproved;
            _context.SaveChanges();
            return RedirectToAction("Recipes");
        }
        public async Task<IActionResult> Comments()
        {
            var comments = await _context.Comments.Include(s => s.AppUser).Include(d => d.Recipe).ToListAsync();
            return View(comments);
        }
        [HttpGet]
        public async Task<IActionResult> EditComment(int id)
        {
            var comment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
            var data = new ConfirmComment()
            {
                Text = comment.Text,
                IsApproved = comment.IsConfirmed,
                Id = comment.Id,
                RecipeId = comment.RecipeId,
                AppUserId = comment.AppUserId
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditComment(ConfirmComment confirmComment)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(s => s.Id == confirmComment.Id);
            comment.IsConfirmed = confirmComment.IsApproved;
            _context.SaveChanges();
            return RedirectToAction("Comments");
        }
    }
}