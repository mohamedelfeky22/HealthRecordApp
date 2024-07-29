using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Entities.DBSets
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int status { get; set; } = 1;
        public DateTime addedDate { get; set; } = DateTime.UtcNow;
        public DateTime updatedDate { get; set; }
    }
}
