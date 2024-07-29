using HealthRecordApp.DataService.Configuration;
using HealthRecordApp.DataService.IRepository;
using HealthRecordApp.DataService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private AppDBContext _context;

        private readonly ILogger _logger;

        public IUsersRepository Users { get; set; }

        public UnitOfWork(AppDBContext context, ILoggerFactory logger)
        {
            _context = context;

            _logger = logger.CreateLogger("db_logs");

            Users=new UsersRepository(context, _logger);
        }

        public async Task completeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
