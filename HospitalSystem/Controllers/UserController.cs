using HospitalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly HospitalContext HospitalContext;
        public UserController(HospitalContext _HospitalContext)
        {
            HospitalContext = _HospitalContext;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var Users = HospitalContext.Users.ToList();
            return View(Users);
        }
        // GET: UserController/login
        public ActionResult Login()
        {
            return View();
        }
        // Post: User/login
        [HttpPost]
        public ActionResult Login(UserLogin UserLogin)
        {
            var User = HospitalContext.Users.Where(t=>t.Email== UserLogin.Email).FirstOrDefault();
            if (User == null)
            {
                ViewBag.message = "البريد الالكتروني خطا";
                return View("Login", UserLogin);
            }
            else
            {
                 
                if (User.Password != UserLogin.Password)
                {
                    ViewBag.message = "كلمة المرور خطا";
                    return View("Login", UserLogin);
                }
                else
                {
                    Service.doctor = User.Doctor;
                    return RedirectToAction(nameof(Index), "Home");
                }
               
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users User)
        {
            try
            {
                HospitalContext.Users.Add(User);
                HospitalContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = HospitalContext.Users.Where(t => t.Id == id).FirstOrDefault();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Users User)
        {
            try
            {
                User.Id = id;
                HospitalContext.Users.Remove(User);
                HospitalContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
