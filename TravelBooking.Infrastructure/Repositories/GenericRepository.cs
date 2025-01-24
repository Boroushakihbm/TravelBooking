
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelBooking.Domain.IRepository;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.Infrastructure.mssql.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class, IRootEntity
    {
        protected readonly TravelBookingDbContext _context;
        protected readonly DbSet<T> _contextDb;
        public IMapper _mapper { get; private set; }
        private bool _isDisposed;

        public GenericRepository(TravelBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _contextDb = _context.Set<T>();
        }
        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
           return await _context.Set<T>().FindAsync([id], cancellationToken: cancellationToken);
        }
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, "Entity");

            if (_context == null || _isDisposed)
                throw new NullReferenceException("DBContext is Null");

            _context.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, "Entity");

            if (_context == null || _isDisposed)
                throw new NullReferenceException("DBContext is Null");

            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>().FindAsync([id], cancellationToken: cancellationToken);

            ArgumentNullException.ThrowIfNull(entity, "Entity");

            if (_context == null || _isDisposed)
                throw new NullReferenceException("DBContext is Null");

            _context.Set<T>().Entry(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
            _isDisposed = true;
        }
    }
}
