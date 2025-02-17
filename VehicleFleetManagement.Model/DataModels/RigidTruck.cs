using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;

namespace VehicleFleetManagement.Model.DataModels
{
    [Display(Name = "Ciężarówka sztywna")]
    public class RigidTruck : Vehicle
    {
        [Display(Name = "Model")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Wprowadź między 1 do 50 znaków.")]
        public string Model { get; set; } = null!;

        [Display(Name = "Typ")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public RigidTruckType RigidTruckType { get; set; }

        [Display(Name = "Pojemność")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} musi być większa od 0.")]
        public double Capacity { get; set; }
        //public RigidTruck() : base(VehicleType.RigidTruck)
        //{
        //}
    }
}
