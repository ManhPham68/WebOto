using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.IO;
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

        // GET: SanPhams1
        [Authorize]
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.NhaCungCap).Include(s => s.NhaSanXuat).Include(s => s.ChiTietSP);
            return View(sanPhams.OrderByDescending(n=>n.MaSP).ToList());
        }

        // GET: SanPhams1/Details/5
        [Authorize]
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

        // GET: SanPhams1/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX");
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai");
            return View();
        }

        // POST: SanPhams1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,HinhAnh,DonGia,LuotXem,TrangThai,MaCTSP,MaNCC,MaNSX,HinhAnh1,HinhAnh2,HinhAnh3,HinhAnh4")] SanPham sanPham, HttpPostedFileBase[] HinhAnh)
        {


            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai", sanPham.MaCTSP);
            int loi = 0;
            for (int i = 0; i < HinhAnh.Count(); i++)
            {
                if (HinhAnh[i] != null)
                {
                    //kiem tra hinh anh ton tại chưa
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        //Kiem tra dinh dang hinh anh
                        if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/gif" && HinhAnh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "Hình Ảnh " + i + " Không Hợp Lệ <br/>";
                            loi++;
                        }
                        else {
                            var fileName = Path.GetFileName(HinhAnh[i].FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/assets/images"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload1 = "Hình Ảnh " + i + " Đã tồn Tại <br/>";
                                loi++;
                                return View(sanPham);
                            }
                            else
                            {
                                HinhAnh[i].SaveAs(path);
                                /*Session["tenHinh"] = HinhAnh.FileName;
                                ViewBag.tenHinh = "";*/
                               
                            }
                        }
                    }
                }
            }
            if (loi > 0)
            {
                return View();              
            }
            sanPham.HinhAnh = HinhAnh[0].FileName;
            sanPham.HinhAnh1 = HinhAnh[1].FileName;
            sanPham.HinhAnh2 = HinhAnh[2].FileName;
            sanPham.HinhAnh3 = HinhAnh[3].FileName;
            sanPham.HinhAnh4 = HinhAnh[4].FileName;
                 db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            
        }

        // GET: SanPhams1/Edit/5
        [Authorize]
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

        // POST: SanPhams1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,HinhAnh,DonGia,LuotXem,TrangThai,MaCTSP,MaNCC,MaNSX,HinhAnh1,HinhAnh2,HinhAnh3,HinhAnh4")] SanPham sanPham , HttpPostedFileBase[] HinhAnh)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            ViewBag.MaCTSP = new SelectList(db.ChiTietSPs, "MaCTSP", "ChungLoai", sanPham.MaCTSP);
            int loi = 0;
            for (int i = 0; i < HinhAnh.Count(); i++)
            {

                if (HinhAnh[i] != null)
                {
                    //kiem tra hinh anh ton tại chưa
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        //Kiem tra dinh dang hinh anh
                        if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/gif" && HinhAnh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "Hình Ảnh " + i + " Không Hợp Lệ <br/>";
                            loi++;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(HinhAnh[i].FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/assets/images"), fileName);
                            
                            
                                HinhAnh[i].SaveAs(path);
                               /* Session["tenHinh"] = HinhAnh.FileName;
                                ViewBag.tenHinh = "";*/
                            
                        }
                    }
                }
            }
            if (loi > 0)
            {
                return View(sanPham);
            }

            sanPham.HinhAnh = HinhAnh[0].FileName;
            sanPham.HinhAnh1 = HinhAnh[1].FileName;
            sanPham.HinhAnh2 = HinhAnh[2].FileName;
            sanPham.HinhAnh3 = HinhAnh[3].FileName;
            sanPham.HinhAnh4 = HinhAnh[4].FileName;
            db.Entry(sanPham).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: SanPhams1/Delete/5
        [Authorize]
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

        // POST: SanPhams1/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
           /* return RedirectToAction("Index");*/
            TempData["SuccessMessage"] = "Xóa Thành Công";
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
            int PageSize = 3;
            int PageNumber = (page ?? 1);

            var sp = db.SanPhams.OrderByDescending(n=>n.MaSP).ToList();
            return View(sp.ToPagedList(PageNumber,PageSize));
        }
        public ActionResult SanPham(int? id , int? page)
        {
            int PageSize = 3;
            int PageNumber = (page ?? 1);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var lstsp = db.SanPhams.Where(n => n.MaNSX==id).ToList();

            if (lstsp == null)
            {
                return HttpNotFound();
            }
            return View(lstsp.ToPagedList(PageNumber, PageSize));
        }
       /* public ActionResult Sort(int? page)
        {
            int PageSize = 2;
            int PageNumber = (page ?? 1);
            var sp = db.SanPhams.OrderByDescending(n => n.MaCTSP);
            return View(sp.ToPagedList(PageNumber, PageSize));
        }*/
        [Authorize]
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
    }
}
