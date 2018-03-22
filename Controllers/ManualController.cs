using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using lastVersionAuthe.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using lastVersionAuthe.Models.ManualViewModels;
using lastVersionAuthe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace lastVersionAuthe.Controllers
{

    [Authorize]
    public class ManualController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IStringLocalizer<ManualController> _localizer;


        public ManualController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IStringLocalizer<ManualController> localizer)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index(SortState sortState = SortState.TitleAsc)
        {
            //TODO refactor
            IQueryable<Manual> list = null;
            var user = await _userManager.GetUserAsync(User);
            var manuals = user.GetUserManuals();
            if (manuals != null)
            {
               list  = await GetUserManuals(manuals, user);

                ViewBag.TitleSort = sortState == SortState.TitleAsc ?
              SortState.TitleDesc : SortState.TitleAsc;
                ViewBag.LikeSort = sortState == SortState.LikeAsc ?
                    SortState.LikeDesc : SortState.LikeAsc;
                ViewBag.RatingSort = sortState == SortState.RatingAsc ?
                    SortState.RatingDesc : SortState.RatingAsc;
                list = SortedListManuals(list, sortState);
            }
       
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> IndexOtherUser(String username, SortState sortState = SortState.TitleAsc)
        {
            //TODO refactor
            IQueryable<Manual> list = null;
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            var manuals = user.GetUserManuals();
            if (manuals != null && user != null)
            {
                list = await GetUserManuals(manuals, user);
                ViewBag.TitleSort = sortState == SortState.TitleAsc ?
              SortState.TitleDesc : SortState.TitleAsc;
                ViewBag.LikeSort = sortState == SortState.LikeAsc ?
                    SortState.LikeDesc : SortState.LikeAsc;
                ViewBag.RatingSort = sortState == SortState.RatingAsc ?
                    SortState.RatingDesc : SortState.RatingAsc;
                ViewBag.UserName = user.UserName;
                list = SortedListManuals(list, sortState);
            }

            return View(list);
        }

        [HttpGet]
        public  IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManualViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var room = await _context.Manuals.FirstOrDefaultAsync(u =>
                   u.Title == model.Title &&
                   u.UserCreator == _userManager.GetUserName(HttpContext.User));
                //TODO BUG IF Title null fix please
                if (room == null && model.Title != null)
                {
                    var manual = new Manual
                    {
                        Themes = model.Themes,
                        Tags = model.Tags,
                        Body = model.Content,
                        Title = model.Title,
                        ShortDescription = model.ShortDescription,
                        CreatedDate = DateTime.Now,
                        UserCreator = _userManager.GetUserName(HttpContext.User),
                        ManualImage = model.Image
                    };

                    var user = await _userManager.GetUserAsync(User);

                    await _context.Manuals.AddAsync(manual);
                    user.Manuals = ConcatManuals(user.Manuals, manual.Title);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"User {manual.UserCreator} created a new manual.");
                    return RedirectToAction("Index", "Manual");
                }
                else
                    ModelState.AddModelError("", "Sorry, you already created such a manual");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(String title, String userCreator)
        {
            //TODO test
            var user = await _userManager.GetUserAsync(User);
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
                manuals.UserCreator == userCreator &&
                manuals.Title == title);

            if (manual != null && user.UserName == userCreator && userCreator != null)
            {
                var manualViewModel = new EditManualViewModel
                {
                    Title = manual.Title,
                    Content = manual.Body,
                    ShortDescription = manual.ShortDescription,
                    Tags = manual.Tags,
                    Themes = manual.Themes
                };
                
                return View(manualViewModel);
            }

            return NotFound(title);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditManualViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var room = await _context.Manuals.FirstOrDefaultAsync(u =>
                   u.Title == model.Title &&
                   u.UserCreator == _userManager.GetUserName(HttpContext.User));
                //TODO BUG IF Title null fix please
                if (room != null && model.Title != null)
                {
                    var manual = new Manual
                    {
                        Themes = model.Themes,
                        Tags = model.Tags,
                        Body = model.Content,
                        Title = model.Title,
                        ShortDescription = model.ShortDescription,
                        CreatedDate = DateTime.Now,
                        UserCreator = _userManager.GetUserName(HttpContext.User),
                    };

                    var user = await _userManager.GetUserAsync(User);
                    _context.Manuals.Remove(room);

                    await _context.Manuals.AddAsync(manual);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"User {manual.UserCreator} created a new manual.");
                    return RedirectToAction("Index", "Manual");
                }
                else
                    ModelState.AddModelError("", "Sorry, you already created such a manual");
            }

            return View(model);
        }

        [HttpPost]
        public Task<IActionResult> EditSaveChange(CreateManualViewModel model, string returnUrl = null)
        {
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Remove()
        {
            List<Manual> list = null;
            var user = await _userManager.GetUserAsync(User);
            var manuals = user.GetUserManuals();
            if (manuals != null)
            {
                list = new List<Manual>();
                foreach (var item in manuals)
                {
                    var manualInContext = await _context.Manuals.FirstOrDefaultAsync(manual =>
                        manual.Title == item && manual.UserCreator == user.UserName);
                    if (manualInContext != null)
                    {
                        list.Add(manualInContext);
                    }
                    else
                        _logger.LogWarning($"{item} manual not found , User {user.UserName}");
                }
            }
            ViewBag.Manuals = list;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(DeleteViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
                manuals.Title == model.Title && manuals.UserCreator == user.UserName);
            if (manual != null)
            {
                user.Manuals = RemoveManuals(user.Manuals, model.Title);
                _context.Manuals.Remove(manual);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"manual {model.Title} remove, User {user.UserName}");
                return RedirectToAction("Index", "Manual");
            }
            else
                _logger.LogInformation($"manual {model.Title} User {user.UserName} not Found");

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Manual(String title, String userCreator)
        {
            //TODO test
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
                manuals.UserCreator == userCreator &&
                manuals.Title == title);

            if (manual != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(us =>
                us.UserName == userCreator);

                var manualViewModel = new ManualViewModel
                {
                    Title = manual.Title,
                    Body = manual.Body,
                    Creator = manual.UserCreator,
                    ShortDescription = manual.ShortDescription,
                    AmountLike = manual.AmountLike,
                    Rating = manual.Rating,
                    Tags = GetTagsManuals(manual.Tags),
                    ImageManual = manual.ManualImage,
                    OtherManualsUser = user.GetUserManuals()
                };

                return View(manualViewModel);
            }
            return NotFound(title);
        }



        #region Suport Methods

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        private String ConcatManuals(String manuals, String newManual)
        {
            if (manuals == null)
            {
                return newManual;
            }
            return (manuals + "/" + newManual);
        }

        private String RemoveManuals(String manuals, String removeManuals)
        {
            String responce = manuals.Replace(removeManuals, " ");
            if (responce == " ")
                responce = null;

            return responce;
        }

        private IEnumerable<String> GetTagsManuals(String tags)
        {
             return tags?.Split(','); ;
        }

        private async Task<IQueryable<Manual>> GetUserManuals(IEnumerable<String> manuals, ApplicationUser user)
        {
            List<Manual> list = new List<Manual>(); 
            foreach (var item in manuals)
            {
                var manualInContext = await _context.Manuals.FirstOrDefaultAsync(manual =>
                    manual.Title == item && manual.UserCreator == user.UserName);
                if (manualInContext != null)
                {
                    list.Add(manualInContext);
                }
            }
            return list.AsQueryable();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private IQueryable<Manual> SortedListManuals(IQueryable<Manual> list,SortState sortState)
        {
            IQueryable<Manual> responce = null;
            switch (sortState)
            {
                case SortState.TitleDesc:
                    responce = list.OrderByDescending(x => x.Title);
                    break;
                case SortState.LikeAsc:
                    responce = list.OrderBy(x => x.AmountLike);
                    break;
                case SortState.LikeDesc:
                    responce = list.OrderByDescending(x => x.AmountLike);
                    break;
                case SortState.RatingAsc:
                    responce = list.OrderBy(x => x.Rating);
                    break;
                case SortState.RatingDesc:
                    responce = list.OrderByDescending(x => x.Rating);
                    break;
                default:
                    responce = list.OrderBy(x => x.Title);
                    break;
            }

            return responce;
        }
        
        #endregion
    }

    static class ApplicationUserExtension
    {
        public static IEnumerable<String> GetUserManuals(this ApplicationUser user)
        {
            return user.Manuals?.Split('/');
        }
    }
}