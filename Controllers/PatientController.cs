using Microsoft.AspNetCore.Http;
using HospitalApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientRepository _patientRepository = new PatientRepository();

        private readonly MedicineRepository _medicineRepository = new MedicineRepository();

        private readonly PrescriptionRelationRepository _prescriptionRelationRepository = new PrescriptionRelationRepository();

        [HttpPost("api/patient/add")]
        [Authorize]
        public IActionResult Add([FromBody] Patient patient)
        {
            var addedPatient = _patientRepository.Add(patient);
            return Ok(addedPatient);

        }

        [HttpGet("api/patient/getAll")]
        [Authorize]
        public IActionResult GetAllPatients()
        {
            //if (HttpContext.Session.GetString("isLoggedIn") == "true")
            //{
            //    return Ok(_patientRepository.GetAll());
            //}

            //String value = HttpContext.Session.GetString("isLoggedIn");

            //Console.WriteLine(value);

            //return Ok("Sorry, you're not logged in");

            return Ok(_patientRepository.GetAll());
        }

        [HttpGet("api/patient/getById")]
        public IActionResult GetById(int patientId)
        {
            var patient = _patientRepository.GetById(patientId);
            return Ok(patient);
        }

        [HttpPost("api/patient/update")]
        [Authorize]
        public IActionResult UpdatePatient([FromBody] Patient patient)
        {
            return Ok(_patientRepository.Update(patient));
        }

        [HttpGet("api/patient/delete")]
        [Authorize]
        public IActionResult DeletePatient(int patientId)
        {
            var patient = _patientRepository.GetById(patientId);
            _patientRepository.Delete(patient);
            return Ok();
        }

        [HttpGet("api/patient/getByAge")]
        public IActionResult GetByAge(int age)
        {
            var patients = _patientRepository.GetByAge(age);
            return Ok(patients);
        }

        [HttpPost("api/prescription/add")]
        [Authorize]
        public IActionResult AddPrescription([FromBody] PrescriptionRelation prescription)
        {
            var addedPrescription = _prescriptionRelationRepository.Add(prescription);
            return Ok(addedPrescription);

        }

        [HttpGet("api/prescription/delete")]
        [Authorize]
        public IActionResult DeletePrescription(int patientId, int medicineId)
        {
            _prescriptionRelationRepository.DeletePatientMedicineRelation(patientId, medicineId);
            return Ok();

        }

        [HttpGet("api/patient/getMedicines")]
        [Authorize]
        public IActionResult GetMedicineById(int patientId)
        {
            List<int> medicineIds = _prescriptionRelationRepository.GetMedicineIds(patientId);

            List<Medicine> medicines = _medicineRepository.GetByPatient(medicineIds);

            return Ok(medicines);
        }
    }
}
