using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Awards.Models;
using Awards.Models.ViewModels;
using System.Data.SqlClient;
using System.Threading;
using Awards.Models.Helpers;

namespace Awards.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;
        List<User> _users;
        public HomeController(ApplicationContext context)
        {
            _context = context;
            _users = GetMainPage().Users;
        }
        public IActionResult Index()
        {
            var mainPage = GetMainPage();
            return View(mainPage);
        }

        private MainPage GetMainPage()
        {
            var mainPage = new MainPage();
            mainPage.Users = _context.Users.Where(u => u.Name != null).ToList();
            mainPage.Awards = _context.Awards.Where(a=>a.Title != null).ToList();
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
            return mainPage;
        }
        public FileResult DownloadUsersTxt()
        {
            new TxtHelper().CreateTxtWithUsers(_users);
            return new TxtHelper().GetUsersTxt();
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
        [HttpGet]
        public bool CheckDateOfBirth(DateTime BirthDate)
        {
            if (DateTime.Now.Year - BirthDate.Year > 150 || (DateTime.Now.Year - BirthDate.Year) < 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
