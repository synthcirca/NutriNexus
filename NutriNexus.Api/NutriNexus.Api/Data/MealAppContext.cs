using Microsoft.EntityFrameworkCore;
using NutriNexusAPI.Entities;

namespace NutriNexusAPI.Data
{
    public class MealAppContext(DbContextOptions<MealAppContext> options) : DbContext(options)
    {
        public DbSet<Recipe> Recipes => Set<Recipe>(); //DbSet is an object that can be used to query the db
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        //public DbSet<Direction> Directions => Set<Direction>();
        //public DbSet<Unit> Units => Set<Unit>(); 
        //public DbSet<Equipment> Equipment => Set<Equipment>(); 
    
        //public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        //this will execute when you do the model migration
        //only for simple static data that wont be changed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new
                {
                    RecipeId = 1,
                    Name = "The Best Chocolate Chip Cookie Recipe Ever",
                    Rating = 4.99m,
                    ImageUrl = "/chocolate-chip-cookie.jpg",
                    PrepTime = 10,
                    CookTime = 8,
                    TotalTime = 30,
                    ServingSize = 36,
                    Description = "This is the best chocolate chip cookie recipe ever. No funny ingredients, no chilling time, etc. Just a simple, straightforward, amazingly delicious, doughy yet still fully cooked, chocolate chip cookie that turns out perfectly every single time!",
                    Course = "Dessert",
                    Cuisine = "American",
                    Calories = 100,
                    Carbs = 100,
                    Protein = 100,
                    Fat = 100
                
                }
            );

            modelBuilder.Entity<Direction>().HasData(
                new { Id = 1, RecipeId = 1, StepNumber = 1, Instruction = "cookie 1" },
                new { Id = 2, RecipeId = 1, StepNumber = 2, Instruction = "cookie 2" },
                new { Id = 3, RecipeId = 1, StepNumber = 3, Instruction = "cookie 3" }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new {Id = 1, Name = "salted butter", Amount = 1f, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 2, Name = "granulated sugar", Amount = 1f, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 3, Name = "light brown sugar", Amount = 1f, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 4, Name = "pure vanilla extract", Amount = 1f, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 5, Name = "egg", Amount = 2f, Calories = 34.9f, UnitId = 2, RecipeId = 1},
                new {Id = 6, Name = "all-purpose flour", Amount = 3f, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 7, Name = "baking soda", Amount = 1f, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 8, Name = "baking powder", Amount = 0.5f, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 9, Name = "sea salt", Amount = 1f, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 10, Name = "chocolate chips", Amount = 2f, Calories = 34.9f, UnitId = 1, RecipeId = 1}
            );

            //modelBuilder.Entity<Unit>().HasData(
            //    new {Id = 1, Name = "cup"},
            //    new {Id = 2, Name = "egg"},
            //    new {Id = 3, Name = "tsp"}
            //);

        }
    }
}
