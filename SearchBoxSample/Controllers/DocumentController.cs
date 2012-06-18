using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nest;
using SearchBoxSample.Models;
using System.Configuration;

namespace SearchBoxSample.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Documents/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string q)
        {
            var result = ElasticClient.Search<Document>(body =>
                // return first 5 results, default is 10
                body.Size(5).Query(query =>
                    query.QueryString(qs => qs.Query(q))));

            ViewBag.Query = q;
            ViewBag.Total = result.Total;
            return View("SearchResult", result.Documents.ToList());
        }
        private static ElasticClient ElasticClient
        {
            get
            {
                try
                {
                    var uriString = ConfigurationManager.AppSettings["SEARCHBOX_URL"];
                    var searchBoxUri = new Uri(uriString);
                    var settings = new ConnectionSettings(searchBoxUri);
                    settings.SetDefaultIndex("sample");
                    return new ElasticClient(settings);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
