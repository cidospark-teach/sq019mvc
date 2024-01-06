using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserManagementApp.Models;
using UserManagementApp.Models.ViewModels;

namespace UserManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dummyListOfUsers = new List<UserToReturnViewModel>
            {
                new UserToReturnViewModel{ Id = "1", FirstName = "Kenedy", LastName = "Tariah", Email="kenedyt@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" },
                new UserToReturnViewModel{ Id = "2", FirstName = "Murphy", LastName = "Ogbeide", Email="murphyo@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" },
                new UserToReturnViewModel{ Id = "3", FirstName = "Agberowo", LastName = "Kayode", Email="agberowok@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" },
                new UserToReturnViewModel{ Id = "4", FirstName = "Babatunde", LastName = "Mustapha", Email="babatundem@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" },
                new UserToReturnViewModel{ Id = "5", FirstName = "Godwin", LastName = "Ozioko", Email="godwino@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" },
                new UserToReturnViewModel{ Id = "6", FirstName = "Ozoeze", LastName = "Boniface", Email="ozoezob@sample.com", PhotoUrl = "https://image-placeholder.com/images/actual-size/120x150.png" }
            };
            return View(dummyListOfUsers);
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