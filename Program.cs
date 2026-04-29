using Microsoft.Data.SqlClient;
using System.Data;
using WebApisforstorebySuchi.DAO;
using WebApisforstorebySuchi.Interface;
using WebApisforstorebySuchi.Manager;

var builder = WebApplication.CreateBuilder(args);

// ? SERVICES (MUST be before Build)

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? DB Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Dependency Injection
builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<IProductManager, ProductManager>();

// ? CORS (MUST be here, NOT after Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ? BUILD APP
var app = builder.Build();


// ? MIDDLEWARE (after Build)

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ? Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Optional test endpoint
app.MapGet("/", () => "Hello World!");

app.Run();