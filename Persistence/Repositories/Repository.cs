using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.EntitiyModels.BaseEntitityModels;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntityModel
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdAsync(int ID)
        {
            try
            {
                return await _dbSet.FindAsync(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(T entity) // t nin anlamı type yani ben sana string verebilirim yani verilen değere göre şekillenir hiçbir şey dönmezse dönmez.
        {
            try
            {
                entity.LastModifiedOn = DateTime.Now;
                _dbSet.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(T entity)
        {
            try
            {
                entity.ObjectStatus = ObjectStatus.Deleted;
                entity.Status = Status.Passive;
                _dbSet.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
