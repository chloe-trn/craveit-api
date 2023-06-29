namespace CraveIt.API.Services.RandomizerService
{
    // Interface that defines the necessary implementation for a randomizer service
    public interface IRandomizerService
    {
        // Get a random item from a list of items
        T GetRandomListItem<T>(List<T> items);
    }
}
