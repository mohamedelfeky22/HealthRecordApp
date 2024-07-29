using HealthRecordApp.DataService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthRecordApp.Entities.DBSets;
using HealthRecordApp.DataService.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HealthRecordApp.DataService.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(AppDBContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _dbSet.Where(u => u.status == 1).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo GetAll method has generated error in UserRepository");
                return new List<User>();
            }

        }

        public async Task<User> GetUserByEmailAdress(string email)
        {
            try
            {
                return await _dbSet.Where(u => u.email == email).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo GetUserByEmailAdress method has generated error in UserRepository");
                return new User();
            }
        }
    }
}