
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
namespace LaheKvass.Models
{
    public static class DbSetExtension
    {
        public static async Task<T> ElementOrDefault<T>(this DbSet<T> DB, int id) where T : DBModel =>
            await DB.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? null;
    }
}