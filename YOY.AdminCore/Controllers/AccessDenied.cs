using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YOY.AdminCore.Controllers
{
    public class AccessDenied : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
