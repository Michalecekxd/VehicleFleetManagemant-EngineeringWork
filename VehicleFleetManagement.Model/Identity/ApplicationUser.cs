using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetManagement.Model.Identity
{
    // we create this class to add additional columns to AspNetUsers table and change <string>(guid<-- its digits and numbers like '6f043c57-42a2-4704-ab97-828c74343463') to <int> for Id in IdentityUser
    // we can use discriminator also (OnModelCreating) to add additional column to AspNetUsers table 
    public class ApplicationUser : IdentityUser<int>
    {
        public string? UserType { get; set; }
    }
}
