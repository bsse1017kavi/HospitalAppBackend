using HospitalApp.Models;
using HospitalApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.Controllers
{
    public class MedicineController : Controller
    {
        private readonly MedicineRepository _medicineRepository = new MedicineRepository();

        [HttpPost("api/medicine/add")]
        [Authorize]
        public IActionResult Add([FromBody] Medicine medicine)
        {
            var addedPatient = _medicineRepository.Add(medicine);
            return Ok(addedPatient);

        }

        [HttpGet("api/medicine/getAll")]
        [Authorize]
        public IActionResult GetAllMedicines()
        {
            return Ok(_medicineRepository.GetAll());
        }

        [HttpGet("api/medicine/getById")]
        public IActionResult GetById(int medicineId)
        {
            var medicine = _medicineRepository.GetById(medicineId);
            return Ok(medicine);
        }

        [HttpPost("api/medicine/update")]
        [Authorize]
        public IActionResult UpdateMedicine([FromBody] Medicine medicine)
        {
            return Ok(_medicineRepository.Update(medicine));
        }

        [HttpGet("api/medicine/delete")]
        [Authorize]
        public IActionResult DeleteMedicine(int medicineId)
        {
            var medicine = _medicineRepository.GetById(medicineId);
            _medicineRepository.Delete(medicine);
            return Ok();
        }
    }
}
