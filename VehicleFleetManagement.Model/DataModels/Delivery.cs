using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;

namespace VehicleFleetManagement.Model.DataModels
{
    public class Delivery
    {
        public int Id { get; set; }   
        public DeliveryStatus Status { get; set; }

        [Display(Name = "Opis towaru")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [StringLength(100, ErrorMessage = "Maksymalnie 100 znaków.")]
        public string CargoDescription { get; set; } = null!;

        [Display(Name = "Ciężar(tony)")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "{0} musi być większy od 0.")]
        public double CargoWeight { get; set; }

        [Display(Name = "Miejsce załadunku")]
        [Required(ErrorMessage = "{0} jest wymagane.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0} musi zawierać od 1 do 20 znaków.")]
        public string LoadingLocation { get; set; } = null!;

        [Display(Name = "Miejsce rozładunku")]
        [Required(ErrorMessage = "{0} jest wymagane.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0} musi zawierać od 1 do 20 znaków.")]
        public string UnloadingLocation { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int? TractorUnitId { get; set; }
        public TractorUnit? TractorUnit { get; set; }
        public int? SemiTrailerId { get; set; }
        public SemiTrailer? SemiTrailer { get; set; }
        public int? RigidTruckId { get; set; }
        public RigidTruck? RigidTruck { get; set; }
        public int? DeliveryVanId { get; set; }
        public DeliveryVan? DeliveryVan { get; set; }
    }
}