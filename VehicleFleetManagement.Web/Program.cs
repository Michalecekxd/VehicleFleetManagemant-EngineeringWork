using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing.Text;
using VehicleFleetManagement.DAL.EF;
using VehicleFleetManagement.Model.DataModels;
using VehicleFleetManagement.Model.Identity;
using VehicleFleetManagement.Web.Areas.Identity.ErrorDescriber;
using ZstdSharp.Unsafe;


var builder = WebApplication.CreateBuilder(args);


// SQL Server
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// We don't use MySQL- it doesn't have a built-in sequence feature (we need sequence to create unique Id for each inherit table- Id values MUST BE DIFFERENT in each table e.g. table TractorUnit Id 1 and table RigidTruck Id 2 <-- RigidTruck cannot have 1 )
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Identity 
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)    // Default Identity User- default user class provided by Identity (include AspNetUsers, AspNetRoles and other tables. It contains columns like: first name, last name, email, passwordhash etc.)
    .AddRoles<ApplicationRole>()  // Add Role Support (?thanks to this we can assign roles to users?)
    .AddEntityFrameworkStores<ApplicationDbContext>()  // Add Entity Framework Stores to keep data about users and roles in database 
    .AddErrorDescriber<CustomIdentityErrorDescriber>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

#region COOKIES
//We can change the default settings of the cookie (for example when we close the browser, we can be log in only for 1 minute, 30 mintues etc.)) 
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);         // the duration of the cookies (if the page does not refresh after x minutes the user will be logged out)
//    options.SlidingExpiration = true;    // user logs in at 12:00, the cookie expires at 12:01 (if user refreshes the page, the cookie expiration is extended by x minutes, when user does smth on the website at 12:00:45 the logout time will be extended to 12:01:45)
//    //options.SlidingExpiration = false; // user logs in at 12:00, the cookie expires at 12:01 (even if user refreshes the page)- cookie expiration is fixed, is not extended
//    options.Cookie.IsEssential = true;   // set this property true
//});
#endregion

var app = builder.Build();


await SeedRolesAndUsers(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adding authentication- you must be logged in to access the page (after logging in you have access to everything)- when you are not logged in you will be redirected to the login page- UseAuthentication is necessary to UseAuthorization- when you are not logged in you can't go to the page http://localhost:5045/OwnerRepair/ActiveRepairs (401 Unauthorized))
app.UseAuthentication();
// Adding authorization- when you are logged you can add a role to access the page (for example when u are logged in to the Client u can't go to the page http://localhost:5045/OwnerRepair/ActiveRepairs (403 Forbidden))
// Don't forget add [Authorize(Roles = "Owner")] in your controller
app.UseAuthorization();

app.MapRazorPages();  // Mapping to appropriate endpoints for example Pages/Account/Login.cshtml 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


//Create Roles, Users(only Owner) and assigning him a role
async Task SeedRolesAndUsers(IServiceProvider serviceProvider) // create roles and users (users mean only Owner in this case)
{
    using var scope = serviceProvider.CreateScope();

    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    //Create vehicles and displaying them (checking correctness of classes)
    //TractorUnit tractorUnit = new TractorUnit();
    //tractorUnit.Brand = "BrandTractorUnit";
    //tractorUnit.RegistrationNumber = "tr 2222";
    //tractorUnit.Model = "ModelTractorUnit";
    //_context.TractorUnits.Add(tractorUnit);
    //_context.SaveChanges();

    //DeliveryVan deliveryvan = new DeliveryVan();
    //deliveryvan.Brand = "BrandDelieveryVan";
    //deliveryvan.RegistrationNumber = "de 4444";
    //deliveryvan.Model = "ModelDeliveryVan";
    //_context.DeliveryVans.Add(deliveryvan);
    //_context.SaveChanges();

    //RigidTruck rigid = new RigidTruck();
    //rigid.Brand = "BrandRigidTruck";
    //rigid.RegistrationNumber = "ri 1111";
    //rigid.Model = "ModelRigidTruck";
    //_context.RigidTrucks.Add(rigid);
    //_context.SaveChanges();

    //var tractorunits = _context.TractorUnits.ToList();
    //var rigidtrucks = _context.RigidTrucks.ToList();
    //var delivervans = _context.DeliveryVans.ToList();
    //var vehicles = new List<Vehicle>();
    //vehicles.AddRange(tractorunits);
    //vehicles.AddRange(rigidtrucks);
    //vehicles.AddRange(delivervans);
    //foreach (var vehicle in vehicles)
    //{
    //    Console.WriteLine($"Typ pojazdu: {vehicle.GetType().Name}"); // Wypisujemy typ pojazdu

    //    foreach (var property in vehicle.GetType().GetProperties())
    //    {
    //        var propertyName = property.Name;
    //        var propertyValue = property.GetValue(vehicle);
    //        Console.WriteLine($"  {propertyName}: {propertyValue}");
    //    }

    //    Console.WriteLine(); 
    //}


    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>(); // RoleManager is a class provided by Identity to manage roles
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();  // UserManager is a class provided by Identity to manage users

    string[] roleNames = { "Owner", "Client" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new ApplicationRole(roleName)); // CreateAsync is a method provided by Identity to create a new role
        }
    }

    // Owner data
    string ownerEmail = "owner11@owner11.pl";
    string ownerPassword = "Owner@123";


    var owner = await userManager.FindByEmailAsync(ownerEmail);
    if (owner == null)
    {

        var newOwner = new ApplicationUser      // we use ApplicationUser instead of IdentityUser
        {
            UserName = ownerEmail,
            Email = ownerEmail,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(newOwner, ownerPassword);    // CreateAsync is a method provided by Identity to create a new user with a password

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newOwner, "Owner");
            Console.WriteLine(newOwner.Id);

            var dbUser = await _context.Users.FindAsync(newOwner.Id);
            if (dbUser != null)
            {
                dbUser.UserType = "Owner";  // add value "Owner" to custom column "UserType" (AspNetUsers table)
                await _context.SaveChangesAsync();
            }
        }
        else
            throw new Exception("Seeding owner account went wrong");
    }


    //CREATE USERS (checking the correctness of role assignment) 
    //var user1 = new ApplicationUser
    //{
    //    UserName = "adam",
    //    Email = "adam@adam.pl",
    //    EmailConfirmed = true,
    //};
    //var user2 = new ApplicationUser
    //{
    //    UserName = "ewa",
    //    Email = "ewa@ewa.pl",
    //};

    //var user1Result = await userManager.CreateAsync(user1);
    //if (user1Result.Succeeded)
    //{
    //    await userManager.AddToRoleAsync(user1, "Owner");
    //}

    //var user2Result = await userManager.CreateAsync(user2);
    //if (user2Result.Succeeded)
    //{
    //    await userManager.AddToRoleAsync(user2, "Client");
    //}


    // Displaying all users and roles
    //var users = _context.Users.ToList();
    //Console.WriteLine("Lista uzytkownikow: ");
    //foreach (var user in users)
    //{
    //    Console.WriteLine($"ID: {user.Id}, Email: {user.Email}");
    //}

    // var roles = _context.Roles.ToList();
    //Console.WriteLine("Lista ról: ");
    // foreach (var role in roles)
    // {
    //     Console.WriteLine($"ID: {role.Id}, Name: {role.Name}");
    // }
}
