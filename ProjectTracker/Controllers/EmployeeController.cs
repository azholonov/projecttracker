using ProjectTracker.Domain;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracker.Controllers
{
    public class EmployeeController : Controller
    {

        PTContext ptcontext = new PTContext();

        public ActionResult Index()
        {
            List<Employee> companies = new List<Employee>();
            companies = ptcontext.Employees.ToList();
            return View(companies);
        }
        
        public ActionResult Create()
        {
            Employee company = new Employee();
            return View(company);
        }
        
        [HttpPost]
        public ActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                ptcontext.Employees.Add(model);
                ptcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var employee = ptcontext.Employees.FirstOrDefault(x => x.ID == id);
            if (employee == null)
                return RedirectToAction("Index");
            return View(employee);
        }
        
        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var employee = ptcontext.Employees.FirstOrDefault(x => x.ID == model.ID);
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.Email = model.Email;
                ptcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var employee = ptcontext.Employees.FirstOrDefault(x => x.ID == id);
            if (employee == null)
                return RedirectToAction("Index");
            ptcontext.Employees.Remove(employee);
            ptcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            ptcontext.Dispose();
            base.Dispose(disposing);
        }
    }
}
