using lab6.Data;
using lab6.Models;
using lab6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace lab6.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private Context _context;
        public ValuesController(Context context)
        {
            _context = context;
        }

       //  GET api/values
        [HttpGet]
        [Produces("application/json")]
        public List<TreatmentsViewModel> Get()
        {
            var list = _context.Treatments.Include(s => s.Disease).Include(e => e.Medicine).Select(o =>
                new TreatmentsViewModel
                {
                    TreatmentID = o.TreatmentID,
                    DiseaseName = o.Disease.DiseaseName,
                    MedicineName = o.Medicine.MedicineName,
                    TreatmentDate = o.TreatmentDate,
                    DiseaseID = o.DiseaseID,
                    MedicineID = o.MedicineID
                });
            return list.OrderBy(t => t.TreatmentID).Take(20).ToList();
        }

        [HttpGet("diseases")]
        [Produces("application/json")]
        public IEnumerable<Disease> GetDiseases()
        {
            return _context.Diseases.ToList();
        }

        [HttpGet("patients")]
        [Produces("application/json")]
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        [HttpGet("medicines")]
        [Produces("application/json")]
        public IEnumerable<Medicine> GetMedicines()
        {
            return _context.Medicines.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Treatment treatment = _context.Treatments.FirstOrDefault(x => x.TreatmentID == id);
            if (treatment == null)
                return NotFound();
            return new ObjectResult(treatment);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Treatment treatment)
        {
            if (treatment == null)
                return BadRequest();

            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            return Ok(treatment);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Treatment treatment)
        {
            if (treatment == null)
                return BadRequest();
            if (!_context.Treatments.Any(x => x.TreatmentID == treatment.TreatmentID))
            {
                return NotFound();
            }

            _context.Update(treatment);
            _context.SaveChanges();

            return Ok(treatment);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Treatment treatment = _context.Treatments.FirstOrDefault(x => x.TreatmentID == id);
            if (treatment == null)
            {
                return NotFound();
            }
            _context.Treatments.Remove(treatment);
            _context.SaveChanges();
            return Ok(treatment);
        }
    }
}
