using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;

namespace Application.Repositories
{
    public interface IRepository<T> where T : BaseEntityModel
    {
        Task<T> GetByIdAsync(int id); //Read
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity); //Create
        void Update(T entity); //Update
        void Delete(T entity); //Delete
    }
}
