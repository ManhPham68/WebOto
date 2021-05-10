using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Car.Models;

namespace Car.Controllers
{
    public class AdminController : Controller
    {
        WebOtoEntities db = new WebOtoEntities();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login (string txtTaiKhoan, string txtMatKhau)
        {
            Admin tv = db.Admins.SingleOrDefault(n => n.User == txtTaiKhoan && n.PassWord == txtMatKhau);
            if (tv != null)
            {
                //Truy cập lấy ra tất cả quyền của thành viên đó
                IEnumerable<ThanhVien_Quyen> lstQuyen = db.ThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.Quyen.TenQuyen + ","; //Lấy quyền trong bảng chi tiết quyền và loại thành viên

}
                Quyen = Quyen.Substring(0, Quyen.Length - 1); //Cắt đi dấu , cuối cùng (Chuỗi sau khi cắt: 

               PhanQuyen(txtTaiKhoan, Quyen); //Xử lý phương thức phân quyền
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
            TaiKhoan, //user
            DateTime.Now, //begin
            DateTime.Now.AddHours(3), //timeout
            false, //remember?
            Quyen, // permission.. "admin" or for more than one
            FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        //Tạo Trang Ngăn Quyền Truuy Cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
    }
}