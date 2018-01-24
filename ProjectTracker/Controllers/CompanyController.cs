using ProjectTracker.Domain;
using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracker.Controllers
{
    public class CompanyController : Controller
    {
        PTContext ptcontext = new PTContext();
        // GET: Company
        public ActionResult Index()
        {
            List<Company> companies = new List<Company>();
            companies = ptcontext.Companies.ToList();
            return View(companies);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            Company company = new Company();
            return View(company);
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Company model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                ptcontext.Companies.Add(model);
                ptcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            var company = ptcontext.Companies.FirstOrDefault(x => x.ID == id);
            if (company == null)
                return RedirectToAction("Index");
            return View(company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(Company model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var company = ptcontext.Companies.FirstOrDefault(x => x.ID == model.ID);
                company.Name = model.Name;
                ptcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var company = ptcontext.Companies.FirstOrDefault(x => x.ID == id);
            if (company == null)
                return RedirectToAction("Index");
            ptcontext.Companies.Remove(company);
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
