
using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models
{
    public class HospitalContext :DbContext
    {
        #region Methods Context
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        #endregion

        #region Properties DbSet
        public DbSet<Users> Users { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<PatientStatus> PatientStatus { get; set; }
        #endregion



    }
}


