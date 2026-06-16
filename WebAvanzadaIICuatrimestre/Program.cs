using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.BLL;
using WebAvanzadaIICuatrimestre.BLL.Services.Carro;
using WebAvanzadaIICuatrimestre.BLL.Services.Cliente;
using WebAvanzadaIICuatrimestre.BLL.Services.Duenno;
using WebAvanzadaIICuatrimestre.DAL.Data;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Carro;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Cliente;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Duenno;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar Antiforgery para aceptar el token en headers
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

// Register EF Core DbContext (SQLite). Update the connection string in appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Inyección de dependencias para repositorios, servicios, etc. Extraer a clase configuracion de servicios para mantener el Program.cs limpio y organizado. Se pueden crear clases estáticas para cada capa (Repositorios, Servicios, etc.) y llamar a sus métodos de configuración desde aquí para una mejor organización.
// Repositorios
builder.Services.AddScoped<ICarroRepositorio, CarroRepositorio>();
builder.Services.AddScoped<IDuennoRepositorio, DuennoRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

//Servicios
builder.Services.AddScoped<ICarroServicio, CarroServicio>();
builder.Services.AddScoped<IDuennoServicio, DuennoServicio>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();

// Servicios Terceros
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases)); // Directamente desde la documentación



















var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
