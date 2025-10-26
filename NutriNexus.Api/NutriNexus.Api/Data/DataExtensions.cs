using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutriNexusAPI.Data;

namespace GameStore.API.Data;

//don't want to have to do migrations through the command line each time
public static class DataExtensions
{
	public static async Task MigrateDb(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<MealAppContext>();
		await dbContext.Database.MigrateAsync();
	}
}
