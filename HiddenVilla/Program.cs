using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Business.Mapper;
using Business.Repository.IRepository;
using Business.Repository;
using CurrieTechnologies.Razor.SweetAlert2;
using HiddenVilla.Service.IService;
using HiddenVilla.Service;
using HiddenVilla.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();



// Register AutoMapper with the specific profile.
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();  // Register your mapping profile
});
builder.Services.AddTransient<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddTransient<IHotelImagesRepository, HotelImagesRepository>();
builder.Services.AddTransient<IFileUpload, FileUpload>();
builder.Services.AddTransient<IHotelAmenityRepository, HotelAmenityRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSweetAlert2();
builder.Services.AddRazorComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});


app.Run();
