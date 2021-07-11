using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "يجب ادخال الاسم بالكامل ")]
        public string FullName  { get; set; }
        [Required(ErrorMessage = "يجب ادخال كلمة المرور ")]
        public string Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "يجب ادخال البريد الالكتروني ")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Doctor { get; set; }
    }
}
