using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Awards.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awards.Controllers
{
    public class UserController : Controller
    {
        ApplicationContext _context;
        public UserController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {         
            user.Age = user.BirthDate.Year - DateTime.Today.Year;
            user.Age *= -1;
            var imageDownloader = new ImageHelper();
            user.Photo = imageDownloader.WriteImage(user.PhotoData, _context);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult DeleteUser(int Id)
        {
            var user = _context.Users.FirstOrDefault(u=>u.Id == Id);
            new ImageHelper().DeletePhoto(user.Photo);
            _context.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EditUser(int Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            user.Age = user.BirthDate.Year - DateTime.Today.Year;
            user.Age *= -1;
            new ImageHelper().EditImage(user.PhotoData, user.Photo);
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult AwardUser(int userId, int awardId)
        {
            if (!_context.UsersAwards.Any(ua=>ua.UserId == userId && ua.AwardId == awardId))
            {
                _context.UsersAwards.Add(new UsersAwards() { UserId = userId, AwardId = awardId });
                _context.SaveChanges();
            }          
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult RemoveUsersAward(int userId, int AwardId)
        {
            var userAward = _context.UsersAwards.FirstOrDefault(ua=>ua.AwardId == AwardId && ua.UserId == userId);
            _context.Remove(userAward);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}