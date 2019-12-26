using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Awards.Models;
using Awards.Models.ViewModels;
using System.Data.SqlClient;

namespace Awards.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;
        public HomeController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mainPage = new MainPage();
            mainPage.Users = _context.Users.ToList();
            mainPage.Awards = _context.Awards.ToList();
            var usersAwards = _context.UsersAwards.ToList();
            foreach (var user in mainPage.Users)
            {
                user.Awards = new List<Award>();
                foreach (var connecter in usersAwards)
                {
                    if (connecter.UserId == user.Id)
                    {
                        user.Awards.AddRange(mainPage.Awards.Where(a => a.Id == connecter.AwardId).ToList());
                    }                 
                }
            }
            return View(mainPage);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
