using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;

namespace NLayer.Repositoryy.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class


    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet; //readonly constructorda değer atılır daha sonra set edilemez

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) //return yok -> async Task
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable(); //AsNoTracking çekmiş olduğu dataları memory'e almasın track etmesin diye  
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); //FindAsync primary key kabul eder
        }

        public void Remove(T entity)
        {
           // _context.Entry(entity).State = EntityState.Deleted; //AYNI SEY
            _dbSet.Remove(entity); //db'den silmez sadece o entity'nin state'ini deleted olarak işaretler, flagelar
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        } 
    }
}
