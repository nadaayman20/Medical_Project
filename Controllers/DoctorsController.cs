using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repo.Services;
using Doctor_Appointment.Repo;
using Microsoft.AspNetCore.Authorization;

namespace Doctor_Appointment.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly MedcareDbContext _context;

        public IDoctorRepo Doctor { get; }

        public DoctorsController(MedcareDbContext context , IDoctorRepo doctor)
        {
            _context = context;
            Doctor = doctor;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            return View(Doctor.GetAll());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorID == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(Doctor.GetById(id));
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,FullName,gender,Email,specialist,Degree,Description,Clinic_Location,Clinic_PhonNum,ReservationStartTime,ReservationEndTime,WorkDays,HomeExamination")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
               Doctor.Insert(doctor);
               return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorID,FullName,gender,Email,specialist,Degree,Description,Clinic_Location,Clinic_PhonNum,ReservationStartTime,ReservationEndTime,WorkDays,Image,HomeExamination")] Doctor doctor)
        {
            if (id != doctor.DoctorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   Doctor.Update(id, doctor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
          
            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'MedcareDbContext.Doctors'  is null.");
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                Doctor.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctors?.Any(e => e.DoctorID == id)).GetValueOrDefault();
        }
    }
}
