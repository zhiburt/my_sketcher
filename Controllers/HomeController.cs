using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lastVersionAuthe.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using lastVersionAuthe.Data;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace lastVersionAuthe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
        {
            this._context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.TopManuals = GetTopManuals(_context.Manuals);
            ViewBag.LastManuals = GetLastManuals(_context.Manuals);

            string message = "Hello World";
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            byte[] messageHash = SHA256.Create().ComputeHash(messageByte);
            var messageList = new List<string>();
            messageList.Add(message);
            messageList.Add(Convert.ToBase64String(messageByte, Base64FormattingOptions.InsertLineBreaks));
            messageList.Add(messageHash.ToString());
            messageList.Add(Convert.ToBase64String(messageHash));
            messageList.Add(Convert.ToBase64String(MD5.Create().ComputeHash(messageByte)));
            ViewBag.MessageList = messageList;
            Type[] type = Assembly.GetExecutingAssembly().GetTypes();
            ViewBag.Types = type;


            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["Messages"];

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Language()
        {
            ViewData["Message"] = "Your language select page.";

            return View();
        }
        #region Support Methods

        private IQueryable<Manual> GetTopManuals(IQueryable<Manual> list, int amount = 3)
        {
            var responceList = list.OrderByDescending(n => n.Rating).Take(amount);

            return responceList;
        }

        private IQueryable<Manual> GetLastManuals(IQueryable<Manual> list, int amount = 3)
        {
            var responceList = list.OrderByDescending(n => n.CreatedDate).Take(amount);

            return responceList;
        }

        #endregion
    }
}
