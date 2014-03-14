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
    public class RoleController : Controller
    {
        //private BenchRockersDbContext db = new BenchRockersDbContext();

        private readonly IServiceFacade _serviceFacade;

        public RoleController(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        public RoleController()
        {
        }

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View(_serviceFacade.RoleService.GetAllRoles());
        }

        public ActionResult Roles()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RolesList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var roleCount = _serviceFacade.RoleService.GetAllRoles().Count();
                var roles = GetRoles(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = roles, TotalRecordCount = roleCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Role> GetRoles(int startIndex, int count, string sorting)
        {
            IEnumerable<Role> query = _serviceFacade.RoleService.GetAllRoles().ToList();

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(r => r.RoleName);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(r => r.RoleName);
            }
            else
            {
                query = query.OrderBy(r => r.RoleName); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateRole(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedRole = _serviceFacade.RoleService.CreateRole(role);
                //db.SaveChanges();
                return Json(new { Result = "OK", Record = addedRole });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateRole(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                //db.Entry(role).State = EntityState.Modified;
                //db.SaveChanges();

                _serviceFacade.RoleService.UpdateRole(role);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteRole(int RoleId)
        {
            try
            {
                Thread.Sleep(50);
                //Role role = db.Roles.Find(RoleId);
                //db.Roles.Remove(role);
                //db.SaveChanges();

                _serviceFacade.RoleService.DeleteRole(RoleId);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public List<Role> GetAllRoles()
        {
            return _serviceFacade.RoleService.GetAllRoles().OrderBy(e => e.RoleId).ToList();
        }       
    }
}