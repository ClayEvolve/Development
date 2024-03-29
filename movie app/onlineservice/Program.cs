global using onlineservice.Models;
global using onlineservice.Services.MovieService;
global using onlineservice.Services.MovieWatchedService;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using onlineservice.Data;
global using Microsoft.EntityFrameworkCore.Sqlite;
global using IMDbApiLib;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{ 
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy  =>
                      {
                          policy.WithOrigins("*");
                      });
});

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieWatchedService, MovieWatchedService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<DataContext>();



// builder.Services.AddImdb<AppSettingsModel>(builder.Configuration.GetSection("ApplicationSettings"));

// Other configuration stuff

//builder.services.AddOptions();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
