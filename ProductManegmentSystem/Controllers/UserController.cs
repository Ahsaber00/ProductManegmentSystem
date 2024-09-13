using Microsoft.AspNetCore.Mvc;
using ProductManegmentSystem.Context;
using ProductManegmentSystem.Models;

namespace ProductManegmentSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _context;
        public UserController(IHttpContextAccessor httpContextAccesor)
        {
            _context = httpContextAccesor;
        }
        ProductManegmentContext db=new ProductManegmentContext();
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }
            if (db.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("", "This Email is already in use");
                return View();
            }
            db.Users.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User loginInfo)
        {
            ModelState.Remove("Adress");
            ModelState.Remove("Firstname");
            ModelState.Remove("Lastname");
            ModelState.Remove("Confirmpassword");
            if (!ModelState.IsValid)
            {
                return RedirectToAction("login");
            }

            var getAccount=db.Users.FirstOrDefault(u=>u.Email == loginInfo.Email&&u.Password==loginInfo.Password);
            if(getAccount!=null)
            {
                HttpContext.Session.SetString("Id", getAccount.Id.ToString());
                HttpContext.Session.SetString("Name", getAccount.FirstName);
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError("", "Wrong Eamill or Password");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
