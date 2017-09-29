using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducationEfMVC.Models;
using EducationEfMVC.Utility;
using Api = System.Web.Http;

namespace EducationEfMVC.Controllers
{
    public class StudentsController : Controller
    {
        private EducationEfMVCContext db = new EducationEfMVCContext();

		public ActionResult List() {
			var students = db.Students.ToList();
			foreach (var student in students) {
				student.Major = db.Majors.Find(student.MajorId);
			}
			return new JsonNetResult { Data = students };
			//return new JsonNetResult { Data = db.Students.ToList() };
		}
		public ActionResult Get(int? id) {
			if (id == null) {
				return Json(new Msg { Result = "Failed", Message = "ID is null" }, 
					JsonRequestBehavior.AllowGet);
			}
			Student student = db.Students.Find(id);
			if(student == null) {
				return Json(new Msg { Result = "Failed", Message = $"Student {id} not found" }, 
					JsonRequestBehavior.AllowGet);
			}
			return new JsonNetResult { Data = student };
		}

		public ActionResult Add([Bind(Include = "Id,FirstName,LastName,SAT,GPA,Phone,Email,MajorId")] Student student) {
			if (ModelState.IsValid) {
				db.Students.Add(student);
				db.SaveChanges();
				return Json(new Msg { Result = "Success", Message = "Add Successful!" }, 
					JsonRequestBehavior.AllowGet);
			} else {
				return Json(new Msg { Result = "Failed", Message = "Model-state dictionary is invalid." },
					JsonRequestBehavior.AllowGet);
			}
		}

		#region MVC Methods

		public ActionResult Change([Bind(Include = "Id,FirstName,LastName,SAT,GPA,Phone,Email")] Student student) {
			if(student == null) {
				return Json(new Msg { Result = "Failed", Message = "Student is null" });
			}
			Student studentDb = db.Students.Find(student.Id);
			if(studentDb == null) {
				return Json(new Msg { Result = "Failed", Message = $"Student {student.Id} not found" });
			}
			studentDb.Copy(student);
			db.SaveChanges();
			return Json(new Msg { Result = "Success", Message = $"Change successful!" });
		}

		public ActionResult Grade(int? id) {
			return View(id);
		}

		// GET: Students/Print/5
		public ActionResult Print(int? id) {
			return View(id);
		}

        // GET: Students
        public ActionResult Index()
        {
			var students = db.Students.ToList();
			foreach(var student in students) {
				student.Major = db.Majors.Find(student.MajorId);
			}

            return View(students);
			// return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
			if(student.MajorId != null) {
				student.Major = db.Majors.Find(student.MajorId);
			}
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,SAT,GPA,Phone,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,SAT,GPA,Phone,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
#endregion
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
