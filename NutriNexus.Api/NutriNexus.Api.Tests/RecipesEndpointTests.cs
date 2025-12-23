using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NutriNexus.Api.DTO;
using NutriNexusAPI.Data;
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
    //[Fact]
    //public async Task CreateRecipe_WithValidData_ReturnsCreatedRecipe()
    //{
    //    // Arrange

    //    RecipeCreateRequest newRecipe = new RecipeCreateRequest
    //    (
    //        "Test Recipe",
    //        new List<RecipeIngredientCreateRequest>
    //        {
    //            new("ingredient 1", 1, "cup", "notes here"),
    //            new("ingredient 1", 1, "cup", "notes here")
    //        },
    //        new List<RecipeInstructionCreateRequest>
    //        {
    //            new(1, "test instruction 1"),
    //            new(2, "test instruction 2")
    //        },
    //        new List<RecipeEquipmentCreateRequest>
    //        {
    //            new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff"),
    //            new("pot", "for boiling water", "get-url-equipment-here.com", 1, "stuff")
    //        },
    //        "/test.jpg",
    //        "Test description",
    //        4.5m,
    //        10,
    //        20,
    //        30,
    //        4,
    //        "Dinner",
    //        "Italian",
    //        "Test Author",
    //        "",
    //        "",
    //        ""
    //    );

    //    // Act
    //    var response = await _client.PostAsJsonAsync("/meals", newRecipe);

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.Created);

    //    var createdRecipe = await response.Content.ReadFromJsonAsync<RecipeDetailResponse>();

    //    // Verify response contract
    //    createdRecipe.Should().NotBeNull();
    //    createdRecipe.Id.Should().BeGreaterThan(0);
    //    createdRecipe.Name.Should().Be(newRecipe.Name);
    //    createdRecipe.Description.Should().Be(newRecipe.Description);
    //    createdRecipe.Rating.Should().Be(newRecipe.Rating);
    //    createdRecipe.ServingSize.Should().Be(newRecipe.ServingSize);

    //    createdRecipe.Ingredients.Should().HaveCount(2);
    //    createdRecipe.Ingredients[0].Name.Should().Be("Pasta");
    //    createdRecipe.Ingredients[0].Quantity.Should().Be(200);
    //    createdRecipe.Ingredients[0].Unit.Should().Be("g");

    //    createdRecipe.Equipment.Should().HaveCount(2);
    //    createdRecipe.Equipment[0].Name.Should().Be("Pot");
    //    createdRecipe.Equipment[0].Quantity.Should().Be(1);

    //    createdRecipe.Directions.Should().HaveCount(3);
    //    createdRecipe.Directions[0].StepNumber.Should().Be(1);
    //    createdRecipe.Directions[0].Instruction.Should().Be("Step 1");

    //    // Verify Location header
    //    response.Headers.Location.Should().NotBeNull();
    //    response.Headers.Location!.ToString().Should().Contain($"/recipes/{createdRecipe.Id}");
    //}


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
        recipe.PrepTime.Should().BeGreaterThan(0);
        recipe.CookTime.Should().BeGreaterThan(0);
        recipe.TotalTime.Should().BeGreaterThan(0);
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

    //[Fact]
    //public async Task UpdateRecipe_WithValidData_ReturnsUpdatedRecipe()
    //{
    //    // Arrange
    //    var recipeId = 1;
    //    var updateRequest = new RecipeUpdateRequest
    //    {
    //        Name = "Updated Recipe Name",
    //        ImageUrl = "/updated.jpg",
    //        Description = "Updated description",
    //        Rating = 5.0m,
    //        PrepTime = 15,
    //        CookTime = 25,
    //        TotalTime = 40,
    //        ServingSize = 6,
    //        Course = "Lunch",
    //        Cuisine = "French",
    //        Author = "Updated Author",
    //        SourceUrl = "http://example.com",
    //        VideoUrl = "http://example.com/video",
    //        Notes = "Updated notes",
    //        Ingredients = new List<IngredientRequest>
    //        {
    //            new() { Name = "New Ingredient", Amount = 100, Unit = "g" }
    //        },
    //        Equipment = new List<EquipmentRequest>
    //        {
    //            new() { Name = "New Equipment", Quantity = 1 }
    //        },
    //        Directions =
    //        [
    //            "New Step 1",
    //            "New Step 2"
    //        ]
    //    };

    //    // Act
    //    var response = await _client.PutAsJsonAsync($"/recipes/{recipeId}", updateRequest);

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var updatedRecipe = await response.Content.ReadFromJsonAsync<RecipeDetailDTO>();

    //    // Verify contract
    //    updatedRecipe.Should().NotBeNull();
    //    updatedRecipe.Id.Should().Be(recipeId);
    //    updatedRecipe.Name.Should().Be(updateRequest.Name);
    //    updatedRecipe.Description.Should().Be(updateRequest.Description);
    //    updatedRecipe.Rating.Should().Be(updateRequest.Rating);

    //    updatedRecipe.Ingredients.Should().HaveCount(1);
    //    updatedRecipe.Ingredients[0].Name.Should().Be("New Ingredient");

    //    updatedRecipe.Equipment.Should().HaveCount(1);
    //    updatedRecipe.Equipment[0].Name.Should().Be("New Equipment");

    //    updatedRecipe.Directions.Should().HaveCount(2);
    //    updatedRecipe.Directions[0].Instruction.Should().Be("New Step 1");
    //}

    //[Fact]
    //public async Task UpdateRecipe_NotFound_Returns404()
    //{
    //    // Arrange
    //    var updateRequest = new RecipeUpdateRequest
    //    {
    //        Name = "Test",
    //        ImageUrl = "/test.jpg",
    //        Description = "Test",
    //        Rating = 4.0m,
    //        PrepTime = 10,
    //        CookTime = 10,
    //        TotalTime = 20,
    //        ServingSize = 4,
    //        Course = "Dinner",
    //        Cuisine = "Italian",
    //        Author = "Test",
    //        SourceUrl = "",
    //        VideoUrl = "",
    //        Notes = "",
    //        Ingredients = new List<IngredientRequest>(),
    //        Equipment = new List<EquipmentRequest>(),
    //        Directions = new List<string>()
    //    };

    //    // Act
    //    var response = await _client.PutAsJsonAsync("/recipes/99999", updateRequest);

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    //}

    //////////////////////////////////// DELETE //////////////////////////////////

    //[Fact]
    //public async Task DeleteRecipe_ExistingRecipe_Returns204()
    //{
    //    // Arrange - Create a recipe to delete
    //    var newRecipe = new RecipeCreateRequest
    //    {
    //        Name = "Recipe to Delete",
    //        ImageUrl = "/delete.jpg",
    //        Description = "Will be deleted",
    //        Rating = 3.0m,
    //        PrepTime = 5,
    //        CookTime = 10,
    //        TotalTime = 15,
    //        ServingSize = 2,
    //        Course = "Snack",
    //        Cusine = "American",
    //        Author = "Test",
    //        SourceUrl = "",
    //        VideoUrl = "",
    //        Notes = "",
    //        Ingredients = new List<IngredientRequest>(),
    //        Equipment = new List<EquipmentRequest>(),
    //        Directions = new List<string> { "Step 1" }
    //    };

    //    var createResponse = await _client.PostAsJsonAsync("/recipes", newRecipe);
    //    var created = await createResponse.Content.ReadFromJsonAsync<RecipeDetailDTO>();

    //    // Act
    //    var deleteResponse = await _client.DeleteAsync($"/recipes/{created.Id}");

    //    // Assert
    //    deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

    //    // Verify it's actually deleted
    //    var getResponse = await _client.GetAsync($"/recipes/{created.Id}");
    //    getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    //}
}