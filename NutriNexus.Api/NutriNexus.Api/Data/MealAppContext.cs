using Microsoft.EntityFrameworkCore;
using NutriNexus.Api.Entities;
using NutriNexusAPI.Entities;

namespace NutriNexusAPI.Data
{
    public class MealAppContext(DbContextOptions<MealAppContext> options) : DbContext(options)
    {
        public DbSet<Recipe> Recipes => Set<Recipe>(); //DbSet is an object that can be used to query the db

        public DbSet<Equipment> Equipment => Set<Equipment>();
        public DbSet<RecipeEquipment> RecipeEquipment => Set<RecipeEquipment>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

        public DbSet<RecipeInstruction> RecipeInstructions => Set<RecipeInstruction>();

        //this will execute when you do the model migration
        //only for simple static data that wont be changed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new
                {
                    RecipeId = 1,
                    Name = "The Best Chocolate Chip Cookie Recipe Ever",
                    Author = "Laura",
                    Description = "This is the best chocolate chip cookie recipe ever. No funny ingredients, no chilling time, etc. Just a simple, straightforward, amazingly delicious, doughy yet still fully cooked, chocolate chip cookie that turns out perfectly every single time!",
                    SourceUrl = "",
                    Rating = 4.99m,
                    ImageUrl = "/chocolate-chip-cookie.jpg",
                    Course = "Dessert",
                    Cuisine = "American",
                    ServingSize = 36,
                    PrepTime = 10,
                    CookTime = 8,
                    TotalTime = 30,
                    VideoUrl = "",
                    Notes = "",
                    Calories = 100,
                    Carbs = 100,
                    Protein = 100,
                    Fat = 100
                }
            );

            modelBuilder.Entity<Equipment>().HasData(
                new { Id = 1, Name = "measuring spoons", Description = "Standard measuring spoons", SourceUrl = "https://amzlink.to/az0Tmz0p6RGTA" },
                new { Id = 2, Name = "measuring cups", Description = "Dry measuring cups", SourceUrl = "https://amzlink.to/az0DjP0Sx7AVd" },
                new { Id = 3, Name = "electric mixer", Description = "Hand or stand mixer", SourceUrl = "https://amzlink.to/az04r9lAhaKsP" },
                new { Id = 4, Name = "spatula", Description = "Rubber spatula for mixing", SourceUrl = "https://amzlink.to/az0cHANfPnAKd" },
                new { Id = 5, Name = "baking sheet", Description = "Standard baking sheet", SourceUrl = "https://amzlink.to/az0h3VEQB4BtJ" },
                new { Id = 6, Name = "parchment paper", Description = "For lining baking sheets", SourceUrl = "https://amzlink.to/az0e1HAvwbn07" },
                new { Id = 7, Name = "cookie scoop", Description = "for scooping cookies", SourceUrl = "https://amzlink.to/az0935VbND2Ud" },
                new { Id = 8, Name = "silicone baking mat", Description = "silicone baking mat", SourceUrl = "https://amzlink.to/az0tZruG7esod" },
                new { Id = 9, Name = "hand mixer", Description = "for mixing", SourceUrl = "https://amzlink.to/az0GGY6pRrF4W" }
            );

            modelBuilder.Entity<RecipeEquipment>().HasData(
                new { Id = 1, RecipeId = 1, EquipmentId = 1, Quantity = 1 }, // measuring spoons
                new { Id = 2, RecipeId = 1, EquipmentId = 2, Quantity = 1 }, // measuring cups
                new { Id = 3, RecipeId = 1, EquipmentId = 3, Quantity = 1, Notes = "The Kitchen Aid is a very popular mizer" }, // electric mixer
                new { Id = 4, RecipeId = 1, EquipmentId = 4, Quantity = 1 }, // spatula
                new { Id = 5, RecipeId = 1, EquipmentId = 5, Quantity = 3 }, // baking sheet
                new { Id = 6, RecipeId = 1, EquipmentId = 6, Quantity = 1 }, // parchment paper
                new { Id = 7, RecipeId = 1, EquipmentId = 7, Quantity = 1 },  // cookie scoop
                new { Id = 8, RecipeId = 1, EquipmentId = 8, Quantity = 1 },  // silicone baking mat
                new { Id = 9, RecipeId = 1, EquipmentId = 9, Quantity = 1 }  // hand mixer
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new { Id = 1, Name = "salted butter", Calories = 40f },
                new { Id = 2, Name = "granulated sugar", Calories = 40f },
                new { Id = 3, Name = "light brown sugar", Calories = 40f },
                new { Id = 4, Name = "pure vanilla extract", Calories = 40f },
                new { Id = 5, Name = "egg", Calories = 40f },
                new { Id = 6, Name = "all-purpose flour", Calories = 40f },
                new { Id = 7, Name = "baking soda", Calories = 40f },
                new { Id = 8, Name = "baking powder", Calories = 40f },
                new { Id = 9, Name = "sea salt", Calories = 40f },
                new { Id = 10, Name = "chocolate chips", Calories = 40f }
            );

            modelBuilder.Entity<RecipeIngredient>().HasData(
                new { Id = 1, RecipeId = 1, IngredientId = 1, Quantity = 1f, Unit = "cup", Note = "softned"},
                new { Id = 2, RecipeId = 1, IngredientId = 2, Quantity = 1f, Unit = "cup" },
                new { Id = 3, RecipeId = 1, IngredientId = 3, Quantity = 1f, Unit = "cup", Note = "packed" },
                new { Id = 4, RecipeId = 1, IngredientId = 4, Quantity = 2f, Unit = "tsp" },
                new { Id = 5, RecipeId = 1, IngredientId = 5, Quantity = 2f, Unit = "large" },
                new { Id = 6, RecipeId = 1, IngredientId = 6, Quantity = 3f, Unit = "cup" },
                new { Id = 7, RecipeId = 1, IngredientId = 7, Quantity = 1f, Unit = "tsp" },
                new { Id = 8, RecipeId = 1, IngredientId = 8, Quantity = 0.5f, Unit = "tsp" },
                new { Id = 9, RecipeId = 1, IngredientId = 9, Quantity = 1f, Unit = "tsp" },
                new { Id = 10, RecipeId = 1, IngredientId = 10, Quantity = 1f, Unit = "cup", Note = "12 oz" }
            );

            modelBuilder.Entity<RecipeInstruction>().HasData(
                new { Id = 1, RecipeId = 1, StepNumber = 1, Instruction = "Preheat oven to 375 degrees Fahrenheit (190 degrees Celsius). Line three baking sheets with parchment paper and set aside." },
                new { Id = 2, RecipeId = 1, StepNumber = 2, Instruction = "In a medium bowl mix flour, baking soda, baking powder and salt. Set aside." },
                new { Id = 3, RecipeId = 1, StepNumber = 3, Instruction = "Cream together butter and sugars until combined." },
                new { Id = 4, RecipeId = 1, StepNumber = 4, Instruction = "Beat in eggs and vanilla until light (about 1 minute)." },
                new { Id = 5, RecipeId = 1, StepNumber = 5, Instruction = "Mix in the dry ingredients until combined." },
                new { Id = 6, RecipeId = 1, StepNumber = 6, Instruction = "Add chocolate chips and mix well." },
                new { Id = 7, RecipeId = 1, StepNumber = 7, Instruction = "Roll 2-3 Tablespoons (depending on how large you like your cookies) of dough at a time into balls and place them evenly spaced on your prepared cookie sheets. " },
                new { Id = 8, RecipeId = 1, StepNumber = 8, Instruction = "Bake in preheated oven for approximately 8-10 minutes. Take them out when they are just barely starting to turn brown." },
                new { Id = 9, RecipeId = 1, StepNumber = 9, Instruction = "Let them sit on the baking pan for 5 minutes before removing to cooling rack." }
            );
        }
    }
}
