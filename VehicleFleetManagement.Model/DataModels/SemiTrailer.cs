using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;

namespace VehicleFleetManagement.Model.DataModels
{
    public class SemiTrailer : Vehicle
    {
        [Display(Name = "Typ")]
        [Required(ErrorMessage = "{0} jest wymagany.")] 
        public SemiTrailerType SemiTrailerType { get; set; }

        [Display(Name = "Pojemność")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} musi być większa od 0.")] 
        public double Capacity { get; set; }
        //public SemiTrailer() : base(VehicleType.SemiTrailer)
        //{
        //}
    }
}
