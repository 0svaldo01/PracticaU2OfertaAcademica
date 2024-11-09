using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaU2OfertaAcademica.Models;
using PracticaU2OfertaAcademica.Models.ViewModels;

namespace PracticaU2OfertaAcademica.Controllers
{
	public class HomeController : Controller
	{
		MapaCurricularContext context = new();
		public IActionResult Index()
		{
			var datos = context.Carreras.OrderBy(x => x.Nombre);
			return View(datos);
		}
		[Route("/{Carrera}/Info")]
		public IActionResult Info(string Carrera)
		{
			var datos = context.Carreras.FirstOrDefault(x => x.Nombre == Carrera.Replace("-", " "));
			if (datos == null)
			{
				return RedirectToAction("Index");
			}
			return View(datos);
		}

		[Route("/{Carrera}/Mapa")]
		public IActionResult Mapa(string Carrera)
		{
			MapaCurricularViewModel vm = new();
			var datos = context.Carreras.Include(x => x.Materias).FirstOrDefault(x => x.Nombre == Carrera.Replace("-", " "));
			if (datos != null)
			{
				vm = new MapaCurricularViewModel()
				{
					TotalCreditos = datos.Materias.Sum(x => x.Creditos),
					Carreras = datos
				};
			}
			if (datos == null)
			{
				return RedirectToAction("Index");
			}

			return View(vm);
		}

	}
}
