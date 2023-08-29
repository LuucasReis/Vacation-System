

using Vacation_API.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Vacation_API.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        internal DbSet<T> Set;

        public Repository(AppDbContext context)
        {
                _context = context;
                Set = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = Set;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var prop in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, string? includeProperties = null) 
        {
            IQueryable<T> query = Set;

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }
    }
}
