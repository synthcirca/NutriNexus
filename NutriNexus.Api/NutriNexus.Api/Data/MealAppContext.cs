using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;
using NutriNexusAPI.Entities;

namespace NutriNexusAPI.Data
{
    public class MealAppContext(DbContextOptions<MealAppContext> options) : DbContext(options)
    {
        public DbSet<Recipe> Recipes => Set<Recipe>(); //DbSet is an object that can be used to query the db
        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        public DbSet<Direction> Directions => Set<Direction>();
        public DbSet<Unit> Units => Set<Unit>(); 
        public DbSet<Equipment> Equipment => Set<Equipment>(); 
        public DbSet<NutritionFactCategory> NutritionFactCategories => Set<NutritionFactCategory>(); 

        public DbSet<NutritionFact> NutritionFacts => Set<NutritionFact>(); 
        //public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        //this will execute when you do the model migration
        //only for simple static data that wont be changed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new
                {
                    Id = 1,
                    Name = "The Best Chocolate Chip Cookie Recipe Ever",
                    Rating = 4.99m,
                    ImageUrl = "/chocolate-chip-cookie.jpg",
                    PrepTime = 10,
                    CookTime = 8,
                    TotalTime = 30,
                    ServingSize = 36,
                    Description = "This is the best chocolate chip cookie recipe ever. No funny ingredients, no chilling time, etc. Just a simple, straightforward, amazingly delicious, doughy yet still fully cooked, chocolate chip cookie that turns out perfectly every single time!",
                    Course = "Dessert",
                    Cuisine = "American"
                }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new {Id = 1, Name = "salted butter", Amount = 1, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 2, Name = "granulated sugar", Amount = 1, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 3, Name = "light brown sugar", Amount = 1, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 4, Name = "pure vanilla extract", Amount = 1, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 5, Name = "egg", Amount = 2, Calories = 34.9f, UnitId = 2, RecipeId = 1},
                new {Id = 5, Name = "all-purpose flour", Amount = 3, Calories = 34.9f, UnitId = 1, RecipeId = 1},
                new {Id = 5, Name = "baking soda", Amount = 1, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 5, Name = "baking powder", Amount = 0.5, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 5, Name = "sea salt", Amount = 1, Calories = 34.9f, UnitId = 3, RecipeId = 1},
                new {Id = 5, Name = "chocolate chips", Amount = 2, Calories = 34.9f, UnitId = 1, RecipeId = 1}
            );

            modelBuilder.Entity<Direction>().HasData(
                new {Id = 1, StepNumber = 1, Description = "Preheat oven to 375 degrees Fahrenheit (190 degrees Celsius). Line three baking sheets with parchment paper and set aside.", RecipeId = 1},
                new {Id = 2, StepNumber = 2, Description = "In a medium bowl mix flour, baking soda, baking powder and salt. Set aside.", RecipeId = 1},
                new {Id = 3, StepNumber = 3, Description = "Cream together butter and sugars until combined.", RecipeId = 1}
            );

            modelBuilder.Entity<NutritionFact>().HasData(
                new {Id = 1, RecipeId = 1, NutritionFactCategoryId = 1, Value = 183},
                new {Id = 2, RecipeId = 1, NutritionFactCategoryId = 2, Value = 8},
                new {Id = 3, RecipeId = 1, NutritionFactCategoryId = 3, Value = 26},
                new {Id = 4, RecipeId = 1, NutritionFactCategoryId = 4, Value = 2}
            );

            modelBuilder.Entity<Unit>().HasData(
                new {Id = 1, Name = "cup"},
                new {Id = 2, Name = "egg"},
                new {Id = 3, Name = "tsp"}
            );

            modelBuilder.Entity<NutritionFactCategory>().HasData(
                new {Id = 1, Name = "Calories"},
                new {Id = 2, Name = "Fat"},
                new {Id = 3, Name = "Carbs"},
                new {Id = 4, Name = "Protein"}
            );
        }
    }
}

       // modelBuilder.Entity<RecipeStep>().HasData(
            //     new { Id = 1, Description = "Bowl water", RecipeId = 1},
            //     new { Id = 2, Description = "put pasta in the water", RecipeId = 1}
            // );