using HospitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem
{
    public class DbSeeder
    {
        public static void Initialize(HospitalContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
