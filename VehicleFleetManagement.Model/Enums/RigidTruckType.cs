using System.ComponentModel.DataAnnotations;

namespace VehicleFleetManagement.Model.Enums
{
    public enum RigidTruckType
    {
        [Display(Name = "Plandeka/firana")]
        Curtainsider,
        [Display(Name = "Furgon(sztywna zabudowa)")]
        BoxTruck,
        [Display(Name = "Wywrotka")]
        Tipper,
        [Display(Name = "Wywrotka z hds")]
        TipperWithCrane,
        [Display(Name = "Chłodnia")]
        Refrigerated,
        [Display(Name = "Izoterma")]
        Isothermal,
        [Display(Name = "Autolaweta")]
        TowTruck
    }
}