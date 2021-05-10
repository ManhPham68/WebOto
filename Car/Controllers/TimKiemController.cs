using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car.Models;
using PagedList;
namespace Car.Controllers
{
    public class TimKiemController : Controller
    {
        WebOtoEntities db = new WebOtoEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        /*public ActionResult KQTimKiemPartial (string sTuKhoa) 
            {
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return PartialView(lstSP.OrderBy(n=>n.DonGia));
            } */
        //Search
        [HttpGet]
        public ActionResult Search(string searchString ,int ? page)
        {
            int PageSize = 2;
            int PageNumber = (page ?? 1);
            var search = from s in db.SanPhams
                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                search = search.Where(c => c.TenSP.Contains(searchString));
            }

            return View(search.OrderBy(c=>c.DonGia).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult FormTimKiem()
        {
            return PartialView();
        }
    }
}