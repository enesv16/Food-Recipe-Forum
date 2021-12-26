using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace WebProgramlamaProje.Controllers
{
    public class HomeController : Controller
    {


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IHtmlLocalizer<HomeController> _localizer;

        private readonly ApplicationDbContext _context;

      

        public HomeController(ApplicationDbContext context,IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            
            var recipes = _context.Recipes.Include(r => r.AppUser).Include(s => s.Comments).Include(d => d.Category).OrderByDescending(s => s.PublishTime);
            viewModel.Recipes = recipes;
            viewModel.Categories = _context.Categories;
            return View(viewModel);
        }

       


        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CultureManagement(string culture,string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return LocalRedirect(returnUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
