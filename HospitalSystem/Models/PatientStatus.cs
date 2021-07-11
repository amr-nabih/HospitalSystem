using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Models
{
    public class PatientStatus
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Patient Arabic Name  is required")]
        public string StatusName { get; set; }
        public bool Active { get; set; } = true;
    }
}
