using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awards.Models;
using Microsoft.AspNetCore.Mvc;

namespace Awards.Controllers
{
    public class AwardController : Controller
    {
        ApplicationContext _context;
        public AwardController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var awards = _context.Awards.ToList();
            return View(awards);
        }
        public IActionResult AddAward()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAward(Award award)
        {
            _context.Awards.Add(award);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
       [HttpPost]
       public IActionResult RemoveAward(int awardId)
        {
            var usersAwards = _context.UsersAwards.Where(a => a.AwardId == awardId);
            var award = _context.Awards.FirstOrDefault(a => a.Id == awardId);
            _context.RemoveRange(usersAwards);
            _context.Remove(award);
            _context.SaveChanges();
            return RedirectToAction("Index", "Award");
        }
        public IActionResult EditAward(int awardId)
        {
            var award = _context.Awards.FirstOrDefault(a => a.Id == awardId);
            return View(award);
        }
        [HttpPost]
        public IActionResult EditAward(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
            return RedirectToAction("Index", "Award");
        }
    }
}