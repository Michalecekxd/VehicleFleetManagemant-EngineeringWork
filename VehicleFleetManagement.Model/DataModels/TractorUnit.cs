using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;

namespace VehicleFleetManagement.Model.DataModels
{
    [Display(Name = "Ciągnik siodłowy")]
    public class TractorUnit : Vehicle
    {

        [Display(Name = "Model")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Wprowadź między 1 do 20 znaków.")]
        public string Model { get; set; } = null!;

        public int? SemiTrailerId { get; set; } 
        public SemiTrailer? SemiTrailer { get; set; } 
        //public TractorUnit() : base(VehicleType.TractorUnit)
        //{
        //}
    }
}
