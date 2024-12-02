using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LaheKvass.Models;
using LaheKvass.Models.DB;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LaheKvass.Controllers
{
    public class OrderController : Controller
    {
        private DBContext db = new DBContext();

        private Dictionary<int, Tuple<AccountModel, DrinkModel>> orders = new Dictionary<int, Tuple<AccountModel, DrinkModel>>();
        public static Tuple<SelectList, SelectList> onlyOrders;

        private async Task UpdateOrders()
        {
            foreach (OrderModel order in db.OrderModels)
            {
                orders.Add(order.Id, Tuple.Create(await db.AccountModels.ElementAtWithTrack(order.AccountId), await db.DrinkModels.ElementAtWithTrack(order.DrinkId)));
            }
        }
        private void UpdateOnlyOrders() => 
            onlyOrders = Tuple.Create(
                new SelectList(
                    db.AccountModels
                        .ToList()
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = $"{a.FirstName} {a.LastName}"
                        }),
                    "Value",
                    "Text"
                ),
                new SelectList(
                    db.DrinkModels
                        .ToList()
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = $"{a.Type} | {a.DrinkName}"
                        }),
                    "Value",
                    "Text"
                )
            );


        // GET: Order
        public async Task<ActionResult> Index()
        {
            await UpdateOrders();
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = await db.OrderModels.FindAsync(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            UpdateOnlyOrders();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AccountId,DrinkId")] OrderModel orderModel)
        {
            UpdateOnlyOrders();
            if (ModelState.IsValid)
            {
                db.OrderModels.Add(orderModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(orderModel);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = await db.OrderModels.FindAsync(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderModel orderModel = await db.OrderModels.FindAsync(id);
            db.OrderModels.Remove(orderModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
