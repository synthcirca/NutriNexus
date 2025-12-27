using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NutriNexus.Api.DTO;
using NutriNexusAPI.Data;
using NutriNexusAPI.DTO;
using System.Net;
using System.Net.Http.Json;

namespace MealApp.Tests;

public class MealAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the real database
            services.Remove(
                services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<MealAppContext>))
            );
            // Add in-memory database for testing
            services.AddDbContext<MealAppContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Seed test data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MealAppContext>();
            SeedTestData(db);
        });
    }

    private static void SeedTestData(MealAppContext db)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // Seed will happen automatically from OnModelCreating
        // Or add additional test data here if needed
        db.SaveChanges();
    }
}

public class RecipesEndpointTests : IClassFixture<MealAppFactory>
{
    private readonly HttpClient _client;

    public RecipesEndpointTests(MealAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    ///////////////////////////////////// CREATE //////////////////////////////////
    [Fact]
    public async Task CreateRecipe_WithValidData_ReturnsCreatedRecipe()
    {
        // Arrange
        RecipeCreateRequest newRecipe = new RecipeCreateRequest
        (
            "Test Recipe",
            new List<RecipeIngredientCreateRequest>
            {
                new("ingredient 1", 1, "cup", "notes here"),
                new("ingredient 1", 1, "cup", "notes here")
            },
            new List<RecipeInstructionCreateRequest>
            {
                new(1, "test instruction 1"),
                new(2, "test instruction 2")
            },
            new List<RecipeEquipmentCreateRequest>
            {
                new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff"),
                new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff")
            },
            "/test.jpg",
            "Test description",
            4.5m,
            "00:10:00",
            "00:20:00",
            "00:30:00",
            4,
            "Dinner",
            "Italian",
            "Test Author",
            "test source",
            "video url",
            "notes here"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/meals", newRecipe);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdRecipe = await response.Content.ReadFromJsonAsync<RecipeDetailResponse>();

        // Verify response contract
        createdRecipe.Should().NotBeNull();
        createdRecipe.Id.Should().BeGreaterThan(0);
        createdRecipe.Name.Should().Be(newRecipe.Name);
        createdRecipe.Description.Should().Be(newRecipe.Description);
        createdRecipe.Rating.Should().Be(newRecipe.Rating);
        createdRecipe.ServingSize.Should().Be(newRecipe.ServingSize);

        createdRecipe.Ingredients.Should().HaveCount(2);
        createdRecipe.Ingredients[0].Name.Should().Be(newRecipe.Ingredients[0].Name);
        createdRecipe.Ingredients[0].Quantity.Should().Be(newRecipe.Ingredients[0].Quantity);
        createdRecipe.Ingredients[0].Unit.Should().Be(newRecipe.Ingredients[0].Unit);
        //createdRecipe.Ingredients[0].Calories.Should().Be(newRecipe.Ingredients[0].);

        createdRecipe.Equipment.Should().HaveCount(2);
        createdRecipe.Equipment[0].Name.Should().Be(newRecipe.Equipment[0].Name);
        createdRecipe.Equipment[0].Quantity.Should().Be(newRecipe.Equipment[0].Quantity);
        createdRecipe.Equipment[0].Notes.Should().Be(newRecipe.Equipment[0].Notes);

        createdRecipe.RecipeInstructions.Should().HaveCount(2);
        createdRecipe.RecipeInstructions[0].StepNumber.Should().Be(newRecipe.Instructions[0].StepNumber);
        createdRecipe.RecipeInstructions[0].Instruction.Should().Be(newRecipe.Instructions[0].Instruction);

        // Verify Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain($"/meals/{createdRecipe.Id}");
    }


    //////////////////////////////////// READ //////////////////////////////////
    [Fact]
    public async Task GetRecipe_ReturnsRecipeWithCorrectContract()
    {
        // Arrange
        var recipeId = 1;

        // Act
        var response = await _client.GetAsync($"/meals/{recipeId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var recipe = await response.Content.ReadFromJsonAsync<RecipeDetailResponse>();

        // Verify contract structure
        recipe.Should().NotBeNull();
        recipe.Id.Should().Be(recipeId);
        recipe.Name.Should().NotBeNullOrEmpty();
        recipe.ImageUrl.Should().NotBeNullOrEmpty();
        recipe.Description.Should().NotBeNullOrEmpty();
        recipe.Rating.Should().BeGreaterThan(0);
        recipe.PrepTime.Should().NotBeNull();
        recipe.CookTime.Should().NotBeNull();
        recipe.TotalTime.Should().NotBeNull();
        recipe.ServingSize.Should().BeGreaterThan(0);
        recipe.Course.Should().NotBeNullOrEmpty();
        recipe.Cuisine.Should().NotBeNullOrEmpty();

        // Verify nested collections
        recipe.Ingredients.Should().NotBeNull();
        recipe.Ingredients.Should().NotBeEmpty();
        recipe.Ingredients[0].Should().NotBeNull();
        recipe.Ingredients[0].Name.Should().NotBeNullOrEmpty();
        recipe.Ingredients[0].Quantity.Should().BeGreaterThan(0);
        recipe.Ingredients[0].Unit.Should().NotBeNullOrEmpty();

        recipe.Equipment.Should().NotBeNull();
        recipe.Equipment.Should().NotBeEmpty();
        recipe.Equipment[0].Should().NotBeNull();
        recipe.Equipment[0].Name.Should().NotBeNullOrEmpty();

        recipe.RecipeInstructions.Should().NotBeNull();
        recipe.RecipeInstructions.Should().NotBeEmpty();
        recipe.RecipeInstructions[0].Should().NotBeNull();
        recipe.RecipeInstructions[0].StepNumber.Should().BeGreaterThan(0);
        recipe.RecipeInstructions[0].Instruction.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetRecipe_NotFound_Returns404()
    {
        // Act
        var response = await _client.GetAsync("/meals/99999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    //////////////////////////////////// UPDATE //////////////////////////////////
    [Fact]
    public async Task UpdateRecipe_WithValidData_ReturnsUpdatedRecipe()
    {
        // Arrange
        var recipeId = 1;
        var updateRequest = new RecipeUpdateRequest
        {
            Name = "Updated Recipe Name",
            ImageUrl = "/updated.jpg",
            Description = "Updated description",
            Rating = 5.0m,
            PrepTime = "00:15:00",
            CookTime = "00:25:00",
            TotalTime = "00:40:00",
            ServingSize = 6,
            Course = "Lunch",
            Cuisine = "French",
            Author = "Updated Author",
            SourceUrl = "http://example.com",
            VideoUrl = "http://example.com/video",
            Notes = "Updated notes",
            RecipeIngredients = new List<RecipeIngredientCreateRequest>
            {
                new("New Ingredient", 100, "g", "notes")
            },
            RecipeEquipment = new List<RecipeEquipmentCreateRequest>
            {
                new("New Equipment","equipmnent description", "source url", 3, "notes")
            },
            RecipeInstructions = new List<RecipeInstructionCreateRequest>
            {
                new(1, "step")
            }
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/meals/{recipeId}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var updatedRecipe = await response.Content.ReadFromJsonAsync<RecipeDetailResponse>();

        // Verify contract
        updatedRecipe.Should().NotBeNull();
        updatedRecipe.Id.Should().Be(recipeId);
        updatedRecipe.Name.Should().Be(updateRequest.Name);
        updatedRecipe.Description.Should().Be(updateRequest.Description);
        updatedRecipe.Rating.Should().Be(updateRequest.Rating);
        updatedRecipe.PrepTime.Should().Be(updateRequest.PrepTime);
        updatedRecipe.CookTime.Should().Be(updateRequest.CookTime);
        updatedRecipe.TotalTime.Should().Be(updateRequest.TotalTime);
        updatedRecipe.ServingSize.Should().Be(updateRequest.ServingSize);
        updatedRecipe.Course.Should().Be(updateRequest.Course);
        updatedRecipe.Cuisine.Should().Be(updateRequest.Cuisine);

        updatedRecipe.Ingredients.Should().HaveCount(1);
        updatedRecipe.Ingredients[0].Name.Should().Be(updateRequest.RecipeIngredients[0].Name);
        updatedRecipe.Ingredients[0].Quantity.Should().Be(updateRequest.RecipeIngredients[0].Quantity);
        updatedRecipe.Ingredients[0].Unit.Should().Be(updateRequest.RecipeIngredients[0].Unit);
        //updatedRecipe.Ingredients[0].Note.Should().Be(updateRequest.RecipeIngredients[0].Name);

        updatedRecipe.Equipment.Should().HaveCount(1);
        updatedRecipe.Equipment[0].Name.Should().Be(updateRequest.RecipeEquipment[0].Name);
        //updatedRecipe.Equipment[0].Description.Should().Be(updateRequest.RecipeEquipment[0].Description);
        updatedRecipe.Equipment[0].SourceUrl.Should().Be(updateRequest.RecipeEquipment[0].SourceUrl);
        updatedRecipe.Equipment[0].Quantity.Should().Be(updateRequest.RecipeEquipment[0].Quantity);
        updatedRecipe.Equipment[0].Notes.Should().Be(updateRequest.RecipeEquipment[0].Notes);

        updatedRecipe.RecipeInstructions.Should().HaveCount(1);
        updatedRecipe.RecipeInstructions[0].StepNumber.Should().Be(updateRequest.RecipeInstructions[0].StepNumber);
        updatedRecipe.RecipeInstructions[0].Instruction.Should().Be(updateRequest.RecipeInstructions[0].Instruction);
    }

    [Fact]
    public async Task UpdateRecipe_NotFound_Returns404()
    {
        // Arrange
        var updateRequest = new RecipeUpdateRequest
        {
            Name = "Updated Recipe Name",
            ImageUrl = "/updated.jpg",
            Description = "Updated description",
            Rating = 5.0m,
            PrepTime = "00:15:00",
            CookTime = "00:25:00",
            TotalTime = "00:40:00",
            ServingSize = 6,
            Course = "Lunch",
            Cuisine = "French",
            Author = "Updated Author",
            SourceUrl = "http://example.com",
            VideoUrl = "http://example.com/video",
            Notes = "Updated notes",
            RecipeIngredients = new List<RecipeIngredientCreateRequest>
            {
                new("New Ingredient", 100, "g", "notes")
            },
            RecipeEquipment = new List<RecipeEquipmentCreateRequest>
            {
                new("New Equipment","equipmnent description", "source url", 3, "notes")
            },
            RecipeInstructions = new List<RecipeInstructionCreateRequest>
            {
                new(1, "step")
            }
        };
        // Act
        var response = await _client.PutAsJsonAsync("/meals/99999", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    //////////////////////////////////// DELETE //////////////////////////////////
    [Fact]
    public async Task DeleteRecipe_ExistingRecipe_Returns204()
    {
        // Arrange - Create a recipe to delete
        RecipeCreateRequest newRecipe = new RecipeCreateRequest
        (
            "Test Recipe",
            new List<RecipeIngredientCreateRequest>
            {
                new("ingredient 1", 1, "cup", "notes here"),
                new("ingredient 1", 1, "cup", "notes here")
            },
            new List<RecipeInstructionCreateRequest>
            {
                new(1, "test instruction 1"),
                new(2, "test instruction 2")
            },
            new List<RecipeEquipmentCreateRequest>
            {
                new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff"),
                new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff")
            },
            "/test.jpg",
            "Test description",
            4.5m,
            "00:10:00",
            "00:20:00",
            "00:30:00",
            4,
            "Dinner",
            "Italian",
            "Test Author",
            "test source",
            "video url",
            "notes here"
        );

        var createResponse = await _client.PostAsJsonAsync("/meals", newRecipe);
        var created = await createResponse.Content.ReadFromJsonAsync<RecipeDetailResponse>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/meals/{created.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verify it's actually deleted
        var getResponse = await _client.GetAsync($"/meals/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}