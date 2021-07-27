using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class DatabaseContext : DbContext
    {
        private const String ConnectionString = @"Server=DESKTOP-8PD3BAA\MSSQLSERVER01;Database=Hospital;Trusted_Connection=true;";

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<PrescriptionRelation> PrescriptionRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
