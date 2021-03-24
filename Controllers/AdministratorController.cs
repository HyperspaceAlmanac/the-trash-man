using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: AdministratorController
        public ActionResult Index()
        {
            return View();
        }
    }
}
