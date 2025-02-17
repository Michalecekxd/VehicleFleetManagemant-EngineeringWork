using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;

namespace VehicleFleetManagement.Model.DataModels
{
    [Display(Name = "Samochód dostawczy")]
    public class DeliveryVan : Vehicle
    {

        [Display(Name = "Model")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Wprowadź między 1 do 20 znaków.")]
        public string Model { get; set; } = null!;

        [Display(Name = "Typ")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        public DeliveryVanType DeliveryVanType { get; set; }

        [Display(Name = "Pojemność")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} musi być większa od 0.")]
        public double Capacity { get; set; }
        //public DeliveryVan () : base(VehicleType.DeliveryVan)
        //{
        //}
    }
}
