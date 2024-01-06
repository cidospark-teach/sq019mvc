﻿using Microsoft.AspNetCore.Mvc;

namespace UserManagementApp.Controllers
{
    public class ErrorController : Controller
    {

        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            return View("NotFound");
        }
    }
}
