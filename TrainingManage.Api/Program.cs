using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;
using TrainingManage.Api;
using TrainingManage.Api.Interfaces;
using TrainingManage.Data;
using TrainingManage.Data.Interfaces;
using TrainingManage.Data.Repositories;
using TrainingManage.Api.Managers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------- Databázové připojení a EF Core konfigurace ----------
var connectionString = builder.Configuration.GetConnectionString("LocalTrainingManageConnection");

builder.Services.AddDbContext<TrainingDbContext>(options =>
    options.UseSqlServer(connectionString)
           .UseLazyLoadingProxies()
           .ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)));


// ---------- Controllers + JSON konfigurace ----------
builder.Services.AddControllers()
       // Převod enum na string v JSON (čitelné výstupy v API)
       .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


// ---------- Swagger / OpenAPI ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Popis dokumentace
    options.SwaggerDoc("TrainingManage", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tréninková databáze",
        Description = "Webové API pro tréninkovou databázi pomocí ASP.NET",
        Contact = new OpenApiContact { Name = "Kontakt" }
    });

    // Definice bezpečnostního schématu (Bearer JWT)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Vlož JWT token ve formátu: Bearer {token}"
    });

    // Požadavek použití definovaného schématu pro endpointy
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


// ---------- Dependency Injection: repositories & managers ----------
// Data repositories (implementace pro přístup k databázi)
builder.Services.AddScoped<IStatsRepository, StatsRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

// Business layer (managers)
builder.Services.AddScoped<IStatsManager, StatsManager>();
builder.Services.AddScoped<ITrainingManager, TrainingManager>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IExpenseManager, ExpenseManager>();
builder.Services.AddScoped<IRegistrationManager, RegistrationManager>();

// AutoMapper konfigurace (mapování entit ↔ DTO)
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));


// ---------- Build & middleware pipeline ----------
var app = builder.Build();

// Swagger jen v development prostředí
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/TrainingManage/swagger.json",
                                "Tréninková databáze - v1");
    });
}

// Jednoduchý health-check endpoint na kořenové URL
app.MapGet("/", () => "Training Manage API is running...");

// Namapování controller rout
app.MapControllers();

// Spuštění aplikace
app.Run();




// USER_PASSWORD  = 'user';
// ADMIN_PASSWORD = 'admin';



//"userName": "Michal",
//"password": "Michal123!"

//{
//  "userName": "MichVach",
//  "password": "Tr.Manage110100!"
//}

//{
//    "identificationNumber": 26,
//  "name": "testuser",
//  "age": 40,
//  "telephone": "555555555",
//  "email": "user.user@user.cz",
//  "password": "User123!"
//}