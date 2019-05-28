using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6.ViewModels
{
    public class TreatmentsViewModel
    {
        public int TreatmentID { get; set; }
        public string DiseaseName { get; set; }
        public string MedicineName { get; set; }
        public string TreatmentDate { get; set; }
        public int? DiseaseID { get; set; }
        public int? MedicineID { get; set; }
    }
}
