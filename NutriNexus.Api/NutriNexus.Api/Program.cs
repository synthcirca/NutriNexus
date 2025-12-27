using NutriNexusAPI.Data;
using NutriNexusAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("MealApp");
builder.Services.AddSqlite<MealAppContext>(connString); //EF creates a mapping all the way to the GameStoreContext class
builder.Services.AddScoped<MealAppContext>(); //ensure connections to db is efficient. this context is not thread safe
builder.Services.AddCors(opt => {
    opt.AddPolicy("AllowOrigin", policyBuilder => {
        policyBuilder.WithOrigins("https://localhost:60071/", "http://localhost:5173/")
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowOrigin");
app.MapMealAppEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

public partial class Program { }