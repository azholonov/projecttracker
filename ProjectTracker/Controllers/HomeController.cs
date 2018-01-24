using ProjectTracker.Domain;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        PTContext ptcontext = new PTContext();
        public ActionResult Index(string name = "", int customer= 0, int executor=0, string bgdate="", string edate="", int manager=0, int employee=0, int priority=-1)
        {
            var companylist = ptcontext.Companies.ToList();
            var employeelist = ptcontext.Employees.Select(x => new { ID = x.ID, Name = (x.LastName + " " + x.FirstName) }).ToList();
            companylist.Add(new Company { ID = 0, Name = "Не выбран" });
            employeelist.Add(new { ID = 0, Name="Не выбран" });
            SelectList companies = new SelectList(companylist.OrderBy(x=>x.ID), "Id", "Name", 0);
            ViewBag.Companies = companies;
            SelectList employees = new SelectList(employeelist.OrderBy(x=>x.ID), "Id", "Name", 0);
            ViewBag.Employees = employees;
            var enumData = (from Priority e in Enum.GetValues(typeof(Priority))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           }).ToList();
            enumData.Add(new { ID = -1, Name = "Не выбран" });
            SelectList priorities = new SelectList(enumData.OrderBy(x=>x.ID), "ID", "Name", -1);
            ViewBag.Priorities = priorities;

            IQueryable<Project> projects;
            projects = ptcontext.Projects;
            #region filters
            DateTime beginDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (!string.IsNullOrEmpty(bgdate))
            {
                if (DateTime.TryParse(bgdate, out beginDate))
                    projects = projects.Where(x => x.BeginDate >= beginDate);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                if( DateTime.TryParse(edate, out endDate))
                    projects = projects.Where(x => x.EndDate <= endDate); ;
            }
            if (!string.IsNullOrEmpty(name))
                projects = projects.Where(x => x.Name.Contains(name));
            if (customer > 0 )
                projects = projects.Where(x => x.CustomerCompany.ID == customer);
            if (executor > 0)
                projects = projects.Where(x => x.ExecutorCompany.ID == executor);
            if (manager > 0)
                projects = projects.Where(x => x.Manager.ID == manager);
            if (employee > 0)
                projects = projects.Where(x => x.Employees.Count(m => m.ID == employee) > 0);
            if (priority >= 0)
                projects = projects.Where(x => x.Priority == (Priority)priority);

            #endregion

            return View(projects.ToList());
        }

        public ActionResult Create()
        {
            Project model = new Project();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Project model)
        {
            try
            {
                ptcontext.Projects.Add(model);
                ptcontext.SaveChanges();
                return RedirectToAction("Edit", new { id = model.ID });
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var companylist = ptcontext.Companies.ToList();
            var employeelist = ptcontext.Employees.ToList();
            SelectList companies = new SelectList(companylist, "Id", "Name");
            ViewBag.Companies = companies;
            SelectList employees = new SelectList(employeelist, "Id", "LastName");
            ViewBag.Managers = employees;
            ViewBag.Employees = employeelist;
            var model = ptcontext.Projects.FirstOrDefault(x => x.ID == id);
            if (model.CustomerCompany == null)
                ViewBag.Title = "Детали";
            else
                ViewBag.Title = "Редактирование";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Project model, int[] selectedEmps)
        {
            try
            {
                var frombase = ptcontext.Projects.FirstOrDefault(x => x.ID == model.ID);
                frombase.Name = model.Name;
                frombase.BeginDate = model.BeginDate;
                frombase.EndDate = model.EndDate;
                frombase.CustomerCompany = ptcontext.Companies.FirstOrDefault(x => x.ID == model.CustomerCompany.ID);
                frombase.ExecutorCompany = ptcontext.Companies.FirstOrDefault(x => x.ID == model.ExecutorCompany.ID);
                frombase.Manager = ptcontext.Employees.FirstOrDefault(x => x.ID == model.Manager.ID);
                frombase.Description = model.Description;

                frombase.Employees.Clear();
                if (selectedEmps != null)
                {
                    foreach (var emp in ptcontext.Employees.Where(x => selectedEmps.Contains(x.ID)))
                    {
                        frombase.Employees.Add(emp);
                    }
                }

                ptcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            ptcontext.Dispose();
            base.Dispose(disposing);
        }
    }
}
