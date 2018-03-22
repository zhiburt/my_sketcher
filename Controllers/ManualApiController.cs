using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lastVersionAuthe.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using lastVersionAuthe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace lastVersionAuthe.Controllers
{
    [Produces("application/json")]
    public class ManualApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public ManualApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Route("api/rating")]
        [HttpGet]
        public async Task<double> GetRatingManualAsync(string manualName, string creator)
        {
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
               manuals.UserCreator == creator &&
               manuals.Title == manualName);

            if (manual != null)
            {
                return manual.Rating;
            }
            Response.StatusCode = 404;

            return 0;
        }

        [HttpPost]
        [Route("api/setRating")]
        public async Task<double> AddRatingManualAsync(string manualName, string creator, double rating)
        {
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
               manuals.UserCreator == creator &&
               manuals.Title == manualName);
            var user = await _userManager.Users.FirstOrDefaultAsync(p =>
                        p.UserName == creator);

            if (manual != null && user != null)
            {
                var userRatings = await _context.UserRating.FirstOrDefaultAsync(rait =>
                    rait.UserId == user.Id);

                if (userRatings == null)
                {
                    await _context.UserRating.AddAsync(userRatings);

                }

                manual.Rating = await AddRating(manual.Rating, rating);
                manual.AmountRatingChange++;
                await _context.SaveChangesAsync();
                return (int)manual.Rating;
            }
            Response.StatusCode = 404;
            return 0;
        }

        [HttpPost]
        [Route("api/AddLike")]
        public async Task<string> AddLikeManualAsync(string manualName, string creator, bool data)
        {
            var manual = await _context.Manuals.FirstOrDefaultAsync(manuals =>
               manuals.UserCreator == creator &&
               manuals.Title == manualName);

            if (manual != null)
            {
                manual.AmountLike = await AddLike(manual.AmountLike, data);
                await _context.SaveChangesAsync();
                return manual.AmountLike;
            }
            Response.StatusCode = 404;
            return null;
        }

        #region Supports

        private async Task<Double> AddInfo(Double rating, Double addRating)
        {
            return await Task.Run(() =>
            {
                if (rating != 0)
                {
                    rating = (rating + addRating) / 2;
                    return rating;
                }
                rating = addRating;
                return rating;
            });
        }

        private async Task<Double> AddRating(Double rating, Double addRating)
        {
            return await Task.Run(() =>
              {
                  if (rating != 0)
                  {
                      rating = (rating + addRating) / 2;
                      return rating;
                  }
                  rating = addRating;
                  return rating;
              });
        }

        private async Task<string> AddLike(String amount, bool data)
        {
            return await Task.Run(() =>
            {
                double amountLikes;
                if (amount == null)
                {
                    amountLikes = 0;
                }
                else
                    amountLikes = Double.Parse(amount);
                return data ? (++amountLikes).ToString() : (--amountLikes).ToString();
            });
        }

        #endregion

    }
}
