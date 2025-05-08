using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;

namespace Application.Repositories
{
    // T, generic tür parametresidir. Bu interface, içinde çalışacağı veri türünü (örneğin Request, User) dışarıdan alır.
    // Bu, tekrar kullanılabilir kod yazmayı sağlar. Aynı IRepository<T> yapısı, tüm entity'lerde işe yarar.
    public interface IRepository<T> where T : BaseEntityModel //Bu bir kısıtlamadır. T yerine sadece BaseEntityModel'den türemiş sınıflar kullanılabilir demektir.
    {
        Task<T> GetByIdAsync(int ID); //Read
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity); //Create
        void Update(T entity); //Update
        void Delete(T entity); //Delete
    }
}
