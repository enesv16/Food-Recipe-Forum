using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;
using WebProgramlamaProje.HelpersAndServices;

namespace WebProgramlamaProje.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Blog

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,FoodRecipe,PreparationTime,CategoryId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                recipe.AppUserId = userId;
                recipe.PublishTime = DateTime.Now;

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", "Seçiniz");
            return View(recipe);
        }

        //public async Task<IActionResult> BlogDetails(string title, int id)
        //{

        //    var recipe = await _context.Recipes
        //        .Include(r => r.AppUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (recipe == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(recipe);
        //}





























        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipes.Include(r => r.Category).Include(a => a.AppUser);

            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Blog/Details/5

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {


            var recipe = await _context.Recipes
                .Include(r => r.AppUser).Include(s => s.Comments).ThenInclude(d => d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (recipe == null)
                return RedirectToAction(nameof(Index));
            //if (Url.FriendlyUrl(recipe.Title) != title)
            //    recipe = null;
            

            return View(recipe);
        }
        [HttpPost]
        public async Task<IActionResult> Details(string yorum, int id)
        {


            var recipe = await _context.Recipes
                .Include(r => r.AppUser).Include(s => s.Comments).ThenInclude(d=>d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);



            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == userId);
            
            if (!ModelState.IsValid)
            {
                return View(yorum,id);
            }
            else
            {
                Comment yeniYorum = new Comment()
                {
                    Text = yorum,
                    RecipeId = id,
                    Recipe = recipe,
                    AppUserId = userId,
                    AppUser = user,
                    PublishTime = DateTime.Now

                };
                _context.Comments.Add(yeniYorum);
                _context.SaveChanges();
            }
        
        ViewBag.basarili = "Yorumunuz gönderildi admin onayı bekleniyor.";
            recipe = await _context.Recipes
                    .Include(r => r.AppUser).Include(s => s.Comments).ThenInclude(d => d.AppUser)
                    .FirstOrDefaultAsync(m => m.Id == id);
            return View(recipe);
            //if (recipe == null)
            //    return RedirectToAction(nameof(Index));
            //if (Url.FriendlyUrl(recipe.Title) != title)
            //    recipe = null;


            //return View(recipe);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", recipe.AppUserId);
            return View(recipe);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FoodRecipe,ImageUrl,IsConfirmed,PreparationTime,PublishTime,AppUserId,CategoryId")] Recipe recipe)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", recipe.AppUserId);
            return View(recipe);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
