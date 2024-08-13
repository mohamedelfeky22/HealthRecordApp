using HealthRecordApp.DataService.Data;
using HealthRecordApp.DataService.IRepository;
using HealthRecordApp.Entities.DBSets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.DataService.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDBContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
