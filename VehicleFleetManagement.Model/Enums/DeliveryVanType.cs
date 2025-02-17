using System.ComponentModel.DataAnnotations;

namespace VehicleFleetManagement.Model.Enums
{
    public enum DeliveryVanType
    {
        [Display(Name = "Plandeka/firana")]
        Curtainsider,
        [Display(Name = "Furgon(blaszak)")]
        BoxTruck,
        [Display(Name = "Kontener")]
        Container,
        [Display(Name = "Wywrotka")]
        Tipper,
        [Display(Name = "Doka")]
        Dropside,
        [Display(Name = "Chłodnia")]
        Refrigerated,
        [Display(Name = "Izoterma")]
        Isothermal,
        [Display(Name = "Autolaweta")]
        TowTruck
    }
}