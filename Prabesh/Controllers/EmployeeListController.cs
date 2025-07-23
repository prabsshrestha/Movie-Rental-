using Prabesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace Prabesh.Controllers
{
    public class EmployeeListController : Controller
    {
        // GET: EmployeeList
        public ActionResult EmployeeList([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<Employee> _emp = new List<Employee>
        {
            new Employee(1, "Bobb", "Ross"),
            new Employee(2, "Pradeep", "Raj"),
            new Employee(3, "Arun", "Kumar")
        };

                DataSourceResult result = _emp.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}