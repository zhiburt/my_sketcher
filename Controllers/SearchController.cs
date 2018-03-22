using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lastVersionAuthe.Models.SearchViewModels;
using lastVersionAuthe.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using lastVersionAuthe.Models;

namespace lastVersionAuthe.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public SearchController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            ViewBag.Manuals = from m in _context.Manuals
                               select m;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            var manuals = from m in _context.Manuals
                          select m;

            if (!String.IsNullOrEmpty(model.SearchString))
            {
                manuals = await SearchManualsByTermAsync(manuals, model.SearchString, model.Term);
                ViewBag.Manuals = manuals;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(String searchTag)
        {
            var manuals = from m in _context.Manuals
                          select m;

            if (!String.IsNullOrEmpty(searchTag))
            {
                manuals = await SearchManualsByTermAsync(manuals, searchTag, SearchTerm.Tags);
                ViewBag.Manuals = manuals;
            }

            return View("Index");
        }

        #region SuportMethods

        private async Task<IQueryable<Manual>> SearchManualsByTermAsync(IQueryable<Manual> manuals,
            String searchString, SearchTerm term = SearchTerm.Title)
        {
            await Task.Run(() =>
            {
                if (manuals != null)
                {
                    switch (term)
                    {
                        case SearchTerm.Tags:
                            manuals = manuals.Where(manual => manual.Tags.Contains(searchString));
                            break;
                        case SearchTerm.Themes:
                            manuals = manuals.Where(manual => manual.Themes.Contains(searchString));
                            break;
                        case SearchTerm.Title:
                            manuals = manuals.Where(manual => manual.Title.Contains(searchString));
                            break;
                        case SearchTerm.TitleAndBody:
                            manuals = manuals.Where(manual => manual.Body.Contains(searchString) || 
                                                        manual.Title.Contains(searchString));
                            break;
                        default:
                            manuals = manuals.Where(manual => manual.Title.Contains(searchString));
                            break;
                    }
                } 
            });
            return manuals;
        }
        
        #endregion
    }
}