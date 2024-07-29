using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Entities.DBSets
{

    public class User : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public string country { get; set; }

        public DateTime dateOfBirth { get; set; }
    }
}
