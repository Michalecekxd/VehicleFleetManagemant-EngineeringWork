using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetManagement.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;




namespace VehicleFleetManagement.Model.DataModels
{
    public abstract class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Numer rejestracyjny")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [StringLength(8, MinimumLength = 5, ErrorMessage = "{0} musi zawierać od 5 do 7 znaków.")]
        [RegularExpression(@"^[A-Z]{2,3}\s[A-Z0-9]{3,5}$", ErrorMessage = "To nie jest poprawny {0}.")]
        public string RegistrationNumber { get; set; } = null!; //never null 

        [Display(Name = "Marka")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0} musi zawierać od 1 do 20 znaków.")]
        public string Brand { get; set; } = null!;

        [Display(Name = "Rok produkcji")]
        [Required(ErrorMessage = "{0} jest wymagany.")]
        [Range(1900, 2025, ErrorMessage = "{0} nie jest poprawny.")]
        public int YearOfProduction { get; set; } //never null (default 0)

        [Display(Name = "Data ważności badania technicznego")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Column(TypeName = "date")]
        public DateOnly TechnicalInspectionValidUntil { get; set; }   // default is DateTime.MinValue in SQL Server, MySQL and -Infinity in PostgreSQL <--each of them is MinValue of Date

        [Display(Name = "Data ważności OC")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Column(TypeName = "date")]
        public DateTime InsuranceValidUntil { get; set; } 

        [Display(Name = "Masa własna")]
        [Required(ErrorMessage = "{0} jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} musi być większa od 0.")] 
        public double CurbWeight { get; set; }
        public bool IsDamaged { get; set; }
        public Vehicle() { }
        //public Vehicle (VehicleType type)   //constructor to set the vehicle type for each inherinting class
        //{
        //    Type = type;
        //}

    }
}
