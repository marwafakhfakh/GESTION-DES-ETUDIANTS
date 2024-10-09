using GESTIONDESETUDIANTS.Models;
using GESTIONDESETUDIANTS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GESTIONDESETUDIANTS.Controllers
{
	[Authorize]
	public class SchoolController : Controller
	{

		readonly ISchoolRepository SchoolRepository;

		public SchoolController(ISchoolRepository schoolRepository)
		{
			SchoolRepository = schoolRepository;
		}

		// GET: Schoolcs
		[AllowAnonymous]
		public ActionResult Index()
		{
			return View(SchoolRepository.GetAll());
		}

		// GET: Schoolcs/Details/5
		public ActionResult Details(int id)
		{
			var school = SchoolRepository.GetById(id);
			return View(school);
		}

		// GET: Schoolcs/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Schoolcs/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(School school)
		{
			try
			{
				
					
					SchoolRepository.Add(school);

				
					return RedirectToAction(nameof(Index));
				
			
			
			}
			catch
			{
				// Gérer les erreurs éventuelles
				return View();
			}
		}

		// GET: Schoolcs/Edit/5
		// GET: Schoolcs/Edit/5
		public ActionResult Edit(int id)
		{
            var school = SchoolRepository.GetById(id);
            return View(school);
        }

		// POST: Schoolcs/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection, School school)
        {
            try
            {
                SchoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Schoolcs/Delete/5
        public ActionResult Delete(int id)
		{
			var schoolToDelete = SchoolRepository.GetById(id);

			return View(schoolToDelete);
		}

		// POST: Schoolcs/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(School s)
		{
			try
			{
				if (s != null)
				{
					// Supprimer l'employé
					SchoolRepository.Delete(s);

					// Rediriger vers la liste des employés après suppression
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return NotFound();
				}
			}
			catch
			{
				// Gérer les erreurs si quelque chose se passe mal
				return View();
			}
		}
	}
}
