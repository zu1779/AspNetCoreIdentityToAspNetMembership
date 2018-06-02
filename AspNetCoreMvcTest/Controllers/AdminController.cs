namespace AspNetCoreMvcTest.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using AspNetCoreIdentityAspNetMembershipImplementation;
    using AspNetCoreMvcTest.Models.Admin;

    public class AdminController : Controller
    {
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        private readonly UserManager<ApplicationUser> userManager;

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Users = userManager.Users.ToList()
                .Select(async c => new UserModel
                {
                    UserId = c.UserId,
                    UserName = c.UserName,
                    Roles = await userManager.GetRolesAsync(c),
                }).Select(c => c.Result).ToList()
            };
            return View(model);
        }
    }
}