using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchBoxSample.Models;
using System.Data;

namespace SearchBoxSample.Controllers
{
   
    public class DocumentManagementController : Controller
    {

        private SampleEntities db = new SampleEntities();

        //
        // GET: /DocumentsManagement/

        public ActionResult Index()
        {
            var documents = db.Documents.ToList(); ;
            return View(documents);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: /DocumentManager/Edit/5
        public ActionResult Edit(int id)
        {
            Document document = db.Documents.Find(id);
            return View(document);
        }

        [HttpPost]
        public ActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        public ActionResult Delete(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
