using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetManagement.Model.Enums
{
    public enum DeliveryStatus
    {
        [Display(Name = "W drodze")]
        InProgress,
        [Display(Name = "Zakończona")]
        Completed
    }
}
