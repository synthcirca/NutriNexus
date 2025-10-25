using GameStore.API.Data;
using GameStore.API.Endpoints;
using NutriNexusAPI.Data;
using NutriNexusAPI.Endpoints; 

var builder = WebApplication.CreateBuilder(args);

//"Data Source=GameStore.db"; //this will create a Sqlite database at the root of the project

var connString = builder.Configuration.GetConnectionString("MealApp");
builder.Services.AddSqlite<MealAppContext>(connString); //EF creates a mapping all the way to the GameStoreContext class
builder.Services.AddScoped<MealAppContext>(); //ensure connections to db is efficient. this context is not thread safe
builder.Services.AddCors(opt =>{
	opt.AddPolicy("AllowOrigin", policyBuilder =>{
		policyBuilder.WithOrigins("https://localhost:60071/")
			.AllowAnyHeader()
			.AllowAnyOrigin()
			.AllowAnyMethod();
	});
});
var app = builder.Build();
app.UseCors("AllowOrigin");
//app.MapGamesEndpoints();
app.MapMealAppEndpoints();


app.Run();

