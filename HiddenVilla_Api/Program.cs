using Business.Repository.IRepository;
using Business.Repository;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Business.Mapper;
using DataAccess.Data.Entities;
using HiddenVilla_Api.Helper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Stripe;
using Microsoft.Extensions.DependencyInjection;
using HiddenVilla.notify.Models;
using HiddenVilla.notify.Implementation;
using HiddenVilla.notify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var appSettings = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(appSettings);

//builder.Services.Configure<APISettings>(builder.Configuration.GetSection("APISettings"));

// Configure Stripe
builder.Services.AddTransient<StripeClient>(_ => new StripeClient(builder.Configuration["Stripe:ApiKey"]));


IServiceCollection serviceCollection = builder.Services.Configure<EmailConfigs>(builder.Configuration.GetSection("EmailConfigs"));


var apisettings = appSettings.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apisettings.Key);

// Add authentication to authenticate the user is valid or not
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apisettings.ValidAudience,
        ValidIssuer = apisettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});

// Register AutoMapper with the specific profile.
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();  // Register your mapping profile
});
builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddScoped<IHotelImagesRepository, HotelImagesRepository>();
builder.Services.AddScoped<IHotelAmenityRepository, HotelAmenityRepository>();
builder.Services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();
builder.Services.AddScoped<IMessages, Messages>();

builder.Services.AddCors(o => o.AddPolicy("HiddenVilla", options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
       }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("HiddenVilla");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
