using HospitalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalContext HospitalContext;
        private IService Service;
        
        public PatientController(HospitalContext _HospitalContext, IService serv)
        {
            HospitalContext = _HospitalContext;
            Service = serv;
        }
        // GET: PatientController
        public ActionResult Index(int Patientid)
        {
            var Patients = HospitalContext.Patients.Where(t =>  t.id== Patientid).FirstOrDefault();
            return View(Patients);
        }
        public ActionResult patientStatus()
        {
            ViewBag.FillPatientStatus = HospitalContext.PatientStatus.Where(e => e.Active == true).Select(t => new SelectListItem { Value = t.id.ToString(), Text = t.StatusName }).ToList();

            var Patients = HospitalContext.Patients.Where(t=>t.active == true).ToList();
            return View(Patients);
        }
        
        public async Task<ActionResult> ShowStatus()
        {
            ViewBag.FillPatientStatus = HospitalContext.PatientStatus.Where(e => e.Active == true).Select(t => new SelectListItem { Value = t.id.ToString(), Text = t.StatusName }).ToList();
            var Patients = HospitalContext.Patients.Where(e => e.PatientDate.Day == DateTime.Now.Date.Day && e.active == true).Skip(Service.number).Take(10).ToList();
           if(Patients.Count==0)
            {
                 Patients = HospitalContext.Patients.Where(e => e.PatientDate.Day == DateTime.Now.Date.Day && e.active == true).Take(10).ToList();
            }
            ViewBag.number = Service.number;
            var PatientsNum = HospitalContext.Patients.Where(e => e.PatientDate.Day == DateTime.Now.Date.Day && e.active == true).ToList();
            ViewBag.Patientsnumber = PatientsNum.Count();
            //    await Task.Run(() => CallShowStatus(number));
           // return RedirectToAction("ShowStatus", "Patient", number = number);
            return View("ShowStatus", Patients);

        }
        public ActionResult CallShow(int number)
        {
            Service.number = number;
            return Json("Success");
        }
            public async Task CallShowStatus(int number)
        {
            var Patients = await HospitalContext.Patients.Where(e => e.PatientDate.Day == DateTime.Now.Date.Day&&e.active==true).ToListAsync();
            var countPatients = Patients.Count();
            countPatients = countPatients - number;
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                int count = 0;
                if (countPatients > 0)
                {
                    if (countPatients >= 10)
                    {
                        if (countPatients == 10)
                        {
                            count += countPatients;
                        }
                        else
                        {
                            countPatients = countPatients - count;
                            count += 10;
                        }

                    }
                    else
                    {
                        if (countPatients > 0)
                        {
                            count = countPatients;
                        }
                    }
                }
                ShowStatus();
            }, null, startTimeSpan, periodTimeSpan);
        }

        [HttpPost]
        public ActionResult UpdatePatientStatus(int id, int PatientStatusId, string PatientDate)
        {
            string patientDate = PatientDate.Replace("-", "");
            DateTime PDate = DateTime.Parse(patientDate);

            // Exception: String was not recognized as a valid DateTime because the day of week was incorrect.  
            //  DateTime PDate = DateTime.ParseExact(PatientDate, "mm/dd/yyyy", provider);
            // DateTime PDate = DateTime.Parse(PatientDate);
            var Patient = HospitalContext.Patients.Where(e => e.id == id).FirstOrDefault();
            Patient.PatientStatusId = PatientStatusId;
            Patient.PatientDate = PDate;
            HospitalContext.Patients.Update(Patient);
            HospitalContext.SaveChanges();
            ViewBag.FillPatientStatus = HospitalContext.PatientStatus.Where(e => e.Active == true).Select(t => new SelectListItem { Value = t.id.ToString(), Text = t.StatusName }).ToList();

            var Patients = HospitalContext.Patients.ToList();
            //    return Ok("success");
            return View("patientStatus", Patients);
        }
        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patients Patient,bool PatientActive,bool Gender)
        {
            try
            {
                Patient.active = PatientActive;
                Patient.Gender = Gender;
                var getPatient = HospitalContext.Patients.Where(t => t.id == Patient.id).FirstOrDefault();
                if(getPatient == null)
                { 
                string patientDate = Patient.PtDate.Replace("-", "");
                DateTime PDate = DateTime.Parse(patientDate);
                Patient.PatientDate = PDate;
                HospitalContext.Patients.Add(Patient);
                HospitalContext.SaveChanges();

                ViewBag.message = "تم تسجيل المريض بنجاح";
                return RedirectToAction(nameof(patientStatus));
                }
                else
                {
                    string patientDate = Patient.PtDate.Replace("-", "");
                    DateTime PDate = DateTime.Parse(patientDate);
                    getPatient.PatientDate = PDate;
                    getPatient.IdentityID = Patient.IdentityID;
                    getPatient.PatientCode = Patient.PatientCode;
                    getPatient.DoctorName = Patient.DoctorName;
                    getPatient.age = Patient.age;
                    getPatient.active = Patient.active;
                    getPatient.Gender = Patient.Gender;
                    getPatient.Note = Patient.Note;
                    getPatient.PhoneNumber = Patient.PhoneNumber;
                    HospitalContext.Patients.Update(getPatient);
                    HospitalContext.SaveChanges();

                    ViewBag.message = "تم تعديل المريض بنجاح";
                    return RedirectToAction(nameof(patientStatus));
                }
            }
            catch(Exception ex)
            {
                ViewBag.message = "فشلت في تسجيل المريض حاول مرة اخري";

                return View("Index", Patient);
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
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

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
