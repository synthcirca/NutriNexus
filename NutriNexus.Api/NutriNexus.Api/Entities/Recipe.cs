using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NutriNexus.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.Entities
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public required string ImageUrl { get; set; }
        [Required]
        public required string Description { get; set; }
        public decimal Rating { get; set; } = 0m;

        //Quick Info
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }
        public int ServingSize { get; set; }

        public string? Course {get; set;}
        public string? Cuisine {get; set;}

        public ICollection<RecipeEquipment> RecipeEquipment { get; set; } = new List<RecipeEquipment>();
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<RecipeInstruction> RecipeInstructions { get; set; } = new List<RecipeInstruction>();
        
        //Nutrition Information
        public int Calories { get; set; } = 0;
        public int Carbs { get; set; } = 0;
        public int Protein { get; set; } = 0;

        public int Fat { get; set; } = 0; 

        // public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
        // public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
        // public string? Notes { get; set; }
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
