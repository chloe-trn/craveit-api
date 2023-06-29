namespace CraveIt.API.Services.RandomizerService
{
    public class RandomizerService : IRandomizerService
    {
        private readonly Random _random;

        public RandomizerService()
        {
            // Initialize the random number generator
            _random = new Random();
        }

        // Get a random item from a list of items
        // A generic type parameter is used so a list of any type can be passed 
        public T GetRandomListItem<T>(List<T> items)
        {
            // Generate a random index within the range of the items list
            int randomIndex = _random.Next(0, items.Count);

            // Retrieve the random item using the random index
            T randomItem = items[randomIndex];

            return randomItem;
        }
    }
}
