using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Entities.DBSets
{
    public class RefreshToken :BaseEntity
    {
        public string token { get; set; }
        public string userId { get; set; }
        public string jwtId { get; set; }   //JTI 

        public bool isUsed { get; set; } //to make sure that the token is only once

        public bool isRevoked { get; set; } //to make sure that they are valid

        public DateTime expiryDate { get; set; }

        [ForeignKey(nameof(userId))]
        public IdentityUser User { get; set; }


    }
}
