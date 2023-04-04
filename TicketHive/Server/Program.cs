using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Models;
using TicketHive.Server.Repos.EventsRepo;
using TicketHive.Server.Repos.UsersRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(identityConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var ticketHiveConnectionString = builder.Configuration.GetConnectionString("TicketHiveDbConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ticketHiveConnectionString));

builder.Services.AddScoped<IUsersRepo, UsersRepo>();
builder.Services.AddScoped<IEventsRepo, EventsRepo>();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, AuthDbContext>(options =>
    {
        options.IdentityResources["openid"].UserClaims.Add("role");
        options.ApiResources.Single().UserClaims.Add("role");
    });

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//skapa en instans av ServiceProvider så att vi kan komma åt allt som ligger i DI container
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    //Hämta instanser från DI containern (context, signInManager och roleManager)
    var context = serviceProvider.GetRequiredService<AuthDbContext>();
    var signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //säkerställ att databasen är skapad, annars skapa den
    context.Database.Migrate();

    ApplicationUser? adminUser = signInManager.UserManager.FindByNameAsync("admin").GetAwaiter().GetResult();

    //om vi inte hittar en användare med användarnamnet admin
    if (adminUser == null)
    {
        //skapa en användare-instans med "admin" username
        adminUser = new()
        {
            UserName = "admin",
        };

        //skapa en användare 
        signInManager.UserManager.CreateAsync(adminUser, "Password1234!").GetAwaiter().GetResult();
    }

    ApplicationUser? user = signInManager.UserManager.FindByNameAsync("user").GetAwaiter().GetResult();

    //om vi inte hittar en användare med användarnamnet admin
    if (user == null)
    {
        //skapa en användare-instans med "admin" username
        user = new()
        {
            UserName = "user",
        };

        //skapa en användare 
        signInManager.UserManager.CreateAsync(user, "Password1234!").GetAwaiter().GetResult();
    }

    //Använd roleManager för att skapa en roll med namn "Admin"
    IdentityRole? adminRole = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();

    if (adminRole == null)
    {
        //skapa en instans av IdentityRole med namn "Admin"
        adminRole = new()
        {
            Name = "Admin"
        };

        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    //Lägg till den nya adminrollen till den nya adminanvändaren
    signInManager.UserManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
