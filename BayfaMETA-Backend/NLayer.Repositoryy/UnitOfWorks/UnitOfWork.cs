using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Repositoryy.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        void IUnitOfWork.Commit()
        {
            _context.SaveChanges(); //değişiklikleri database'e yansıt.
        }

        async Task IUnitOfWork.CommitAsync()
        {
            await _context.SaveChangesAsync(); //mümkün oldugunca asekron methodu çağır
        }
    }
}
