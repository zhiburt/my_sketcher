using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using lastVersionAuthe.Models;
using Microsoft.Extensions.Logging;
using lastVersionAuthe.Data;

namespace lastVersionAuthe.Controllers
{
    public class WebController : Controller
    {
        private readonly String _curectlyPassword = "123456789";
        private readonly String _curectlyEmail = "zhiburt@topdeveloper.com";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public WebController(UserManager<ApplicationUser> userManager,
            ILogger<WebController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> Users(string email, string password, string username)
        {
            if (email == _curectlyEmail && password == _curectlyPassword)
                if (ModelState.IsValid && username != null)
                {
                    var user = await _userManager.FindByNameAsync(username);
                    if (user != null)
                    {
                        _logger.LogDebug("User fly in API Python.");
                        return Json(user);
                    }
                    return Json("user : not fount");
                }
            return Json("Model is not Valid");
        }

        [HttpGet]
        public JsonResult UsersAll(string email, string password)
        {
            if (email == _curectlyEmail && password == _curectlyPassword)
            {
                _logger.LogDebug("Full DB Users fly in API Python.");
                return Json(_userManager.Users.ToArray());
            }
            return Json("Valid : Not valid password and user");
        }

        [HttpPost]
        public async Task<JsonResult> Users(string email, string password, string username, string newUsername)
        {
            if (email == _curectlyEmail && password == _curectlyPassword)
                if (ModelState.IsValid && username != null && newUsername != null)
                {
                    var user = await _userManager.FindByNameAsync(username);
                    if (user != null)
                    {
                        await _userManager.SetUserNameAsync(user, newUsername);
                        _logger.LogDebug($"User {username} reset name fly in API Python.");
                        return Json("Status : OK");
                    }
                    return Json("user : not fount");
                }
            return Json("Model is not Valid");
        }
    }
}