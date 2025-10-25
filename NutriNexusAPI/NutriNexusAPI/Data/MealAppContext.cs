using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;
using NutriNexusAPI.Entities;

namespace NutriNexusAPI.Data
{
    public class MealAppContext(DbContextOptions<MealAppContext> options) : DbContext(options)
    {
        public DbSet<Recipe> Recipes => Set<Recipe>(); //DbSet is an object that can be used to query the db
        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();
        //public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        //this will execute when you do the model migration
        //only for simple static data that wont be changed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new
                {
                    Id = 1,
                    Name = "Pesto Pasta",
                    TimeEstimate = 80970,
                    ServingSize = 1
                }
            );

            modelBuilder.Entity<RecipeStep>().HasData(
                new { Id = 1, Description = "Bowl water", RecipeId = 1},
                new { Id = 2, Description = "put pasta in the water", RecipeId = 1}
            );
        }
    }
}
