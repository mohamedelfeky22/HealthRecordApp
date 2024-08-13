using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Authentication.DTO.Incoming
{
    public class TokenRequest
    {
        [Required]
        public string token { get; set; }
        [Required]
        public string refereshToken { get; set; }
    }
}
