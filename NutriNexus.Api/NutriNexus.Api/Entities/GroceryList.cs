namespace NutriNexus.Api.Entities
{
    public class GroceryList
    {
        public int Id { get; set; }
        public required List<GroceryItem> Items { get; set; }
    }
}
