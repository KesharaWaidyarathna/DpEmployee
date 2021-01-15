using DpEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DpEmployee.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext db)
        {
            _db = db;

        }

        [BindProperty]
        public Employee Employee { get; set; }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? code)
        {
            Employee = new Employee();

            if (code == null)
            {
                return View(Employee);
            }

            Employee = _db.Employee.FirstOrDefault(x => x.Code == code);

            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);

        }

        public IActionResult View(int code)
        {
            Employee = _db.Employee.FirstOrDefault(x => x.Code == code);

            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);

        }


        #region Api calls
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Employee.Code == 0)
                {
                    _db.Employee.Add(Employee);
                }
                else
                {
                    _db.Employee.Update(Employee);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Employee);

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Employee.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int code)
        {
            var employeefromDb = await _db.Employee.FirstOrDefaultAsync(x => x.Code == code);
            if (employeefromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _db.Employee.Remove(employeefromDb);

            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}

