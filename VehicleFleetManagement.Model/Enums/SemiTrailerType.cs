using System.ComponentModel.DataAnnotations;

namespace VehicleFleetManagement.Model.Enums
{
    public enum SemiTrailerType
    {
        [Display(Name = "Kurtynowa(firanka/plandeka)")]
        Curtainsider,
        [Display(Name = "Wywrotka")]
        Tipper,
        [Display(Name = "Platforma")]
        FlatBed,
        [Display(Name = "Burtowa")]
        Dropside,
        [Display(Name = "Niskopodwoziowa")]
        LowBed,
        [Display(Name = "Chłodnia")]
        Refrigerated,
        [Display(Name = "Izoterma")]
        Isothermal,
        [Display(Name = "Kłonicowa")]
        Stanchion
    }
}