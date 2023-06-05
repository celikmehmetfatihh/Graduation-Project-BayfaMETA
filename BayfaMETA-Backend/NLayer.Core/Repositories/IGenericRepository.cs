using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);//asekron method T

        //productRepository.GetAll(x=>x.id>5).ToList();
        IQueryable<T> GetAll();

        //IQueryable ile yazdığımız sorgular direkt database'e gitmez, method çağırımı ToListAsync() ile database'e sorgu yapar.
        //productRepository.where(x=>x.id>5).OrderBy.ToListAsync();        Tipi list olsaydı order by'sız id'yi çekerdi.Memory'i alır sonra order by yapardı.
        //IQueryable ile orderby'ı da yapıp alır.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);    //T = x      bool = return type of id

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); //asekron var mı yok mu       Asekronlar varolan thread'leri daha aktif kullanmak için kullanılır.

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities); //interface alıyoruz

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
