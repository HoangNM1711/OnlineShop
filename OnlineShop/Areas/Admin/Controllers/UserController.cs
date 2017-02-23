using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1,int pageSize = 10)
        {
            var model = new UserDAO().ListAll(page,pageSize);
                
            return View(model);
        }

        [HttpGet]
        public ActionResult Create ()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var encryptor = Encryptor.MD5Hash(user.Password);
                user.Password = encryptor;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }

            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            var user = new UserDAO().ViewById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if(string.IsNullOrEmpty(user.Password))
                {
                    var encryptor = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptor;
                }

                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return View("Index");
        }
    }
}