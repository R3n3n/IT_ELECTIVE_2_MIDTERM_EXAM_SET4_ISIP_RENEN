using ComputerLaboratoryUsageMonitoringSystem.Models;
using ComputerLaboratoryUsageMonitoringSystem.Models.DTOs;
using ComputerLaboratoryUsageMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerLaboratoryUsageMonitoringSystem.Controllers
{
    [Authorize]
    public class LaboratorySessionController : Controller
    {
        private readonly ILaboratorySessionRepository _sessionRepository;

        public LaboratorySessionController(
            ILaboratorySessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public IActionResult Index(string? search)
        {
            var sessions = _sessionRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                sessions = sessions
                    .Where(s =>
                        s.SessionNumber.ToString()
                            .Contains(search, StringComparison.OrdinalIgnoreCase)
                        ||
                        s.StudentNumber.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        s.FirstName.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        s.LastName.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        s.Course.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        s.ComputerNumber.ToString()
                            .Contains(search, StringComparison.OrdinalIgnoreCase)
                        ||
                        s.Status.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                    .ToList();
            }

            ViewBag.Search = search;

            return View(sessions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new LaboratorySessionDto
            {
                TimeIn = DateTime.Now
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LaboratorySessionDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var computerInUse = _sessionRepository
                .GetAll()
                .Any(s =>
                    s.ComputerNumber == model.ComputerNumber &&
                    s.Status == "Using"
                );

            if (computerInUse)
            {
                ModelState.AddModelError(
                    "ComputerNumber",
                    "This computer is currently in use. Please select another computer."
                );

                return View(model);
            }

            var session = new LaboratorySession
            {
                StudentNumber = model.StudentNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Course = model.Course,
                YearLevel = model.YearLevel,
                ComputerNumber = model.ComputerNumber,
                Purpose = model.Purpose,
                TimeIn = model.TimeIn == default
                    ? DateTime.Now
                    : model.TimeIn,
                TimeOut = null,
                Status = "Using",
                Notes = model.Notes
            };

            _sessionRepository.Add(session);

            TempData["SuccessMessage"] =
                "Laboratory session registered successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            var model = new LaboratorySessionDto
            {
                StudentNumber = session.StudentNumber,
                FirstName = session.FirstName,
                LastName = session.LastName,
                Course = session.Course,
                YearLevel = session.YearLevel,
                ComputerNumber = session.ComputerNumber,
                Purpose = session.Purpose,
                TimeIn = session.TimeIn,
                Notes = session.Notes
            };

            ViewBag.SessionNumber = session.SessionNumber;
            ViewBag.Status = session.Status;
            ViewBag.TimeOut = session.TimeOut;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LaboratorySessionDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            var computerInUse = _sessionRepository
                .GetAll()
                .Any(s =>
                    s.Id != id &&
                    s.ComputerNumber == model.ComputerNumber &&
                    s.Status == "Using"
                );

            if (computerInUse)
            {
                ModelState.AddModelError(
                    "ComputerNumber",
                    "This computer is currently in use. Please select another computer."
                );

                ViewBag.SessionNumber = session.SessionNumber;
                ViewBag.Status = session.Status;
                ViewBag.TimeOut = session.TimeOut;

                return View(model);
            }

            session.StudentNumber = model.StudentNumber;
            session.FirstName = model.FirstName;
            session.LastName = model.LastName;
            session.Course = model.Course;
            session.YearLevel = model.YearLevel;
            session.ComputerNumber = model.ComputerNumber;
            session.Purpose = model.Purpose;
            session.TimeIn = model.TimeIn;
            session.Notes = model.Notes;

            _sessionRepository.Update(session);

            TempData["SuccessMessage"] =
                "Laboratory session updated successfully.";

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult TimeOut(int id)
        {
            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TimeOut(
            int id,
            string? notes)
        {
            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            session.TimeOut = DateTime.Now;
            session.Status = "Finished";

            if (!string.IsNullOrWhiteSpace(notes))
            {
                session.Notes = notes;
            }

            _sessionRepository.Update(session);

            TempData["SuccessMessage"] =
                "Student time out recorded successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var session = _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            _sessionRepository.Delete(id);

            TempData["SuccessMessage"] =
                "Laboratory session deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}