using System.Data.Entity;
using System.Linq;

namespace LaheKvass.Models.DB
{
    public class DBContext : DbContext
    {
        public System.Data.Entity.DbSet<LaheKvass.Models.AccountModel> AccountModels { get; set; }

        public System.Data.Entity.DbSet<LaheKvass.Models.DrinkModel> DrinkModels { get; set; }
        public DBContext()
        {
            if (!AccountModels.Any(x => x.Role.Equals("Admin")))
            {
                AccountModels.Add(new AccountModel
                {
                    FirstName = "Main",
                    LastName = "Admin",
                    Gender = "Male",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    Password = "admin"
                });
                SaveChanges();
            }
        }
    }
}