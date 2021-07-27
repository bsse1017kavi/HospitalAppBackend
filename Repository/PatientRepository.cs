using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Repository;

namespace HospitalApp.Repository
{
    public class PatientRepository : DatabaseRepository
    {
        public Patient Add(Patient patient)
        {
            DatabaseContext.Patients.Add(patient);
            DatabaseContext.SaveChanges();
            return patient;
        }

        public List<Patient> GetAll()
        {
            return DatabaseContext.Patients.ToList();
        }

        public Patient GetById(int patientId)
        {
            return DatabaseContext.Patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        public Patient Update(Patient patient)
        {
            DatabaseContext.Patients.Update(patient);
            DatabaseContext.SaveChanges();
            return patient;
        }

        public bool Delete(Patient patient)
        {
            DatabaseContext.Patients.Remove(patient);
            DatabaseContext.SaveChanges();
            return true;
        }

        public List<Patient> GetByAge(int age)
        {
            //return DatabaseContext.Patients.Select(patient => patient.Age == age).ToList();
            List<Patient> allPatients = this.GetAll();

            List<Patient> requiredList = new List<Patient>();

            foreach(Patient patient in allPatients)
            {
                if(patient.Age==age)
                {
                    requiredList.Add(patient);
                }
            }

            return requiredList;
        }
    }
}
