using Prose.Application.Interfaces;
using Prose.Application.ViewModels;
using Prose.UI.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Prose.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserCreateInput userCreateInput)
        {
           var user = _userService.SignIn(userCreateInput);
            if (user != null && user.Enable)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("MyTopics", "Topic");
            }


            return View();
        }
                         
        public ActionResult Signup()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(UserSignup userSignUp)
        {
            if (_userService.ValidUsernameIsUnic(userSignUp.Username))
            {
                var user = _userService.AddUser(new UserCreateInput { Username = userSignUp.Username, Password = userSignUp.Password});
                if (user != null && user.Enable)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("MyTopics", "Topic");
                }
            }

            ModelState.AddModelError("Username", "Username Already Exists.");
            ModelState.AddModelError(string.Empty, "Username Already Exists.");

            return View(userSignUp);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}