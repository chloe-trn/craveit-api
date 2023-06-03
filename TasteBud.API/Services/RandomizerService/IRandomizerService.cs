using TasteBud.API.ViewModels.YelpViewModels;

namespace TasteBud.API.Services.RandomizerService
{
    // Interface that defines the necessary implementation for a randomizer service
    public interface IRandomizerService
    {
        // Gets a random item from a list of items
        T GetRandomListItem<T>(List<T> items);
    }
}
