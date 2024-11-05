using Microsoft.AspNetCore.Mvc;
using PracticaU2OfertaAcademica.Models;

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
            var datos = context.Carreras.FirstOrDefault(x=>x.Nombre == Carrera);
            if (datos == null)
            {
                return RedirectToAction("Index");
            }
            return View(datos);
        }
    }
}
