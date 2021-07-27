using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class DoctorRepository: DatabaseRepository
    {
        public Doctor Add(Doctor doctor)
        {
            DatabaseContext.Doctors.Add(doctor);
            DatabaseContext.SaveChanges();
            return doctor;
        }

        public List<Doctor> GetAll()
        {
            return DatabaseContext.Doctors.ToList();
        }

        public Doctor GetById(int doctorId)
        {
            return DatabaseContext.Doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        public Doctor Update(Doctor doctor)
        {
            DatabaseContext.Doctors.Update(doctor);
            DatabaseContext.SaveChanges();
            return doctor;
        }

        public bool Delete(Doctor doctor)
        {
            DatabaseContext.Doctors.Remove(doctor);
            DatabaseContext.SaveChanges();
            return true;
        }

        public bool SignIn(String username, String password)
        {
            foreach(Doctor doctor in DatabaseContext.Doctors)
            {
                if(doctor.Name == username && doctor.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
