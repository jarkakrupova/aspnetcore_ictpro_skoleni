using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//nezavisi na poradi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); //kouka do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); //DbContext patri k EF
builder.Services.AddDatabaseDeveloperPageExceptionFilter();  

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); //default rovnou prida GUI, reset hesla ap., vsechno ma v sobe
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); //pridano pro podporu API controlleru

var app = builder.Build();

// Configure the HTTP request pipeline.
//!!!POZOR, zavisi na poradi!!!
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    //uzivatel uvidi jenom jednoduchou chybovou stranku
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //proto kdyz nezadame nic, skoci to na home/index
app.MapRazorPages(); //kvuli defaultnim views pro Identity, ty jsou .cshtml

app.Run();
