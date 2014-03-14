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
    public class SkillsController : Controller
    {

        private readonly IServiceFacade _serviceFacade;

        //private BenchRockersDbContext db = new BenchRockersDbContext();

        public SkillsController(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        public SkillsController()
        {
        }

        //
        // GET: /Skills/

        public ActionResult Index()
        {
            return View(_serviceFacade.SkillService.GetAllSkills());
        }

        public ActionResult Skills()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SkillsList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var skillCount = _serviceFacade.SkillService.GetAllSkills().Count();
                var skills = GetSkills(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = skills, TotalRecordCount = skillCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Skill> GetSkills(int startIndex, int count, string sorting)
        {
            IEnumerable<Skill> query = _serviceFacade.SkillService.GetAllSkills().ToList();

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(s => s.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(s => s.Name);
            }
            else
            {
                query = query.OrderBy(s => s.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateSkill(Skill skill)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedSkill = _serviceFacade.SkillService.CreateSkill(skill);
                //db.SaveChanges();
                return Json(new { Result = "OK", Record = addedSkill });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateSkill(Skill skill)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                //db.Entry(skill).State = EntityState.Modified;
                //db.SaveChanges();
                _serviceFacade.SkillService.UpdateSkill(skill);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteSkill(int skillId)
        {
            try
            {
                Thread.Sleep(50);
                //Skill skill = db.Skills.Find(skillId);
                //db.Skills.Remove(skill);
                //db.SaveChanges();

                _serviceFacade.EmployeeService.DeleteEmployee(skillId);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public List<Skill> GetAllSkills()
        {
            return _serviceFacade.SkillService.GetAllSkills().OrderBy(e => e.SkillId).ToList();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}