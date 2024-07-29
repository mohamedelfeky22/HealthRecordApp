using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthRecordApp.Entities.DBSets;

namespace HealthRecordApp.DataService.IRepository
{
    public interface IUsersRepository :IGenericRepository<User>
    {
        Task<User> GetUserByEmailAdress(string email);
    }

}
