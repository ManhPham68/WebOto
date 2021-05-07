using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car.Models;
using PagedList;

namespace Car.Controllers
{
    public class SanPhamsController : Controller
    {
        private WebOtoEntities db = new WebOtoEntities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.NhaCungCap).Include(s => s.NhaSanXuat).Include(s => s.ChiTietSP);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX");
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,HinhAnh,DonGia,LuotXem,TrangThai,MaCTSP,MaNCC,MaNSX")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai", sanPham.MaCTSP);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai", sanPham.MaCTSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,HinhAnh,DonGia,LuotXem,TrangThai,MaCTSP,MaNCC,MaNSX")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai", sanPham.MaCTSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
        //trang xem chi tiết 
        public ActionResult XemChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);

            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult DanhSachSp(int? page)
        {
            int PageSize = 2;
            int PageNumber = (page ?? 1);

            var sp = db.SanPhams.ToList();
            return View(sp.ToPagedList(PageNumber,PageSize));
        }
        public ActionResult SanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var lstsp = db.SanPhams.Where(n => n.MaNSX==id).ToList();

            if (lstsp == null)
            {
                return HttpNotFound();
            }
            return View(lstsp);
        }
    }
}
