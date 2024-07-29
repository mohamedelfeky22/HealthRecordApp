using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HealthRecordApp.DataService.IRepository
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAll ();
         Task<T> GetbyId (Guid id);

        Task<bool> add(T entity);
         
        Task<bool> Delete(Guid Id,Guid userId );

        //update entity or add if it does not existing 
        Task<bool> upsert(T entity);
    }
}
