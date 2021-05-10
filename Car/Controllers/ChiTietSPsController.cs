using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car.Models;

namespace Car.Controllers
{
    [Authorize]
    public class ChiTietSPsController : Controller
    {
    
        private WebOtoEntities db = new WebOtoEntities();

        // GET: ChiTietSPs
        public ActionResult Index()
        {
            
            return View(db.ChiTietSPs.OrderByDescending(n=>n.MaCTSP).ToList());
        }
        // GET: ChiTietSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSP chiTietSP = db.ChiTietSPs.Find(id);
            if (chiTietSP == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSP);
        }

        // GET: ChiTietSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChiTietSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaCTSP,ChungLoai,XuatXu,NamSanXuat,KichThuoc,ChieuDaiCoSo,TuTrong,DungTichXiLanh,KieuDongCo,HopSo,CongSuatLonNhat,Momen,TocDoToiDa,NhienLieu,KieuDanDong,SoChoNgoi,TieuThu,CoLop,MoTa")] ChiTietSP chiTietSP)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietSPs.Add(chiTietSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chiTietSP);
        }

        // GET: ChiTietSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSP chiTietSP = db.ChiTietSPs.Find(id);
            if (chiTietSP == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSP);
        }

        // POST: ChiTietSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaCTSP,ChungLoai,XuatXu,NamSanXuat,KichThuoc,ChieuDaiCoSo,TuTrong,DungTichXiLanh,KieuDongCo,HopSo,CongSuatLonNhat,Momen,TocDoToiDa,NhienLieu,KieuDanDong,SoChoNgoi,TieuThu,CoLop,MoTa")] ChiTietSP chiTietSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chiTietSP);
        }

        // GET: ChiTietSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSP chiTietSP = db.ChiTietSPs.Find(id);
            if (chiTietSP == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSP);
        }

        // POST: ChiTietSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietSP chiTietSP = db.ChiTietSPs.Find(id);
            db.ChiTietSPs.Remove(chiTietSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
