using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LaheKvass.Models;
using LaheKvass.Models.DB;
using System;
using System.Linq;

namespace LaheKvass.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Account
        public async Task<ActionResult> Index()
        {
            return View(await db.AccountModels.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = await db.AccountModels.FindAsync(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Gender,Email,Password,Role")] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                db.AccountModels.Add(accountModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(accountModel);
        }

        // GET: Account/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = await db.AccountModels.FindAsync(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Gender,Email,Password,Role")] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(accountModel);
        }

        // GET: Account/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = await db.AccountModels.FindAsync(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AccountModel accountModel = await db.AccountModels.FindAsync(id);
            db.AccountModels.Remove(accountModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,FirstName,LastName,Gender,Email,Password,Role")] AccountModel accountModel)
        {
            try
            {
                if (db.AccountModels.Where(x => x.Email == accountModel.Email).Count() == 0)
                {
                    accountModel.Role = "User";
                    db.AccountModels.Add(accountModel);
                    UserState.SetCurrentUser(accountModel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception) { }
            return View(accountModel);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "Email, Password")] AccountModel accountModel)
        {
            try
            {
                AccountModel reAcc = await db.AccountModels.Where(x => x.Email == accountModel.Email).FirstOrDefaultAsync();
                if (reAcc?.Password == accountModel?.Password)
                {
                    UserState.IsAuthorized(true);   
                    UserState.SetCurrentUser(reAcc);
                    return RedirectToAction("Introduction", "Book");
                }
            }
            catch { }
            return View(accountModel);
        }

        public ActionResult Logout()
        {
            UserState.IsAuthorized(false);
            UserState.SetCurrentUser(null);
            return RedirectToAction("Introduction", "Book");
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
