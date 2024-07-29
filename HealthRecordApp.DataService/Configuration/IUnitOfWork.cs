using HealthRecordApp.DataService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.DataService.Configuration
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }

        Task completeAsync();

    }
}
