using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Models
{
    public class Patients
    {
        [Key]
        public int id { get; set; }
       [Required(ErrorMessage = "يجب إدخال كود المريض")]
        public string PatientCode { get; set; }
       [Required(ErrorMessage = "يجب إدخال إسم المريض باللغة العربية")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "يجب إدخال رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب إدخال رقم البطاقة")]
        public string IdentityID { get; set; }
        [Required(ErrorMessage = "يجب إدخال عمر المريض")]
        public int age { get; set; }
        [Required(ErrorMessage = "يجب إدخال نوع المريض")]
        public bool Gender { get; set; }
       
        public DateTime PatientDate { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "يجب إدخال التاريخ ")]
        public string PtDate { get; set; }
        public string DoctorName { get; set; }
        public string Note { get; set; }
        public bool active { get; set; }

        public int PatientStatusId { get; set; } = 6;
    }
}
