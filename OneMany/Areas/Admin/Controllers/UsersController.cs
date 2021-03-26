using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OneMany.Models;

namespace OneMany.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            StringBuilder sb = new StringBuilder();
            ViewBag.departmentID = new SelectList(db.Departments, "Id", "dpname");
            var userData = db.Permissions.ToList();
            sb.Append("[");
            sb.Append(GetPermission(userData.Where(x => x.Pid == null).ToList()));  //find root item,  ==null IS ROOT
            sb.Append("]");
            ViewBag.tree = sb.ToString(); //tree is self defined name
            return View();
        }

        //build a tree structure
        public string GetPermission(ICollection<Permission> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var permission in list)
            {
                sb.Append("{\"id\":\"" + permission.PValue + "\",\"text\":\"" + permission.Name + "\"");
                if (permission.PermissionSon.Count > 0)
                {
                    sb.Append(",\"children\":[");
                    //recursion
                    sb.Append(GetPermission(permission.PermissionSon));
                    sb.Append("]");
                }
                // the case of no children
                sb.Append("},");
                sb.ToString().TrimEnd(',');
            }
            return sb.ToString();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordSalt = Utility.CreateSalt(); //新增時 會對每筆加鹽
                user.password = Utility.GenerateHashWithSalt(user.password, user.PasswordSalt); // 完成加密
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departmentID = new SelectList(db.Departments, "Id", "dpname", user.departmentID);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentID = new SelectList(db.Departments, "Id", "dpname", user.departmentID);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,email,name,password,tel,departmentID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departmentID = new SelectList(db.Departments, "Id", "dpname", user.departmentID);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET

        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/LoginViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Id,Account,password")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(x => x.name == loginViewModel.Account); //connect to database
                if (user == null)
                {
                    TempData["error"] = "登入失敗"; //show login error message
                    return View(loginViewModel);
                }
                string password = Utility.GenerateHashWithSalt(loginViewModel.password, user.PasswordSalt);
                //compare input and password
                if (user.password != password)
                {
                    TempData["error"] = "登入失敗"; //密碼錯誤
                    return View(loginViewModel);
                }
                return RedirectToAction("Index");
            }

            return View(loginViewModel);
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