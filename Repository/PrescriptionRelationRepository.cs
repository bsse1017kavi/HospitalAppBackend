using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Repository;

namespace HospitalApp.Repository
{
    public class PrescriptionRelationRepository : DatabaseRepository
    {
        public PrescriptionRelation Add(PrescriptionRelation prescriptionRelation)
        {
            DatabaseContext.PrescriptionRelations.Add(prescriptionRelation);
            DatabaseContext.SaveChanges();
            return prescriptionRelation;
        }

        public List<PrescriptionRelation> GetAll()
        {
            return DatabaseContext.PrescriptionRelations.ToList();
        }

        public List<int> GetMedicineIds(int patientId)
        {
            List<PrescriptionRelation> allRelations = this.GetAll();

            List<int> requiredList = new List<int>();

            foreach(PrescriptionRelation relation in allRelations)
            {
                if(relation.PatientId==patientId)
                {
                    requiredList.Add(relation.MedicineId);
                }
            }

            return requiredList;
        }

        public bool DeletePatientMedicineRelation(int patientId, int medicineId)
        {
            List<PrescriptionRelation> allRelations = this.GetAll();

            foreach (PrescriptionRelation relation in allRelations)
            {
                if (relation.PatientId == patientId && relation.MedicineId==medicineId)
                {
                    DatabaseContext.PrescriptionRelations.Remove(relation);
                    DatabaseContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
