using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchBoxSample.Models;
using System.Data;
using Nest;
using System.Configuration;

namespace SearchBoxSample.Controllers
{

    public class DocumentManagementController : Controller
    {

        private SampleEntities db = new SampleEntities();

        //
        // GET: /DocumentsManagement/

        public ActionResult Index()
        {
            var documents = db.Documents.ToList();
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
                Index(document, "create");
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
                Index(document, "update");
                return RedirectToAction("Index");
            }

            return View(document);
        }

        public ActionResult Delete(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            Index(document, "delete");
            return RedirectToAction("Index");
        }

        public ActionResult ReIndexAll()
        {
            var documents = db.Documents.ToList();
            
            var uriString = ConfigurationManager.AppSettings["SEARCHBOX_URL"];
            var searchBoxUri = new Uri(uriString);

            var settings = new ConnectionSettings(searchBoxUri);
            settings.SetDefaultIndex("sample");

            var client = new ElasticClient(settings);

            // delete index if exists at startup
            if (client.IndexExists("sample").Exists)
            {
                client.DeleteIndex("sample");
            }

            // Create a new "sample" index with default settings
            client.CreateIndex("sample", new IndexSettings());

            // Index all documents
            client.Index<Document>(documents);

            ViewBag.Message = "Reindexing all database is complete!";

            return RedirectToAction("Index");
        }

        private static void Index(Document document, String operation)
        {
            var uriString = ConfigurationManager.AppSettings["SEARCHBOX_URL"];
            var searchBoxUri = new Uri(uriString);
            
            var settings = new ConnectionSettings(searchBoxUri);
            settings.SetDefaultIndex("sample");

            var client = new ElasticClient(settings);

            if (operation.Equals("delete"))
            {
                client.DeleteById("sample", "documents", document.DocumentId);
            }
            else
            {
                client.Index(document, "sample", "documents", document.DocumentId);
            }
        }
    }
}
