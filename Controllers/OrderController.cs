using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LaheKvass.Models;
using LaheKvass.Models.DB;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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
        public static async Task Cleaning(DBContext db, bool isAccount, int deletedId)
        {
            db.OrderModels.Remove(await db.OrderModels.FirstOrDefaultAsync(x => isAccount ? x.AccountId == deletedId : x.DrinkId == deletedId));
        }

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

        // GET: Order/Cart
        public async Task<ActionResult> Cart()
        {
            return View(Tuple.Create(await userCart(), await userCartSum()));
        }

        private async Task<DrinkModel[]> userCart()
        {
            int currentUserId = UserState.GetCurrentUser().Id;
            return await db.OrderModels.Where(x => x.AccountId == currentUserId).Select(x => x.DrinkId).Select(x => db.DrinkModels.FirstOrDefault(x1 => x1.Id == currentUserId)).ToArrayAsync();
        }

        private async Task<double> userCartSum()
        {
            DrinkModel[] drinks = await userCart();
            return drinks.Sum(x => x.Price);
        }

        public async Task<string> MakePayment()
        {
            double amount = await userCartSum();
            if (amount == 0)
                return "Cart is empty";
            string json = JsonSerializer.Serialize(new
            {
                api_username = "e36eb40f5ec87fa2",
                account_name = "EUR3D1",
                amount,
                order_reference = Math.Ceiling(new Random().NextDouble() * 999999),
                nonce = $"a9b7f7e7as{DateTime.Now}{new Random().NextDouble() * 999999}",
                timestamp = DateTime.Now,
                customer_url = "https://maksmine.web.app/makse"
            });

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZTM2ZWI0MGY1ZWM4N2ZhMjo3YjkxYTNiOWUxYjc0NTI0YzJlOWZjMjgyZjhhYzhjZA==");
            HttpResponseMessage response = await client.PostAsync("https://igw-demo.every-pay.com/api/v4/payments/oneoff", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return "Payment failed";
            string responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement paymentLink = jsonDoc.RootElement.GetProperty("payment_link");
            return paymentLink.ToString();
        }

        public async Task<ActionResult> ClearCart()
        {
            await Cleaning(db, true, UserState.GetCurrentUser().Id);
            await db.SaveChangesAsync();
            return RedirectToAction("Cart", await userCart());
        }

        public async Task<JsonResult> DeleteFromCart(int drinkId)
        {
            var order = await db.OrderModels.FirstOrDefaultAsync(x => x.DrinkId == drinkId);

            if (order != null)
            {
                db.OrderModels.Remove(order);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
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
