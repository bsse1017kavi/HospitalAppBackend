using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Repository;

namespace HospitalApp.Repository
{
    public class MedicineRepository : DatabaseRepository
    {
        public Medicine Add(Medicine medicine)
        {
            DatabaseContext.Medicines.Add(medicine);
            DatabaseContext.SaveChanges();
            return medicine;
        }

        public List<Medicine> GetAll()
        {
            return DatabaseContext.Medicines.ToList();
        }

        public Medicine GetById(int medicineId)
        {
            return DatabaseContext.Medicines.SingleOrDefault(medicine => medicine.Id == medicineId);
        }

        public Medicine Update(Medicine medicine)
        {
            DatabaseContext.Medicines.Update(medicine);
            DatabaseContext.SaveChanges();
            return medicine;
        }

        public bool Delete(Medicine medicine)
        {
            DatabaseContext.Medicines.Remove(medicine);
            DatabaseContext.SaveChanges();
            return true;
        }

        public List<Medicine> GetByPatient(List<int> ids)
        {
            //List<Medicine> allMedcines = this.GetAll();

            List<Medicine> requiredList = new List<Medicine>();

            foreach(int id in ids)
            {
                Medicine medicine = this.GetById(id);
                requiredList.Add(medicine);
            }

            return requiredList;
        }
    }
}
