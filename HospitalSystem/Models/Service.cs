using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Models
{
    public class Service:IService
    {
        public static bool doctor { get; set; }
        public int number { get; set; } = 0;
    }
}
