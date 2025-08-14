using Microsoft.Owin.Security;
using Prabesh.Models;
using Prabesh.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Prabesh.Controllers
{
    public class RentalsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeExcept(DeniedRoles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            return View();
        }
    }
}