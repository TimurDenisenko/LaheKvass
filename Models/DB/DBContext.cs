using System.Data.Entity;

namespace LaheKvass.Models.DB
{
    public class DBContext : DbContext
    {
        public System.Data.Entity.DbSet<LaheKvass.Models.AccountModel> AccountModels { get; set; }
    }
}