using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetOneOrMoreAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task MultiDelete(ICollection<T> Entities);
        Task SaveChangeAsync();
    }
}
