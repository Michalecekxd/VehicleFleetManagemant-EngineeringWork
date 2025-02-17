using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetManagement.Model.DataModels
{
    public class Driver
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "{0} jest wymagane.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Wprowadź między 2 do 20 znaków.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "{0} jest wymagane.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Wprowadź między 2 do 60 znaków.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "PESEL")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL musi składać się z 11 cyfr")]
        public string PersonalIdNumber { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = null!;
        public bool IsFree { get; set; }       
    }
}
