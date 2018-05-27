namespace AspNetCoreMvcTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using AspNetCoreIdentityAspNetMembershipImplementation;
    using AspNetCoreMvcTest.Models;

    public class HomeController : Controller
    {
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        private readonly UserManager<ApplicationUser> userManager;

        public IActionResult Index()
        {
            var list = new List<string> { "Test page model" };
            list.Add("Listing users:");
            list.AddRange(userManager.Users.Select(u => JsonConvert.SerializeObject(u)));
            return View(list);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
    }
}
