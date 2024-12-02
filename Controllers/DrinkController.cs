using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LaheKvass.Models;
using LaheKvass.Models.DB;

namespace LaheKvass.Controllers
{
    public class DrinkController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Drink
        public async Task<ActionResult> Index()
        {
            return View(await db.DrinkModels.ToListAsync());
        }

        // GET: Drink/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkModel drinkModel = await db.DrinkModels.FindAsync(id);
            if (drinkModel == null)
            {
                return HttpNotFound();
            }
            return View(drinkModel);
        }

        // GET: Drink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drink/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DrinkName,Type,Description,Price")] DrinkModel drinkModel)
        {
            if (ModelState.IsValid)
            {
                db.DrinkModels.Add(drinkModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(drinkModel);
        }

        // GET: Drink/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkModel drinkModel = await db.DrinkModels.FindAsync(id);
            if (drinkModel == null)
            {
                return HttpNotFound();
            }
            return View(drinkModel);
        }

        // POST: Drink/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DrinkName,Type,Description,Price")] DrinkModel drinkModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drinkModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(drinkModel);
        }

        // GET: Drink/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrinkModel drinkModel = await db.DrinkModels.FindAsync(id);
            if (drinkModel == null)
            {
                return HttpNotFound();
            }
            return View(drinkModel);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DrinkModel drinkModel = await db.DrinkModels.FindAsync(id);
            db.DrinkModels.Remove(drinkModel);
            await OrderController.Cleaning(db, true, id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Store()
        {
            return View(await db.DrinkModels.ToListAsync());
        }

        public void AddToCart(int id)
        {

        }

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
