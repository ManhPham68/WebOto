using Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        WebOtoEntities _db = new WebOtoEntities();
        public ActionResult Index()
        {
            //TẠO VIEWBAG ĐỂ LẤY LIST SẢN PHẨM
            //lIST XE MỚI NHẤT
            var lstNewCar = _db.SanPhams.Where(n => n.TrangThai == true).ToList();
            //gán vào viewbag
            ViewBag.ListNewCar = lstNewCar;
            return View();
            
        }
        [ChildActionOnly]//người dùng k thể get đk 
        public ActionResult SanPhamMoi()
        {
            return PartialView();
        }
        
        public ActionResult MenuPartial()
        {
            var lstSanPham = _db.SanPhams;
            return PartialView(lstSanPham);
        }
        
    }
}