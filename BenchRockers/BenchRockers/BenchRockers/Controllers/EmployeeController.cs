using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BenchRockers.BusinessLayer.Interfaces;
using BenchRockers.Common.DataObjects;
using BenchRockers.Models;

namespace BenchRockers.Web.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IServiceFacade _serviceFacade;

        public EmployeeController(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        public EmployeeController()
        {
        }

//        public FeaturedAccommodationController()
//    : this(new AccommodationService())
//{
//}

//public FeaturedAccommodationController(IAccommodationService accommodationService)
//{
//}


        //private BenchRockersDbContext db = new BenchRockersDbContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(_serviceFacade.EmployeeService.GetAllEmployees());
        }

        public ActionResult Employees()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EmployeeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var employeeCount = _serviceFacade.EmployeeService.GetAllEmployees().Count;
                var skills = GetEmployees(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = skills, TotalRecordCount = employeeCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Employee> GetEmployees(int startIndex, int count, string sorting)
        {
            //IEnumerable<Employee> query = _serviceFacade.EmployeeService.GetAllEmployees().Where(e => e.IsOnBench == true).ToList();
            IEnumerable<Employee> query = _serviceFacade.EmployeeService.GetAllEmployees().ToList();

            //IEnumerable<Employee> query = db.Employees;

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(e => e.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(e => e.Name);
            }
            else if (sorting.Equals("RoleId ASC"))
            {
                query = query.OrderBy(e => e.RoleId);
            }
            else if (sorting.Equals("RoleId DESC"))
            {
                query = query.OrderByDescending(e => e.RoleId);
            }
            else if (sorting.Equals("Account ASC"))
            {
                query = query.OrderBy(e => e.Account);
            }
            else if (sorting.Equals("Account DESC"))
            {
                query = query.OrderByDescending(e => e.Account);
            }
            else if (sorting.Equals("TotalExp ASC"))
            {
                query = query.OrderBy(e => e.TotalExp);
            }
            else if (sorting.Equals("TotalExp DESC"))
            {
                query = query.OrderByDescending(e => e.TotalExp);
            }
            else if (sorting.Equals("Location ASC"))
            {
                query = query.OrderBy(e => e.Location);
            }
            else if (sorting.Equals("Location DESC"))
            {
                query = query.OrderByDescending(e => e.Location);
            }
            else
            {
                query = query.OrderBy(e => e.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedEmployee = _serviceFacade.EmployeeService.CreateEmployee(employee);
                addedEmployee.IsOnBench = true;
                //_serviceFacade..SaveChanges();
                return Json(new { Result = "OK", Record = addedEmployee });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

               // _serviceFacade.Entry(employee).State = EntityState.Modified;
                _serviceFacade.EmployeeService.UpdateEmployee(employee);
                //_serviceFacade.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int EmployeeId)
        {
            try
            {
                Thread.Sleep(50);
                //Employee employee = _serviceFacade.EmployeeService.GetAllEmployees().Where(e => e.EmployeeId==EmployeeId)
                _serviceFacade.EmployeeService.DeleteEmployee(EmployeeId);
                //employee.IsOnBench = false;
                //db.Employees.Remove(employee);
                //_serviceFacade.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #region Role actions

        [HttpPost]
        public JsonResult GetRoles()
        {
            try
            {
               // var roles = new RoleController().GetAllRoles().Select(r => new { DisplayText = r.RoleName, Value = r.RoleId }); ;
                var roles = _serviceFacade.EmployeeService.GetRoles().Select(r => new { DisplayText = r.RoleName, Value = r.RoleId }); 
                    
                return Json(new { Result = "OK", Options = roles });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        [HttpPost]
        public List<Employee> GetAllEmployees()
        {
            return _serviceFacade.EmployeeService.GetAllEmployees().OrderBy(e => e.Name).ToList();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _serviceFacade.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}