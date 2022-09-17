using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Controllers
{
    public class SlidesController : Controller
    {
        // GET: SlidesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SlidesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: SlidesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SlidesController/Edit/5
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

        // GET: SlidesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SlidesController/Delete/5
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
