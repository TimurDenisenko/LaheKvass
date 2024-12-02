using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LaheKvass.Models.DB
{
    public static class DbSetExtension
    {
        public static async Task<T> ElementAtNoTrack<T>(this DbSet<T> DB, int id) where T : DBModel =>
            await DB.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? null;
        public static async Task<T> ElementAtWithTrack<T>(this DbSet<T> DB, int id) where T : DBModel =>
            await DB.FirstOrDefaultAsync(x => x.Id == id) ?? null;
    }
}