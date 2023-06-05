using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T: class
    {
        //IGenericRepository'le dönüş tipleri değişecek. Generic oldugu için şimdilik dönüş tipleri aynı bıraktık.

        Task<T> GetByIdAsync(int id);//asekron method T

        //productRepository.GetAll(x=>x.id>5).ToList();       ---DEĞİŞTİ---  tüm datayı alıyoruz.
        Task<IEnumerable<T>> GetAllAsync();

        //IQueryable ile yazdığımız sorgular direkt database'e gitmez, method çağırımı ToListAsync() ile database'e sorgu yapar.
        //productRepository.where(x=>x.id>5).OrderBy.ToListAsync();        Tipi list olsaydı order by'sız id'yi çekerdi.Memory'i alır sonra order by yapardı.
        //IQueryable ile orderby'ı da yapıp alır.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);    //T = x      bool = return type of id

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); //asekron var mı yok mu       Asekronlar varolan thread'leri daha aktif kullanmak için kullanılır.

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities); //interface alıyoruz

        //--DEĞİŞTİ-- IServicete update ve remove için asekron metodlar var. Async yapıyoruz.
        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
