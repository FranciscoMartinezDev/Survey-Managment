using Microsoft.EntityFrameworkCore;
using Survey_DataEntry;
using Survey_Repository.Interfaces;
using System.Linq.Expressions;

namespace Survey_Repository.Implementation
{
    /// <summary>
    ///  Los accesos de datos envueltos en una clase base de repositorio.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private SurveyDbContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository(SurveyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<ICollection<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<ICollection<T>> GetOneOrMoreAsync(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).ToList();
        public async Task AddAsync(T Entity) => await _dbSet.AddRangeAsync(Entity);
        public async Task MultiAddAsync(ICollection<T> Entity) => await _dbSet.AddRangeAsync(Entity);
        public async Task UpdateAsync(T Entity) => _dbSet.UpdateRange(Entity);
        public async Task MultiDelete(ICollection<T> Entities) =>  _dbSet.RemoveRange(Entities);

        public async Task SaveChangeAsync() => await _context.SaveChangesAsync();
    }
}
